using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Preparation : IPhase
{
    private float holdDownForPickUp;
    private float timeElapsed;
    private Cell initialHoverCell;

    public void EnterState()
    {
        holdDownForPickUp = 0.5f;
        timeElapsed = 0f;
        initialHoverCell = null;
    }

    public void UpdateState()
    {
        if (Input.GetMouseButtonDown(0) && InputManager.Instance.hoverCell != null)
        {
            initialHoverCell = InputManager.Instance.hoverCell.GetComponent<Cell>();
            timeElapsed = 0f;
        }

        if (Input.GetMouseButton(0) && InputManager.Instance.hoverCell != null)
        {
            if (InputManager.Instance.hoverCell.GetComponent<Cell>() == initialHoverCell)
            {
                timeElapsed += Time.deltaTime;
            }
            else
            {
                timeElapsed = 0f;
                initialHoverCell = InputManager.Instance.hoverCell.GetComponent<Cell>();
            }
        }

        if (Input.GetMouseButtonUp(0) && timeElapsed >= holdDownForPickUp && initialHoverCell != null && initialHoverCell.buildingOnCell != null)
        {
            BuildingManager.Instance.PickUpFrom(initialHoverCell);
            ExitState();
            GameManager.Instance.TransitionToPhase(new PlaceBuilding());
        }

        if (Input.GetKeyDown(Constants.KEY_BEGIN_WAVE))
        {
            ExitState();
            GameManager.Instance.TransitionToPhase(new DefendBase());
        }
    }

    public void ExitState()
    {

    }
}
