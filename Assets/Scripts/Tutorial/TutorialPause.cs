using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialPause : MonoBehaviour
{
    public bool isPaused = false;

    [SerializeField] private GameObject tipUIObject;
    private AudioSource playerAudio;
    [SerializeField] private AudioClip dingSound;

    private void Start()
    {
        playerAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
    }

    //Given a string as the tip message, display the tip text on screen and pause movement.
    public void ShowTip(string message)
    {
        playerAudio.PlayOneShot(dingSound, 1f);
        if (tipUIObject != null)
        {
            tipUIObject.SetActive(true);
            tipUIObject.GetComponentInChildren<TextMeshProUGUI>().text = message;
        }

        PauseTutorial();
    }
    
    //Hide the tip text and resume
    public void HideTip()
    {
        if (tipUIObject != null)
        {
            tipUIObject.SetActive(false);
        }

        ResumeTutorial();
    }
    
    //Pause all movement
    private void PauseTutorial()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    //Resume all movement
    private void ResumeTutorial()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
}
