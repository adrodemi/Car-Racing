using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horsePower = 20f;//, turnSpeed = 45f;

    [SerializeField] private TextMeshProUGUI speedometerText, rpmText;
    private float speed, rpm;

    [SerializeField] private List<WheelCollider> allWheels;
    [SerializeField] private int wheelsOnGround;

    [SerializeField] private List<WheelCollider> frontWheels;
    public float maxTurnAngle = 45f;

    public GameObject centerOfMass;

    private float horizontalInput, verticalInput;

    private Rigidbody playerRb;
    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        //rb.centerOfMass = centerOfMass.transform.position;
    }
    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)) foreach (var wheel in allWheels) wheel.brakeTorque = 3000;
        else foreach (var wheel in allWheels) wheel.brakeTorque = 0;

        if (IsOnGround())
        {
            foreach (var wheel in allWheels)
            {
                wheel.motorTorque = verticalInput * horsePower;
            }
            foreach (var wheel in frontWheels)
            {
                wheel.steerAngle = horizontalInput * Time.deltaTime * maxTurnAngle;
                RotateWheel(wheel, wheel.gameObject.transform);
                //if (wheel.transform.rotation.y > maxTurnAngle)
                //    wheel.transform.rotation = Quaternion.Euler(0, maxTurnAngle, 0);
                //else if (wheel.transform.rotation.y < -maxTurnAngle)
                //    wheel.transform.rotation = Quaternion.Euler(0, -maxTurnAngle, 0);
            }

            //if (playerRb.velocity.x > 25) playerRb.velocity = new Vector3(25, playerRb.velocity.y, playerRb.velocity.z);
            //if (playerRb.velocity.x < -25) playerRb.velocity = new Vector3(-25, playerRb.velocity.y, playerRb.velocity.z);
            //if (playerRb.velocity.z > 25) playerRb.velocity = new Vector3(playerRb.velocity.x, playerRb.velocity.y, 25);
            //if (playerRb.velocity.z < -25) playerRb.velocity = new Vector3(playerRb.velocity.x, playerRb.velocity.y, -25);

            speed = Mathf.Round(playerRb.velocity.magnitude * 3.6f); // 3.6 for kph; 2.237 for mph
            speedometerText.SetText("Speed: " + speed + "mph");

            rpm = Mathf.Round((speed % 30) * 40);
            rpmText.SetText("RPM: " + rpm);
        }
    }
    private void RotateWheel(WheelCollider collider, Transform transform)
    {
        Vector3 position; Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        transform.rotation = rotation; transform.position = position;
    }
    private bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (var wheel in allWheels)
        {
            if (wheel.isGrounded)
                wheelsOnGround++;
        }
        if (wheelsOnGround == 4) return true;
        else return false;
    }
}