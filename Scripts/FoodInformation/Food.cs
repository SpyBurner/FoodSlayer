using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Food : MonoBehaviour
{
    public FoodInfo info;
    public float gravityScale = 1;
    public bool isCutable;
    public GameEvent ScoreChanged;
    public IntVariable CurrentScore;

    private Rigidbody rb;
    private Vector2 gravity;
    private MeshFilter meshFilter;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;

        gravity = Vector2.down * 9.81f;

        meshFilter = GetComponent<MeshFilter>();
    }
    public void Initialize(Vector3 speed, Vector3 rotSpeed)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = speed;
        rb.angularVelocity = rotSpeed;

        gameObject.GetComponent<MeshFilter>().mesh = info.mesh;
    }

    private void Update()
    {
        rb.AddForce(gravity * gravityScale * Time.deltaTime, ForceMode.Impulse);
    }

    public void OnHit(Vector3 hitDirection)
    {
        if (!isCutable) return;

        CurrentScore.Value += info.point;
        ScoreChanged.Raise();

        GameObject otherHalf = GenerateCutPieces();
        Rigidbody otherRb = otherHalf.GetOrAddComponent<Rigidbody>();

        float meshYSize = meshFilter.mesh.bounds.extents.y * 2;
        
        transform.right = otherHalf.transform.right = - hitDirection.normalized;

        otherHalf.transform.Rotate(Vector3.forward, 180);

        Vector3 splitForceDirection = Quaternion.Euler(0, 0, -90) * hitDirection;

        Debug.Log("Hit: " + hitDirection + " Split: " + splitForceDirection);

        transform.position -= splitForceDirection.normalized * meshYSize;
        otherHalf.transform.position += splitForceDirection.normalized * meshYSize;

        otherRb.angularVelocity = rb.angularVelocity;
        otherRb.velocity = rb.velocity;

        rb.AddForce(-splitForceDirection * 50);
        otherRb.AddForce(splitForceDirection * 50);
    }

    private GameObject GenerateCutPieces()
    {
        GameObject otherHalf = Instantiate(gameObject, transform.position, Quaternion.identity);
        
        meshFilter.mesh = info.slicedMeshes[0];
        otherHalf.GetComponent<MeshFilter>().mesh = info.slicedMeshes[1];
        isCutable = false;
        otherHalf.GetComponent<Food>().isCutable = false;

        return otherHalf;
    }
}
