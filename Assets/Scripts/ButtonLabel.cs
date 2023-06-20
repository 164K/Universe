using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonLabel : MonoBehaviour
{
    public string label = "";
    public string showName = "";
    public TextMeshProUGUI labelText;
    private void Awake()
    {
        if (showName=="")
        {
            showName = label;
        }
    }
    private void Update()
    {
        labelText.text = showName;
    }
}
