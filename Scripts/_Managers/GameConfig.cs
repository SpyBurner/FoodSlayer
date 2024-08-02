using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Config", menuName = "My assets")]
public class GameConfig : ScriptableObject
{
    [Header("Level")]
    public int startLevel = 0;
    public int maxLevel = 10;
    public int pointPerLevel;

    [Header("Spawn rate")]
    public float spawnRate; //in seconds
    public float spawnRateInc;

    [Header("Spawn amount")]
    public float spawnAmount;
    public float spawnAmountInc;

    [Header("Object init info")]
    public float initSpeedMin;
    public float initSpeedMax;

    public float initRotSpeedMin;
    public float initRotSpeedMax;

    public float initAngleMax;
}
