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

    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private float rotationAcceleration;
    [SerializeField] private float rotationDeceleration;
    private float rotationSpeed;

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
        camParent.transform.position += vel * Time.deltaTime;
    }

    private void Rotation()
    {
        // Get the mouse X input (left/right movement)
        float mouseX = Input.GetAxis("Mouse X");

        if (Input.GetKey(Constants.KEY_CAMERA_ROTATE))
        {
            if (Mathf.Abs(mouseX) > 0.01f) // Detect small mouse movements
            {
                // Accelerate the rotation speed based on the input direction
                rotationSpeed += mouseX * rotationAcceleration * Time.deltaTime;

                // Clamp the rotation speed to the max rotation speed
                rotationSpeed = Mathf.Clamp(rotationSpeed, -maxRotationSpeed, maxRotationSpeed);
            }
        }

        // Decelerate the rotation speed when the mouse stops moving or key is not pressed
        if (Mathf.Abs(mouseX) <= 0.01f || !Input.GetKey(Constants.KEY_CAMERA_ROTATE))
        {
            if (rotationSpeed > 0)
            {
                rotationSpeed -= rotationDeceleration * Time.deltaTime;
                rotationSpeed = Mathf.Max(rotationSpeed, 0); // Prevent overshooting below 0
            }
            else if (rotationSpeed < 0)
            {
                rotationSpeed += rotationDeceleration * Time.deltaTime;
                rotationSpeed = Mathf.Min(rotationSpeed, 0); // Prevent overshooting above 0
            }
        }

        // Apply the rotation based on the current rotation speed
        yaw += rotationSpeed * Time.deltaTime;

        // Update the camParent's rotation
        camParent.transform.rotation = Quaternion.Euler(45, yaw, 0);
    }
}
