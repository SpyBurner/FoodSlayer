using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ejector : MonoBehaviour
{
    public ParticleSystem ejector;
    void Start()
    {
        ejector = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EjectBullet()
    {
        ejector.Play();
    }
}
