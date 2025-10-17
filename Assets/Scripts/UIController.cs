using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalAttemptsText;

    private GameController _gameController;
    // Start is called before the first frame update
    void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void UpdateAttempts()
    {
        totalAttemptsText.text = "Total Attempts: " + _gameController.TotalAttempts;
    }
}
