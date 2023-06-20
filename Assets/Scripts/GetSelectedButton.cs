using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GetSelectedButton : MonoBehaviour
{
    public ToggleGroup tg;

    private GameObject selectedButton = null;
    private string selectedButtonLabel = "";
    private Toggle fstTg;
    public GameObject SelectedButton
    {
        get { return selectedButton; }
    }
    public string SelectedButtonLabel
    {
        get { return selectedButtonLabel; }
    }
    private void Update()
    {
        
        if(tg) fstTg = tg.GetFirstActiveToggle();
        if( fstTg )
        {
            selectedButton = fstTg.gameObject;
            selectedButtonLabel = selectedButton.GetComponent<ButtonLabel>().label;
        }
        else
        {
            selectedButton = null;
            selectedButtonLabel = "";
        }
    }

}
