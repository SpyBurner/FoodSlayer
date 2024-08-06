using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My assets/Attack/New default slicer", order = 1)]
public class SlicerInfo : ScriptableObject
{
    [Header("Visual effects")]
    [Range(0f, 1f)]
    public float bluntness;
    public Color trailColor;
    public Mesh trailShape;

    [Header("Technical effects")]
    public float swingSpeed; //m/s
    public float pointMultiplier;
    public float ultimateChargePerHit;
}
