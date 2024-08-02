using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Config", menuName = "My assets/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Level")]
    public int startLevel = 0;
    public int maxLevel = 10;
    public int pointPerLevel;

    [Header("Spawn rate")]
    public float spawnRate; //in seconds
    public float spawnRateInc;
    public float maxSpawnRate;

    [Header("Spawn amount")]
    public float spawnAmount;
    public float spawnAmountInc;
    public float maxSpawnAmount;

    [Header("Object init info")]
    public float initSpeedMin;
    public float initSpeedMax;

    public float initRotSpeedMin;
    public float initRotSpeedMax;

    public float initAngleMax;
}
