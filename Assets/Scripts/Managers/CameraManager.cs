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

    readonly private float
        rotationSpeedX = 2000f,
        rotationSpeedZ = 1000f;

    private float
        yaw = 0f,
        pitch = 45f;


    public bool cameraMovementActive { get; set; }
    void OnEnable()
    {
        camParent.transform.rotation = Quaternion.Euler(45f, 0, 0);
        cameraMovementActive = true;
    }

    void Update()
    {
        if (Input.GetKey(Constants.KEY_MOVE_CAMERA) && cameraMovementActive)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeedX * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * -rotationSpeedZ * Time.deltaTime;

            yaw += mouseX;
            pitch += mouseY;

            pitch = Mathf.Clamp(pitch, 19f, 89f);

            camParent.transform.rotation = Quaternion.Euler(pitch, yaw, 0);
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
}
