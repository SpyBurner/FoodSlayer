using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    //Score only need to update when needed
    [Header("Score managing")]
    public GameEvent ScoreChanged;
    public IntVariable CurrentScore;

    [Header("Difficulty managing")]
    public GameConfig config;
    public IntVariable CurrentLevel;

    private void Start()
    {
        CurrentScore.Value = 0;
        ScoreChanged.Raise();

        CurrentLevel.Value = 0;
        Time.timeScale = 0.5f;
    }

    private void Update()
    {
        CurrentLevel.Value = CurrentScore.Value / config.pointPerLevel;
    }
}
