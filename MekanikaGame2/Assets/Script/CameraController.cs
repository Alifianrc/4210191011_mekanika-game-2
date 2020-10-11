using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    [Range(1,10)]
    public float smoothFactor;
    public PlayerMovement thePlayer;
    
    private void FixedUpdate()
    {
        Follow();
        if (thePlayer.speed >= 0)
        {
            offset.x = 4;
        }
        else
        {
            offset.x = -4;
        }
        offset.y = 1f;
    }

    void Follow()
    {
        Vector3 targetPosition = playerTransform.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
