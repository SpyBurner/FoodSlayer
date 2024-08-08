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

            if (food.isCutable && food.info.healAmount >= 0)
            {
                OnFoodDropped.Raise();
            }

            Destroy(collision.gameObject);
        }
    }
}
