using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SelectedMode = ModeManage.SelectedMode;
public class UIController : MonoBehaviour
{
    private ModeManage modeManage;
    public GameObject slider;
    private void Awake()
    {
        modeManage = GetComponent<ModeManage>();
    }
    private void Update()
    {

        slider.SetActive(modeManage.CurrentMode == SelectedMode.Set);
    }

}
