using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject levelOptions;
    private Button[] startMenuButtons;
    //private TextMeshProUGUI[] buttonText;
    
    
    void Start()
    {
        foreach (Button startMenuChildButton in startMenu.GetComponentsInChildren<Button>())
        {
            startMenuChildButton.onClick.AddListener(() => SelectOption(startMenuChildButton));
        }
    }

    void SelectOption(Button buttonClicked)
    {
        print("button clicked");
        TextMeshProUGUI buttonText = buttonClicked.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText.text == "Levels")
        {
            SwitchMenus(true);
        }
        else if (buttonText.text == "Tutorial")
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (buttonText.text == "Exit")
        {
            Application.Quit();
        }
    }

    void SwitchMenus(bool isLevelOptions)
    {
        startMenu.SetActive(!isLevelOptions);
        levelOptions.SetActive(isLevelOptions);
    }
    
}
