using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour
{
    private SlicerInfo _slicerInfo;
    public void INIT(SlicerInfo slicerInfo)
    {
        _slicerInfo = slicerInfo;
    }

    private MeshFilter _meshFilter;
    private TrailRenderer _trailRenderer;
    private Rigidbody rb;
    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _trailRenderer = GetComponent<TrailRenderer>();

        _trailRenderer.startColor = _trailRenderer.endColor = _slicerInfo.trailColor;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Food")) return;

        if (other.gameObject.TryGetComponent<Food>(out var food)
            && (Mathf.Abs(rb.velocity.magnitude) > 0.3)
            )
        {
            food.OnHit(rb.velocity.normalized);
        }
    }
}
