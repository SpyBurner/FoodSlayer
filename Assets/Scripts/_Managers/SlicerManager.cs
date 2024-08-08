using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SlicerManager : PersistentSingleton<SlicerManager>
{

    [Header("Attack set")]
    private SlicerInfo slicerInfo;
    private UltimateInfo ultimateInfo;

    [Header("Touch detection")]
    public LayerMask whatToHit;

    [Header ("Debug info")]
    [SerializeField]
    private Vector3 _lastPosition;

    private GameObject _slicer;
    private SlicerBase _slicerScript;

    private GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        slicerInfo = gameManager.GetComponent<GameManager>().config.chosenSlicer;
        ultimateInfo = gameManager.GetComponent<GameManager>().config.chosenUltimate;

        _lastPosition = new Vector3(0, 0, transform.position.z);

        _slicer = Instantiate(slicerInfo.slicerPrefab, _lastPosition, Quaternion.identity, transform);
        _slicerScript = _slicer.GetComponent<SlicerBase>();
        _slicerScript.INIT(slicerInfo);

        DeactivateSlicer();
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
        _slicerScript.InstantMove(_lastPosition);
    }
    private void MoveSlicer()
    {
        _slicerScript.GetComponent<SlicerBase>().Move(_lastPosition);
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
