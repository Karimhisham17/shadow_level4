using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // The object for the camera to follow
    public float cameraSpeed = 5f;  // The speed at which the camera follows
    public float minX, maxX, minY, maxY; // Limits for camera movement

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            float targetX = Mathf.Clamp(target.position.x, minX, maxX);
            float targetY = Mathf.Clamp(target.position.y, minY, maxY);
            Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
        }
    }
}
