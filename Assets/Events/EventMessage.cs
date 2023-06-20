using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EventMessage : MonoBehaviour
{

    public GraphicRaycaster raycaster = null;

    public GameObject focusObj = null; // Only collider detect
    public GameObject uiFocusObj = null; // Only Cavanas detect
    public Vector2 currentCursorWorldPosition = Vector2.zero;
    public Vector2 currentCursorPosition = Vector2.zero;

    public bool IsOnUI
    {
        get { return uiFocusObj != null; }
    }
    public bool IsOnPlanet
    {
        get { return focusObj != null && focusObj.CompareTag("Planet"); }
    }
    public void OnPositionofCursor(InputValue value)
    {
        currentCursorPosition = value.Get<Vector2>();
        SetCursorPosAndWorldPos();
        SetFocusObj();
        SetUIFocusObj();
    }
    void SetFocusObj()
    {
        RaycastHit2D hit = Physics2D.Raycast(currentCursorWorldPosition, Vector2.zero);
        focusObj = hit.rigidbody?.gameObject;
    
    }

    private void SetCursorPosAndWorldPos()
    {
        var pos = Camera.main.ScreenToWorldPoint(currentCursorPosition);
        pos.z += Camera.main.nearClipPlane;
        currentCursorWorldPosition = pos;
    }
    void SetUIFocusObj()
    {
        PointerEventData eventData = new(EventSystem.current);
        eventData.position = currentCursorPosition;
        List<RaycastResult> rcRes = new();
        raycaster.Raycast(eventData, rcRes);
        if(rcRes.Count != 0)
        {
            uiFocusObj = rcRes[0].gameObject;
        }
        else
        {
            uiFocusObj =null;
        }
    }
}
