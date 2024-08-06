using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My assets/Attack/New default ultimate", order = 1)]
public class UltimateInfo : ScriptableObject
{
    public GameObject ultimatePrefab;
    [Header("Visual effects")]
    public Mesh effectShape;
    public Vector3 effectSizeScale;

    [Header("Technical effects")]
    public float chargeMeterMax;
    public float timeSlowMultiplier;
    public float knockUpMultiplier;
}
