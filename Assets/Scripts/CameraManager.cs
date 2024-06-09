using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
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

    [SerializeField] private GameObject camParent;
    [SerializeField] private Camera cam;

    readonly private float
        rotationSpeedX = 500f,
        rotationSpeedZ = 400f;

    private float
        yaw = 0f,
        pitch = 45f;

    void Start()
    {
        camParent.transform.rotation = Quaternion.Euler(45f, 0, 0);
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeedX * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * -rotationSpeedZ * Time.deltaTime;

            yaw += mouseX;
            pitch += mouseY;

            pitch = Mathf.Clamp(pitch, 30f, 89f);

            camParent.transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        }
    }

    public Camera GetCam()
    {
        return cam;
    }
}