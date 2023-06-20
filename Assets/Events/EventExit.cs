using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventExit : MonoBehaviour
{
    public InputAction exitButton;
    // Start is called before the first frame update
    void Start()
    {
        exitButton.Enable();
        exitButton.performed += OnExit;
    }

    private void OnExit(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
