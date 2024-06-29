using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform cam;

    readonly private float zoomSpeed = 50f;
    readonly private float minDistanceToParent = 5f;
    readonly private float maxDistanceToParent = 20f;

    readonly private float rotationSpeedX = 2000f; 
    readonly private float rotationSpeedZ = 1000f;

    private float yaw = 0;
    private float pitch = 45f;

    //readonly private float moveSpeed = 500f;

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeedX * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeedZ * Time.deltaTime;

            yaw += mouseX;
            pitch -= mouseY;

            pitch = Mathf.Clamp(pitch, 10, 89f);
            
            transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        }
        HandleZoom();
        HandleMovement();
    }
    public void SetStandardPitch()
    {
        transform.rotation = Quaternion.Euler(45f, 0, 0);
    }

    private void HandleZoom()
    {
        Vector3 unitTowardParent = (transform.position - cam.position).normalized;
        float distanceToParent = (transform.position - cam.position).magnitude;

        float scrollInput = Input.mouseScrollDelta.y;

        float zoomAmount = scrollInput * zoomSpeed * Time.deltaTime * (distanceToParent/2);

        float newDistanceToParent = distanceToParent - zoomAmount;

        newDistanceToParent = Mathf.Clamp(newDistanceToParent, minDistanceToParent, maxDistanceToParent);

        cam.position = transform.position - unitTowardParent * newDistanceToParent;
    }

    private void HandleMovement()
    {
        //if(Input.GetMouseButton(2))
        //{
        //    float mouseX = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
        //    float mouseY = Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime;

        //    transform.position = new Vector3(transform.position.x - mouseX, 0, transform.position.z - mouseY);
        //}
    }
}
