using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    //Title_Screen buttons and the levelOptions buttons
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject levelOptions;
    
    void Start()
    {
        //Add listeners to the start menu buttons at start
        AddButtonListeners(startMenu, false);
    }
    
    //Navigate to the right place depending on what was clicked. If levels was clicked, switch the menus, If tutorial was clicked, play the tutorial, otherwise exit.
    void SelectOption(Button buttonClicked)
    {
        TextMeshProUGUI buttonText = buttonClicked.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText.text == "Levels")
        {
            SwitchMenus(true);
            AddButtonListeners(levelOptions, true);
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
    
    //Switch from start menu to level options
    void SwitchMenus(bool isLevelOptions)
    {
        startMenu.SetActive(!isLevelOptions);
        levelOptions.SetActive(isLevelOptions);
    }
    
    //Will add the appropriate listener depending on what was clicked. If levels was clicked, then a new set of buttons will appear. If the others were clicked SelectOption will handle the action.
    void AddButtonListeners(GameObject parent, bool isLevelOptions)
    {
        if (!isLevelOptions)
        {
            foreach (Button menuChildButton in parent.GetComponentsInChildren<Button>())
            {
                menuChildButton.onClick.AddListener(() => SelectOption(menuChildButton));
            }
        }
        else
        {
            foreach (Button menuChildButton in parent.GetComponentsInChildren<Button>())
            {
                menuChildButton.onClick.AddListener(() => SelectLevel(menuChildButton));
            }
        }
    }

    //Only one level to be selected currently, but can be expanded for more.
    void SelectLevel(Button buttonClicked)
    {
        TextMeshProUGUI buttonText = buttonClicked.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText.text == "1")
        {
            SceneManager.LoadScene("Level_1");
        }
    }
}
