using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController1 : MonoBehaviour
{
    [SerializeField] private Transform[] wheelsTransforms;
    [SerializeField] private Transform[] frontWheelsTransforms;
    [SerializeField] private Transform[] backWheelsTransforms;

    [SerializeField] private WheelCollider[] wheelsColliders;
    [SerializeField] private WheelCollider[] frontWheelsColliders;
    [SerializeField] private WheelCollider[] backWheelsColliders;

    [SerializeField] private float horsePower, maxTurnAngle;

    private void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("MyVertical");
        float horizontalInput = Input.GetAxis("MyHorizontal");
        foreach (var wheel in backWheelsColliders) wheel.motorTorque = verticalInput * horsePower;

        if (Input.GetKey(KeyCode.Space)) foreach (var wheel in wheelsColliders) wheel.brakeTorque = 3000f;

        else foreach (var wheel in wheelsColliders) wheel.brakeTorque = 0f;


        foreach (var wheel in frontWheelsColliders) wheel.steerAngle = horizontalInput * maxTurnAngle;

        RotateWheel(wheelsColliders[0], wheelsTransforms[0]);
        RotateWheel(wheelsColliders[1], wheelsTransforms[1]);
        RotateWheel(wheelsColliders[2], wheelsTransforms[2]);
        RotateWheel(wheelsColliders[3], wheelsTransforms[3]);
    }

    private void RotateWheel(WheelCollider collider, Transform transform)
    {
        Vector3 position; Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        transform.rotation = rotation; transform.position = position;
    }
}