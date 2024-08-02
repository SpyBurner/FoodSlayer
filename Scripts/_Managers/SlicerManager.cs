using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SlicerManager : PersistentSingleton<SlicerManager>
{
    [Header("Slicer prefab")]
    public GameObject slicerPrefab;

    [Header("Attack set")]
    public SlicerInfo slicerInfo;
    public UltimateInfo UltimateInfo;

    [Header("Touch detection")]
    public LayerMask whatToHit;

    [Header ("Debug info")]
    [SerializeField]
    private Vector3 _lastPosition;
    private Rigidbody _rb;

    private GameObject _slicer;

    private void Start()
    {
        _lastPosition = new Vector3(0, 0, transform.position.z);

        _slicer = Instantiate(slicerPrefab, _lastPosition, Quaternion.identity, transform);
        _slicer.GetComponent<Slicer>().INIT(slicerInfo);
        DeactivateSlicer();

        _rb = _slicer.GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation & RigidbodyConstraints.FreezePositionZ;
        _rb.useGravity = false;
    }

    private bool isSlicing;
    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            UpdatePosition();

            if (!isSlicing) InstantMoveSlicer();
            else MoveSlicer();

            ActivateSlicer();
        }
        else
        {
            DeactivateSlicer();
        }
    }

    private void InstantMoveSlicer()
    {
        _slicer.transform.position = _lastPosition;
    }
    private void MoveSlicer()
    {
        if (Vector3.Distance(_slicer.transform.position, _lastPosition) <= Mathf.Epsilon) return;

        _rb.velocity = ((_lastPosition - _slicer.transform.position) * slicerInfo.swingSpeed) * Time.deltaTime;
    }

    private void UpdatePosition()
    {
        Ray ray;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            ray = Camera.main.ScreenPointToRay(touch.position);
        }
        else
        {
            Vector3 click = Input.mousePosition;
            ray = Camera.main.ScreenPointToRay(click);
        }

        RaycastHit[] hit = Physics.RaycastAll(ray, 10, whatToHit);

        if (hit.Length <= 0) return;

        Vector3 newPosition = new Vector3(hit[0].point.x, hit[0].point.y, transform.position.z);

        _lastPosition = newPosition;
    }

    private void ActivateSlicer()
    {
        _slicer.SetActive(true);
        isSlicing = true;
    }

    private void DeactivateSlicer()
    {
        _slicer.SetActive(false);
        isSlicing = false;
    }


}
