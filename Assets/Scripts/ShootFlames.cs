using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFlames : MonoBehaviour
{
public GameObject projectilePrefab;
public float fireRate = 4f;
public float timeToFire = 0f;
public float rotationSpeed = 30f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    transform.Rotate(0,rotationSpeed * Time.deltaTime,0);
    timeToFire -= Time.deltaTime;
    if(timeToFire <= 0f)
        {
        SpawnProjectile();      
        timeToFire = 1f / fireRate;
        }
    void SpawnProjectile()
    {
        Instantiate(projectilePrefab, transform.position, transform.rotation);
    }
    }
}
