using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayAndPauseButton : MonoBehaviour
{
    public enum PlayMode { Play, Pause };
    public GameObject playObj;
    public GameObject pauseObj;
    public Movement movement;
    public AudioSource audioSource;
    private PlayMode mode = PlayMode.Play;
    public PlayMode Mode
    {
        get { return mode; } set { mode = value; }
    }
    private Button btn;
    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => {
            // If it is Pause status now and to be Play status, just resume once. But vice versa needed to pause until status changed.
            if (mode == PlayMode.Pause)
            {
                movement.ResumeRigid();
                mode = PlayMode.Play;
                audioSource.Play();
            }else
            {
                mode = PlayMode.Pause;
                audioSource.Pause();
            }
        });
    }
    private void Update()
    {
        switch(mode)
        {
            case PlayMode.Play: playObj.SetActive(false); pauseObj.SetActive(true); break;
            case PlayMode.Pause: playObj.SetActive(true); pauseObj.SetActive(false); movement.PauseRigid(); break;
        }
    }
}
