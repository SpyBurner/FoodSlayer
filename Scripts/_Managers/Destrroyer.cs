using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrroyer : MonoBehaviour
{
    public GameEvent OnFoodDropped;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            Food food = collision.gameObject.GetComponent<Food>();

            if (food.isCutable)
            {
                OnFoodDropped.Raise();
            }
        }
        if (collision.gameObject.layer != LayerMask.NameToLayer("Attack"))
            Destroy(collision.gameObject);
    }
}
