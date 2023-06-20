using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using SelectedMode = ModeManage.SelectedMode;

public class EventClickToCreate : MonoBehaviour
{
    public GameObject planet;
    public GameObject planetSet;

    public InputAction leftClick;

    
    public float massOrDensity = 1;
    public float radius = 1;
    // Random
    public bool isMassOrDensityRandom = false;
    public float modMin = 0.1f;
    public float modMax = 0.5f;

    public bool isRadiusRandom = false;
    public float radiusMin = 0.1f;
    public float radiusMax = 0.5f;

    public bool isColorRandom = false;
    public bool isNoMove = false;

    //new planet
    private GameObject newplanet;
    // Vector and its render
    private Vector2 cursorMove;
    private float3 oriPos;

    private EventMessage emsg;
    private ModeManage eventManage;
    private UIMessagePipe messagePipe;
    private LineRenderer lineRenderer;
    private enum Status
    {
        Origin, Creating
    }
    private Status status;

    private void Awake()
    {
        emsg = GetComponent<EventMessage>();
        eventManage = GetComponent<ModeManage>();
        lineRenderer = GetComponent<LineRenderer>();
        messagePipe = GetComponent<UIMessagePipe>();
        status = Status.Origin;
    }
    private void OnEnable()
    {
        leftClick.Enable();
    }
    private void OnDisable()
    {
        leftClick.Disable();
    }

    void Start()
    {
        leftClick.started += LeftClick_started;
        leftClick.performed += LeftClick_performed;
    }


    private void Update()
    {
        if (eventManage.CurrentMode == SelectedMode.Set ||
            eventManage.CurrentMode == SelectedMode.Random
            )
        {
            leftClick.Enable();
        }
        else if (status == Status.Origin)
        {
            leftClick.Disable();
        }
    }

    private void LeftClick_started(InputAction.CallbackContext obj)
    {
        if (!emsg.IsOnUI) status = Status.Creating;
        if (status == Status.Creating)
        {
            OnCreatePlanetAtCursor();
            OnPlanetPaused();
            OnOtherStarted();
        }
    }
    private void LeftClick_performed(InputAction.CallbackContext obj)
    {
        if (status == Status.Creating)
        {
            OnPlanetResume();
            OnOtherPerformed();
        }
        status = Status.Origin;
    }
    public void FixedUpdate()
    {
        if (status==Status.Creating && leftClick.IsPressed())
            UpdateLines();
    }



    void UpdateLines()
    {
        var endPos = GetCurrentCursorPosition();
        Vector3[] vertices = new Vector3[] { oriPos, endPos };
        lineRenderer.SetPositions(vertices);
    }

    void OnOtherStarted()
    {
        oriPos = GetCurrentCursorPosition();
        lineRenderer.enabled = true;
    }
    void OnOtherPerformed()
    {
        var finalPos = GetCurrentCursorPosition();
        cursorMove = new Vector2(finalPos.x - oriPos.x, finalPos.y - oriPos.y);
        lineRenderer.SetPositions(new Vector3[2] { oriPos, oriPos });
        lineRenderer.enabled = false;
        newplanet.GetComponent<Rigidbody2D>().velocity += cursorMove;
    }
    private void OnPlanetResume()
    {
        GetComponent<Movement>().ResumeRigid();
    }

    private void OnPlanetPaused()
    {
        GetComponent<Movement>().PauseRigid();
    }


    private void SetMassAndRadiusAndNoMove()
    {
        if (eventManage.CurrentMode == SelectedMode.Set)
        {
            massOrDensity = messagePipe.MassFromSlider;
            radius = messagePipe.RadiusFromSlider;
            isNoMove = messagePipe.IsNoMove;
            newplanet.GetComponent<Planet>().IsNoMove = isNoMove;
        }
        else
        {
            if (isMassOrDensityRandom) massOrDensity = GetRandomBetween(modMin, modMax);
            if (isRadiusRandom) radius = GetRandomBetween(radiusMin, radiusMax);
            //newplanet.GetComponent<Planet>().IsNoMove = isNoMove;
        }
    }
    private void OnCreatePlanetAtCursor()
    {
        newplanet = Instantiate(planet, GetCurrentCursorPosition(), Quaternion.identity, planetSet.transform);
        var sr = newplanet.GetComponent<SpriteRenderer>();
        SetMassAndRadiusAndNoMove();

        newplanet.GetComponent<Planet>().Initialized(massOrDensity, radius, newplanet.transform.position, default, isColorRandom?GenerateRandomColor():default);


        // Set Trail Color
        newplanet.GetComponent<TrailRenderer>().startColor = sr.color;
        lineRenderer.startColor = ColorDarker(sr.color);
        lineRenderer.endColor = sr.color;
    }

    Color ColorDarker(Color color, float amout_rate = 0.3f)
    {
        return color * amout_rate;
    }
    Color GenerateRandomColor()
    {
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);
        float a = 1.0f;
        return new Color(r, g, b, a);

    }
    float GetRandomBetween(float a, float b)
    {
        return Random.Range(a, b);
    }
    Vector3 GetCurrentCursorPosition()
    {
        return emsg.currentCursorWorldPosition;
    }

}
