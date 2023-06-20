using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventClickFocus : MonoBehaviour
{
    private EventMessage em;
    void Awake()
    {
        em = GetComponent<EventMessage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (em.uiFocusObj != null) Debug.Log("UI Focus: " + em.uiFocusObj.name); 
        else if (em.focusObj != null)  Debug.Log("Focus obj: "+em.focusObj.name);
    }
}
