using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialPause : MonoBehaviour
{
    public bool isPaused = false;

    [SerializeField] private GameObject tipUIObject;
    public void ShowTip(string message)
    {
        if (tipUIObject != null)
        {
            tipUIObject.SetActive(true);
            tipUIObject.GetComponentInChildren<TextMeshProUGUI>().text = message;
        }

        PauseTutorial();
    }

    public void HideTip()
    {
        if (tipUIObject != null)
        {
            tipUIObject.SetActive(false);
        }

        ResumeTutorial();
    }

    private void PauseTutorial()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ResumeTutorial()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
}
