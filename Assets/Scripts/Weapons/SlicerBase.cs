using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicerBase : MonoBehaviour
{
    private SlicerInfo _slicerInfo;
    public void INIT(SlicerInfo slicerInfo)
    {
        _slicerInfo = slicerInfo;
    }

    private MeshFilter _meshFilter;
    private TrailRenderer _trailRenderer;
    private Rigidbody _rb;
    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _trailRenderer = GetComponent<TrailRenderer>();

        _trailRenderer.colorGradient = _slicerInfo.trailColor;

        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation & RigidbodyConstraints.FreezePositionZ;
        _rb.useGravity = false;
    }
    private void Update()
    {
        Debug.Log(_rb.velocity.magnitude);   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Food")) return;

        if (other.gameObject.TryGetComponent<Food>(out var food)
            && (Mathf.Abs(_rb.velocity.magnitude) > 0.3)
            )
        {
            food.OnHit(_rb.velocity.normalized * _slicerInfo.bluntness * 500, _slicerInfo);
        }
    }

    float swingSpeedMultiplier = 100;
    public void Move(Vector3 _lastPosition)
    {
        if (Vector3.Distance(transform.position, _lastPosition) <= Mathf.Epsilon) return;

        _rb.velocity = ((_lastPosition - transform.position) * _slicerInfo.swingSpeed * swingSpeedMultiplier) * Time.deltaTime;

        if (_rb.velocity.magnitude > _slicerInfo.swingSpeed * swingSpeedMultiplier)
        {
            _rb.velocity = _rb.velocity.normalized * _slicerInfo.swingSpeed * swingSpeedMultiplier;
        }
    }

    public void InstantMove(Vector3 lastPosition)
    {
        transform.position = lastPosition;
    }

    public void SetActive(bool active)
    {
        gameObject.GetComponent<Collider>().enabled = active;
        gameObject.GetComponent<TrailRenderer>().enabled = active;
    }

    private void OnDisable()
    {
        _rb.velocity = Vector3.zero;
    }
}
