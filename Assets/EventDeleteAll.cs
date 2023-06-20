using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDeleteAll : MonoBehaviour
{
    public GameObject plantSet;
    private ModeManage modeManage;
    private void Awake()
    {
        modeManage = GetComponent<ModeManage>();
    }
    private void Update()
    {
        if (modeManage.CurrentMode == ModeManage.SelectedMode.DeleteAll)
        {
            foreach (Transform go in plantSet.transform)
            {
                Destroy(go.gameObject);
            }
        }
    }
}
