using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : PersistentSingleton<UIController>
{
    public Text scoreText;
    public IntReference currentScore;

    public void UpdateScoreText()
    {
        if (scoreText == null) return;
        scoreText.text = "Score: " + currentScore.Value.ToString();
    }
}
