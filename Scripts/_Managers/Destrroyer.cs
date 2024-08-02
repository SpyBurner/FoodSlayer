using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Attack"))
            Destroy(collision.gameObject);
    }
}
