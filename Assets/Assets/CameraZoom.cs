using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    public Camera cam;
    public float zoomSpeed = 2f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    float scrollData;
    scrollData = Input.GetAxis("Mouse ScrollWheel");
    float Zoom = cam.orthographicSize - scrollData * zoomSpeed;
    cam.orthographicSize = Mathf.Clamp(Zoom, minZoom, maxZoom);

    }
}
