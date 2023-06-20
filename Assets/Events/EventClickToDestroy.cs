using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SelectedMode = ModeManage.SelectedMode;

public class EventClickToDestroy : MonoBehaviour
{
    public InputAction leftClick;
    private EventMessage em;
    private ModeManage eventManage;
    private bool isOpened = true;
    public bool ISOPENED
    {
        get { return isOpened; }
        set { isOpened = value; }
    }

    private void Awake()
    {
        em = GetComponent<EventMessage>();
        eventManage = GetComponent<ModeManage>();
        leftClick.performed += LeftClick_performed;
    }

    private void LeftClick_performed(InputAction.CallbackContext obj)
    {
        OnDestroyPlanet();
    }
    void OnDestroyPlanet()
    {
        if(em.IsOnPlanet)
        {
            em.focusObj.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (eventManage.CurrentMode == SelectedMode.Delete)
        {
            leftClick.Enable();
        }
        else
        {
            leftClick.Disable();
        }
    }
}
