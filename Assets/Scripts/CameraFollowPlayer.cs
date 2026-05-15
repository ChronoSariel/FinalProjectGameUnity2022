using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject Player;
    private float cameraSpeed = 3f;
    private Vector3 offset = new Vector3(0f, 10f, -10f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Player.transform.position + offset, cameraSpeed * Time.deltaTime);
    }
}
