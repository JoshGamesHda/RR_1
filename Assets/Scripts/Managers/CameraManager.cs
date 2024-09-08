using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Singleton
    private static CameraManager instance;
    private CameraManager() { }
    public static CameraManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CameraManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("CameraManager");
                    instance = obj.AddComponent<CameraManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    [SerializeField] private GameObject camParent;
    [SerializeField] private Camera cam;

    [Header("Values")]

    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float moveAcceleration;
    [SerializeField] private float deceleration;
    private Vector3 vel = Vector3.zero;

    [SerializeField] private float boundsDistance;

    [Header("Horizontal Rotation")]
    [SerializeField] private float maxHorRotationSpeed;
    [SerializeField] private float horRotationAcceleration;
    [SerializeField] private float horRotationDeceleration;
    private float horRotationSpeed;

    [Header("Vertical Rotation")]
    [SerializeField] private float maxVertRotationSpeed;
    [SerializeField] private float vertRotationAcceleration;
    [SerializeField] private float vertRotationDeceleration;
    private float vertRotationSpeed;

    [Header("")]
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 60f;

    private float pitch = 45f;
    private float yaw = 0f;
    
    public bool cameraMovementActive { get; set; }
    void OnEnable()
    {
        camParent.transform.rotation = Quaternion.Euler(45f, 0, 0);
        cameraMovementActive = true;
    }

    void Update()
    {
        if (cameraMovementActive)
        {
            Movement();

            Rotation();
        }
    }

    public Camera GetCam()
    {
        return cam;
    }
    public Vector3 GetCamPos()
    {
        return camParent.transform.position + cam.gameObject.transform.position;
    }

    private void Movement()
    {
        Vector3 dir = Vector3.zero;

        // Get the input direction based on WASD keys
        if (Input.GetKey(Constants.KEY_CAMERA_FORWARD))
        {
            dir += new Vector3(camParent.transform.forward.x, 0, camParent.transform.forward.z);
        }
        if (Input.GetKey(Constants.KEY_CAMERA_BACKWARD))
        {
            dir -= new Vector3(camParent.transform.forward.x, 0, camParent.transform.forward.z);
        }
        if (Input.GetKey(Constants.KEY_CAMERA_RIGHT))
        {
            dir += new Vector3(camParent.transform.right.x, 0, camParent.transform.right.z);
        }
        if (Input.GetKey(Constants.KEY_CAMERA_LEFT))
        {
            dir -= new Vector3(camParent.transform.right.x, 0, camParent.transform.right.z);
        }

        // Normalize the direction vector to avoid faster diagonal movement
        dir = dir.normalized;

        if (dir != Vector3.zero)
        {
            // If changing direction, apply stronger deceleration
            if (Vector3.Dot(vel.normalized, dir) < 0)
            {
                vel = Vector3.Lerp(vel, Vector3.zero, deceleration * Time.deltaTime);
            }

            // Apply acceleration to velocity based on input direction
            vel += dir * moveAcceleration * Time.deltaTime;
            vel = Vector3.ClampMagnitude(vel, maxMoveSpeed);
        }
        else
        {
            // Decelerate when no input is detected
            vel = Vector3.Lerp(vel, Vector3.zero, deceleration * Time.deltaTime);
        }

        // Move the camera parent
        if((camParent.transform.position + vel * Time.deltaTime).magnitude < boundsDistance) 
        camParent.transform.position += vel * Time.deltaTime;
    }

    private void Rotation()
    {
        // Get the mouse X (horizontal) and Y (vertical) input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Horizontal rotation (yaw)
        if (Input.GetKey(Constants.KEY_CAMERA_ROTATE))
        {
            if (Mathf.Abs(mouseX) > 0.01f) // Detect small mouse movements
            {
                // Accelerate the horizontal rotation speed based on input
                horRotationSpeed += mouseX * horRotationAcceleration * Time.deltaTime;

                // Clamp the horizontal rotation speed to max value
                horRotationSpeed = Mathf.Clamp(horRotationSpeed, -maxHorRotationSpeed, maxHorRotationSpeed);
            }
        }

        // Decelerate horizontal rotation when there's no input or key is not pressed
        if (Mathf.Abs(mouseX) <= 0.01f || !Input.GetKey(Constants.KEY_CAMERA_ROTATE))
        {
            if (horRotationSpeed > 0)
            {
                horRotationSpeed -= horRotationDeceleration * Time.deltaTime;
                horRotationSpeed = Mathf.Max(horRotationSpeed, 0); // Prevent overshooting below 0
            }
            else if (horRotationSpeed < 0)
            {
                horRotationSpeed += horRotationDeceleration * Time.deltaTime;
                horRotationSpeed = Mathf.Min(horRotationSpeed, 0); // Prevent overshooting above 0
            }
        }

        // Apply the horizontal rotation (yaw)
        yaw += horRotationSpeed * Time.deltaTime;

        // Vertical rotation (pitch)
        if (Input.GetKey(Constants.KEY_CAMERA_ROTATE))
        {
            if (Mathf.Abs(mouseY) > 0.01f) // Detect small mouse movements
            {
                // Accelerate the vertical rotation speed based on input
                vertRotationSpeed += -mouseY * vertRotationAcceleration * Time.deltaTime; // Invert Y for natural movement

                // Clamp the vertical rotation speed to max value
                vertRotationSpeed = Mathf.Clamp(vertRotationSpeed, -maxVertRotationSpeed, maxVertRotationSpeed);
            }
        }

        // Decelerate vertical rotation when there's no input or key is not pressed
        if (Mathf.Abs(mouseY) <= 0.01f || !Input.GetKey(Constants.KEY_CAMERA_ROTATE))
        {
            if (vertRotationSpeed > 0)
            {
                vertRotationSpeed -= vertRotationDeceleration * Time.deltaTime;
                vertRotationSpeed = Mathf.Max(vertRotationSpeed, 0); // Prevent overshooting below 0
            }
            else if (vertRotationSpeed < 0)
            {
                vertRotationSpeed += vertRotationDeceleration * Time.deltaTime;
                vertRotationSpeed = Mathf.Min(vertRotationSpeed, 0); // Prevent overshooting above 0
            }
        }

        // Apply the vertical rotation (pitch) and enforce bounds
        pitch += vertRotationSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch); // Limit pitch between bounds

        // Update the camParent's rotation using both yaw and pitch
        camParent.transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
}
