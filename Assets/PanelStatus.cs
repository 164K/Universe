using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelStatus : MonoBehaviour
{
    public Button foldButton;
    private Animator animator;
    public GameObject foldBtn;
    public GameObject expandBtn;
    public enum Status
    {
        Fold, Expand
    }
    public Status currentPanelStatus;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentPanelStatus = Status.Fold;
        foldButton.onClick.AddListener(TogglePanel);
    }
    private void TogglePanel()
    {
        animator.SetTrigger("TogglePanel");
        currentPanelStatus = currentPanelStatus == Status.Fold ? Status.Expand : Status.Fold;
    }
    private void Update()
    {
        switch(currentPanelStatus)
        {
            case Status.Fold: foldBtn.SetActive(true); expandBtn.SetActive(false); break;
            case Status.Expand: foldBtn.SetActive(false); expandBtn.SetActive(true); break;
        }
    }
}
