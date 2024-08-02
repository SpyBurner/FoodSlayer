using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

[RequireComponent(typeof(MeshRenderer))]
public class Spawner : MonoBehaviour
{
    [Header("Point update")]
    public IntVariable CurrentScore;
    public GameEvent ScoreChanged;

    [Header("Object Prefab")]
    [SerializeField] private GameObject prefab;

    [Header("Object information")]
    [SerializeField] private FoodInfo[] objInfo;


    [Header("Config")]
    public GameConfig config;
    public float spawnRate; //in seconds
    public float spawnAmount;
    public IntReference Level;

    private MeshRenderer meshRenderer;
    private float spawnBoundX;
    private GameObject holder;

    [Header ("Extra info")]
    [SerializeField] private float spawnTimer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        spawnBoundX = meshRenderer.bounds.extents.x;
        spawnPos = Vector3.zero;

        spawnTimer = 0;

        holder = new GameObject();
        holder.name = "Food Holder";

        spawnRate = config.spawnRate;
        spawnAmount = config.spawnAmount;
    }

    void Update()
    {
        SpawnInfoUpdate();
        AdvanceTimer();
        //Check if it's time to spawn
        if (!TimeCheck()) return;

        //Time to spawn
        for (int i = 0; i < (int)spawnAmount; i++)
        {
            RandomizeSpawnInfo();
            Spawn();
        }
    }

    private void SpawnInfoUpdate()
    {
        spawnRate = config.spawnRate + config.spawnRateInc * Level.Value;
        spawnRate = Mathf.Min(spawnRate, config.maxSpawnRate);

        spawnAmount = config.spawnAmount + config.spawnAmountInc * Level.Value;
        spawnAmount = Mathf.Min(spawnAmount, config.maxSpawnAmount);
    }

    private void AdvanceTimer()
    {
        spawnTimer += Time.deltaTime;
    }

    private bool TimeCheck()
    {
        if (spawnTimer >= 1 / spawnRate)
        {
            spawnTimer = 0;
            return true;
        }
        return false;
    }

    [SerializeField] private Vector3 spawnPos;
    private int newObjIndex;
    private Vector3 newVelocity;
    private Vector3 newAngularVelocity;
    private void RandomizeSpawnInfo()
    {
        newObjIndex = Random.Range(0, objInfo.Length);

        float r = Random.Range(0, spawnBoundX * 2);
        spawnPos = new Vector3(0 - spawnBoundX + r, transform.position.y, transform.position.z);

        float newSpeed = Random.Range(config.initSpeedMin, config.initSpeedMax);
        
        float newRotSpeed = Random.Range(config.initRotSpeedMin, config.initRotSpeedMax);

        Vector3 newDirection = Quaternion.AngleAxis(Random.Range(-config.initAngleMax, config.initAngleMax), Vector3.forward) * Vector3.up;

        newVelocity = newSpeed * newDirection;

        newAngularVelocity = Random.insideUnitSphere * newRotSpeed;
    }

    private void Spawn()
    {
        
        FoodInfo info = objInfo[newObjIndex];

        GameObject newObj = Instantiate(prefab, spawnPos, Quaternion.identity, holder.transform);
        Food food =  newObj.GetComponent<Food>();
        food.info = info;
        food.Initialize(newVelocity, newAngularVelocity);

        food.ScoreChanged = ScoreChanged;
        food.CurrentScore = CurrentScore;

        food.foodHolder = holder;
    }

}
