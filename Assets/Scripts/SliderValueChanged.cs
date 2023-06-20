using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChanged : MonoBehaviour
{
    public string textFormat;
    public TextMeshProUGUI tmp;
    private void FixedUpdate()
    {
        tmp.text = string.Format(textFormat, GetComponent<Slider>().value);
    }
}
