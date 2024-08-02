using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameConfig config;

    //Score only need to update when needed
    [Header("HP managing")]
    public IntVariable HP;
    public GameEvent HPChanged;
    public GameEvent LoseEvent;

    [Header("Score managing")]
    public GameEvent ScoreChanged;
    public IntVariable CurrentScore;
    public IntVariable HighScore;

    [Header("Difficulty managing")]
    public IntVariable CurrentLevel;


    [Header("Debug")]
    [Range(0f, 1f)]
    public float timeScale;
    private void Start()
    {
        CurrentScore.Value = 0;
        ScoreChanged.Raise();

        CurrentLevel.Value = config.startLevel;

        HP.Value = config.startHP;
        HPChanged.Raise();
    }

    private void Update()
    {
        Time.timeScale = timeScale;
        CurrentLevel.Value = CurrentScore.Value / config.pointPerLevel;
        
        HP.Value = Mathf.Min(HP.Value, config.maxHP);
    }

    public void OnHPChanged()
    {
    }

    public void OnFoodDrop()
    {
        HP.Value -= 1;
        HPChanged.Raise();
        if (HP.Value <= 0)
        {
            HighScore.Value = Mathf.Max(HighScore.Value, CurrentScore.Value);
            LoseEvent.Raise();
        }
    }

    public void OnLose()
    {
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(0);
    }
}
