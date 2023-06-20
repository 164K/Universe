using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMessagePipe : MonoBehaviour
{
    public Slider radiusSlider;
    public Slider massSlider;
    public Toggle noMoveToggle;

    public float RadiusFromSlider
    { get { return radiusSlider ? radiusSlider.value : 0.0f; } set { if (radiusSlider) radiusSlider.value = value; } }
    public float MassFromSlider
    { get { return massSlider ? massSlider.value : 0.0f; } set { if (massSlider) massSlider.value = value; } }

    public bool IsNoMove
    { get { return noMoveToggle ? noMoveToggle.isOn : false; } set { if (noMoveToggle) noMoveToggle.isOn = value; } }
}
