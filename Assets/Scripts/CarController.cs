using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] allWheelsMeshes;
    [SerializeField] private MeshRenderer[] frontWheelsMeshes;
    [SerializeField] private MeshRenderer[] backWheelsMeshes;

    [SerializeField] private WheelCollider[] allWheelsColliders;
    [SerializeField] private WheelCollider[] frontWheelsColliders;
    [SerializeField] private WheelCollider[] backWheelsColliders;

    private float gasInput, brakeInput, steeringInput;

    public float motorPower, brakePower;

    private float speed, slipAngle;

    public AnimationCurve steeringCurve;

    private Rigidbody playerRb;
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        CheckInput();
        ApplySteering();
        ApplyWheelUpdate();
        speed = playerRb.velocity.magnitude;
        if (Input.GetKeyDown(KeyCode.Space)) foreach (var wheel in backWheelsColliders) wheel.brakeTorque = 3000f;
        else
        {
            ApplyMotor();
            ApplyBrake();
        }
    }
    private void CheckInput()
    {
        gasInput = Input.GetAxis("MyVertical");
        steeringInput = Input.GetAxis("MyHorizontal");

        slipAngle = Vector3.Angle(transform.forward, playerRb.velocity - transform.forward);
        if (slipAngle < 120f)
        {
            if (gasInput < 0)
            {
                brakeInput = Mathf.Abs(gasInput);
                gasInput = 0;
            }
            else
                brakeInput = 0;
        }
        else
            brakeInput = 0;
    }
    private void ApplyMotor()
    {
        backWheelsColliders[0].motorTorque = motorPower * gasInput;
        backWheelsColliders[1].motorTorque = motorPower * gasInput;
    }
    private void ApplyBrake()
    {
        foreach (var wheel in frontWheelsColliders)
            wheel.brakeTorque = brakeInput * 0.7f;
        foreach (var wheel in backWheelsColliders)
            wheel.brakeTorque = brakeInput * 0.3f;
    }
    private void ApplySteering()
    {
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        frontWheelsColliders[0].steerAngle = steeringAngle;
        frontWheelsColliders[1].steerAngle = steeringAngle;
    }
    private void ApplyWheelUpdate()
    {
        UpdateWheel(allWheelsColliders[0], allWheelsMeshes[0]);
        UpdateWheel(allWheelsColliders[1], allWheelsMeshes[1]);
        UpdateWheel(allWheelsColliders[2], allWheelsMeshes[2]);
        UpdateWheel(allWheelsColliders[3], allWheelsMeshes[3]);
    }
    private void UpdateWheel(WheelCollider collider, MeshRenderer mesh)
    {
        Vector3 position; Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        mesh.transform.position = position; mesh.transform.rotation = rotation;
    }
}