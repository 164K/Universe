using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InputManagement : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject planets;
    //
    public InputAction leftClick;
    public InputAction cursor;
    public InputAction exitBtn;
    public GameObject planet;
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


    //new planet
    private GameObject newplanet;
    // Vector and its render
    private Vector2 cursorMove;
    private float3 oriPos;
    void Start()
    {
        leftClick.started += OnCreatePlanetAtCursor;
        leftClick.started += OnPlanetPaused;
        leftClick.started += OnStarted;

        leftClick.performed += OnPlanetResume;
        leftClick.performed += OnPerformed;

        exitBtn.performed += OnExit;
        Enable();

    }

    private void OnExit(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    public void FixedUpdate()
    {
        if (leftClick.IsPressed())
        {
            var endPos = GetCurrentCursorPosition();
            Vector3[] vertices = new Vector3[] { oriPos, endPos };
            GetComponent<LineRenderer>().SetPositions(vertices);
        }
    }
    void OnStarted(InputAction.CallbackContext obj)
    {

        oriPos = GetCurrentCursorPosition();
        GetComponent<LineRenderer>().enabled = true;
    }
    void OnPerformed(InputAction.CallbackContext obj)
    {

        var finalPos = GetCurrentCursorPosition(); 
        cursorMove = new Vector2(finalPos.x - oriPos.x, finalPos.y - oriPos.y);
        GetComponent<LineRenderer>().SetPositions(new Vector3[2] { oriPos, oriPos });
        GetComponent<LineRenderer>().enabled = false;
        newplanet.GetComponent<Rigidbody2D>().velocity += cursorMove;
    }
    private void OnPlanetResume(InputAction.CallbackContext obj)
    {
        planets.GetComponent<Movement>().ResumeRigid();
    }

    private void OnPlanetPaused(InputAction.CallbackContext obj)
    {
        planets.GetComponent<Movement>().PauseRigid();
    }

    private void OnCreatePlanetAtCursor(InputAction.CallbackContext obj)
    {
        newplanet = Instantiate(planet, GetCurrentCursorPosition(), Quaternion.identity);
        var sr = newplanet.GetComponent<SpriteRenderer>();

        if (isMassOrDensityRandom) massOrDensity = GetRandomBetween(modMin, modMax);
        if (isRadiusRandom) radius = GetRandomBetween(radiusMin, radiusMax);

        newplanet.GetComponent<Planet>().Initialized(massOrDensity, radius, newplanet.transform.position);
        if (isColorRandom) sr.color = GenerateRandomColor();
    }

    //private void FixedUpdate()
    //{
    //    if (leftClick.IsPressed()) planets.GetComponent<Movement>().PauseRigid(); else planets.GetComponent<Movement>().ResumeRigid();
    //}
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
        var transPosition = Camera.main.ScreenToWorldPoint(cursor.ReadValue<Vector2>());
        transPosition.z += Camera.main.nearClipPlane;
        return transPosition;
    }

    private void Enable()
    {
        leftClick.Enable();
        cursor.Enable();
        exitBtn.Enable();
    }
    private void Disable()
    {
        leftClick.Disable();
        cursor.Disable();
        exitBtn.Disable();
    }
}
