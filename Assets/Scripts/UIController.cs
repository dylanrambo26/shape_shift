using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{   
    [SerializeField] private GameObject totalAttemptsObject;
    [SerializeField] private GameObject levelCompleteObject;

    private TextMeshProUGUI totalAttemptsText;
    //private TextMeshProUGUI levelCompleteText;
    private GameController _gameController;
    // Start is called before the first frame update
    void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        totalAttemptsText = totalAttemptsObject.GetComponent<TextMeshProUGUI>();
       // levelCompleteText = levelCompleteObject.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateAttempts()
    {
        totalAttemptsText.text = "Total Attempts: " + _gameController.TotalAttempts;
    }

    public void ShowLevelComplete()
    {
        levelCompleteObject.SetActive(true);
        totalAttemptsObject.SetActive(false);
    }
}
