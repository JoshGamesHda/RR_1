using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Singleton
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
    #endregion
    public GameObject hoverCell { get; set; }
    private Vector3 lastMousePos;

    public bool ignoreNextSelect { private get; set; }

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

        if (hoverCell != null && hoverCell.GetComponent<Cell>().buildingOnCell != null && !hoverCell.GetComponent<Cell>().buildingOnCell.isSupport)
        {
            AttackTower a = (AttackTower)hoverCell.GetComponent<Cell>().buildingOnCell;
            a.showRangeIndication = true;
        }

        if ( !ignoreNextSelect && hoverCell != null && hoverCell.GetComponent<Cell>().buildingOnCell != null &&  Input.GetKeyUp(Constants.KEY_PLACEMENT) && hoverCell.GetComponent<Cell>().buildingOnCell.placed)
        {
            BuildingManager.Instance.SelectBuilding(hoverCell.GetComponent<Cell>().buildingOnCell.gameObject);
        }
        if(ignoreNextSelect && Input.GetKeyUp(Constants.KEY_PLACEMENT)) ignoreNextSelect = false;

        if (hoverCell == null && Input.GetKeyUp(Constants.KEY_PLACEMENT) || hoverCell != null && hoverCell.GetComponent<Cell>().buildingOnCell == null && Input.GetKeyUp(Constants.KEY_PLACEMENT))
        {
            BuildingManager.Instance.UnselectBuilding();
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
