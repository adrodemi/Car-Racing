using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3[] mainCameraOffsets = new[] { new Vector3(0, 3, -6f), new Vector3(0, 0.5f, 0.5f) };
    private int mainCameraOffsetsIndex = 0;
    public KeyCode cameraSwitchKey;

    private void LateUpdate()
    {
        transform.position = player.transform.position + mainCameraOffsets[mainCameraOffsetsIndex];

        if (Input.GetKeyDown(cameraSwitchKey))
        {
            if (mainCameraOffsetsIndex < mainCameraOffsets.Length - 1)
                mainCameraOffsetsIndex++;
            else
                mainCameraOffsetsIndex = 0;
        }
    }
}