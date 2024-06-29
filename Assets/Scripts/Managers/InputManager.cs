using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    private InputManager() { }
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("CameraManager");
                    instance = obj.AddComponent<InputManager>();
                }
            }
            return instance;
        }
    }

    public GameObject hoverCell { get; set; }
    private Vector3 lastMousePos;

    void OnEnable()
    {
        lastMousePos = Input.mousePosition;
    }

    void Update()
    {
        if (lastMousePos != Input.mousePosition)
        {
            hoverCell = HoveringOverCell();
            lastMousePos = Input.mousePosition;
        }
    }

    private GameObject HoveringOverCell()
    {
        Ray ray = CameraManager.Instance.GetCam().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        int layerMask = LayerMask.GetMask("Cell");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            GameObject hitGameObject = hit.collider.gameObject;
            IHoverable hoverable = hitGameObject.GetComponent<IHoverable>();
            if (hoverable != null)
            {
                return hitGameObject;
            }
        }
        return null;
    }
}
