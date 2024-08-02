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

    public GameObject foodHolder;

    private Rigidbody rb;
    private Collider col;
    private Vector2 gravity;
    private MeshFilter meshFilter;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;

        gravity = Vector2.down * 9.81f;

        meshFilter = GetComponent<MeshFilter>();
        col = GetComponent<SphereCollider>();
    }
    public void Initialize(Vector3 speed, Vector3 rotSpeed)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = speed;
        rb.angularVelocity = rotSpeed;

        gameObject.GetComponent<MeshFilter>().mesh = info.mesh;
        
        col = GetComponent<SphereCollider>();
        ((SphereCollider)col).radius *= info.scale;
    }

    private void Update()
    {
        rb.AddForce(gravity * gravityScale * Time.deltaTime, ForceMode.Impulse);
    }

    public void OnHit(Vector3 hitDirection)
    {
        if (!isCutable) return;

        Vector3 splitForceDirection = Quaternion.Euler(0, 0, -90) * hitDirection;

        CurrentScore.Value += info.point;
        ScoreChanged.Raise();

        //Cut generating
        GameObject otherHalf = GenerateCutPieces();
        
        //Get info
        Rigidbody otherRb = otherHalf.GetOrAddComponent<Rigidbody>();
        float meshYSize = meshFilter.mesh.bounds.extents.y * 2;
        
        //Cut rotation
        if (info.cutPlaneNormalVec == Vector3.up)
        {
            transform.right = otherHalf.transform.right = - hitDirection.normalized;
        }
        else if (info.cutPlaneNormalVec == Vector3.forward)
        {
            transform.forward = otherHalf.transform.forward = - splitForceDirection.normalized;
        }
        else
        {
            transform.right = otherHalf.transform.right = - splitForceDirection.normalized;
        }
        otherHalf.transform.Rotate(hitDirection, 180);

        //Cut position
        transform.position -= splitForceDirection.normalized * meshYSize;
        otherHalf.transform.position += splitForceDirection.normalized * meshYSize;


        //Cut retain original velocity
        otherRb.angularVelocity = rb.angularVelocity;
        otherRb.velocity = rb.velocity;

        //Cut add splitting
        rb.AddForce(-splitForceDirection * 50);
        otherRb.AddForce(splitForceDirection * 50);
    }

    private GameObject GenerateCutPieces()
    {
        GameObject otherHalf = Instantiate(gameObject, transform.position, Quaternion.identity, foodHolder.transform);
        
        meshFilter.mesh = info.slicedMeshes[0];
        otherHalf.GetComponent<MeshFilter>().mesh = info.slicedMeshes[1];
        isCutable = false;
        otherHalf.GetComponent<Food>().isCutable = false;

        return otherHalf;
    }
}
