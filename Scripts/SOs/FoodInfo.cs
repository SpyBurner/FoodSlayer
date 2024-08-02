using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My assets/Food", order = 1)]
public class FoodInfo : ScriptableObject
{
    public int point;

    public GameObject prefab;

    public Mesh mesh;
    public Mesh[] slicedMeshes;

    public float scale = 1f;
    public Vector3 cutPlaneNormalVec = Vector3.up;

    public int healAmount = 0;
}
