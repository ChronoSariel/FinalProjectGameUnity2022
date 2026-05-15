using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameProjectile : MonoBehaviour
{
    public float Speed = 5.0f;
    public float lifeTime; // Lifetime of the projectile in seconds
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime); // Move Projectile forward.
        lifeTime -= Time.deltaTime; // Decrease lifetime by the time passed since last frame.
        if (lifeTime <= 0) // If lifetime is up, destroy the projectile.
        {
            Destroy(gameObject);
    }
}
}
