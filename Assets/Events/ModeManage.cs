using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeManage : MonoBehaviour
{
    // Start is called before the first frame update
    public enum SelectedMode
    {
        Nothing, Random, Set, Delete, DeleteAll, Reload
    }
    private SelectedMode currentMode = SelectedMode.Nothing;
    public SelectedMode CurrentMode
    {
        set
        {
            if (value != currentMode)
            {
                var previous = currentMode;
                currentMode = value;
                OnModeChanged(previous, value);
            }
        }
        get
        {
            return currentMode;
        }
    }
    void OnModeChanged(SelectedMode previousMode, SelectedMode nowMode)
    {
        Action action = () => { };
        action.Invoke();
    }
    private GetSelectedButton gsBtn;
    private void Awake()
    {
        gsBtn = GetComponent<GetSelectedButton>();
        
    }
    void Update()
    {
        switch(gsBtn.SelectedButtonLabel)
        {
            case "Random": CurrentMode = SelectedMode.Random; break;
            case "Set": CurrentMode = SelectedMode.Set; break;
            case "Delete": CurrentMode = SelectedMode.Delete; break;
            case "DeleteAll": CurrentMode = SelectedMode.DeleteAll; break;
            case "Reload": CurrentMode = SelectedMode.Reload; break;
            default: CurrentMode = SelectedMode.Nothing; break;
        }
    }
}
