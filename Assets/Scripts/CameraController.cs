using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;
    public float rotationSpeed;

    private Rigidbody playerRb;
    private void Awake()
    {
        playerRb = player.GetComponent<Rigidbody>();
    }
    private void LateUpdate()
    {
        Vector3 playerForward = (playerRb.velocity + player.transform.forward).normalized;

        transform.position = Vector3.Lerp(transform.position, 
            player.position + player.TransformVector(offset) + player.forward * (-5f), rotationSpeed * Time.deltaTime);
        
        transform.LookAt(player);
    }
}