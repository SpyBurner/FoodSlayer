using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameUIController : PersistentSingleton<GameUIController>
{
    public Text scoreText;
    public IntReference currentScore;

    public Text HPText;
    public IntReference currentHP;

    public Text HighScoreText;
    public IntReference HighScore;
    public Button RestartButton;

    private void Update()
    {
        UpdateHP();
    }

    public void UpdateScoreText()
    {
        if (scoreText == null || currentScore == null) return;
        scoreText.text = "Score: " + currentScore.Value.ToString();
    }

    public void UpdateHP()
    {
        if (HPText == null || currentHP == null) return;
        HPText.text = "HP: " + currentHP.Value.ToString();
    }

    public void OnLose()
    {
        Debug.Log("UI OnLose() raised!");
        HighScoreText.text = "Highscore: " + HighScore.Value.ToString();
        
        HighScoreText.GetComponent<Text>().enabled = true;

        RestartButton.GetComponent<Button>().enabled = true;
        RestartButton.GetComponentInChildren<Text>().enabled = true;
    }
}
