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
        holdDownForPickUp = Constants.holdForPickUpLength;
        timeElapsed = 0f;
        initialHoverCell = null;
    }

    public void UpdateState()
    {
        if (Input.GetKeyDown(Constants.KEY_PLACEMENT) && InputManager.Instance.hoverCell != null && InputManager.Instance.hoverCell.GetComponent<Cell>().buildingOnCell != null)
        {
            initialHoverCell = InputManager.Instance.hoverCell.GetComponent<Cell>();
            timeElapsed = 0f;
        }

        if (Input.GetKey(Constants.KEY_PLACEMENT) && initialHoverCell != null)
        {
            if (InputManager.Instance.hoverCell != null && InputManager.Instance.hoverCell.GetComponent<Cell>() == initialHoverCell && initialHoverCell != null && initialHoverCell.buildingOnCell != null)
            {
                timeElapsed += Time.deltaTime;
                if (initialHoverCell.buildingOnCell != null)
                {
                    initialHoverCell.buildingOnCell.ShakeBuilding(timeElapsed / holdDownForPickUp);
                    if (timeElapsed >= holdDownForPickUp)
                    {
                        initialHoverCell.buildingOnCell.UnShakeBuilding();
                        BuildingManager.Instance.PickUpFrom(initialHoverCell);
                        ExitState();
                        GameManager.Instance.TransitionToPhase(new PlaceBuilding());
                    }
                }
            }  
        }
        if (Input.GetKeyUp(Constants.KEY_PLACEMENT) && timeElapsed <= holdDownForPickUp)
        {
            timeElapsed = 0f;
            if (initialHoverCell != null && initialHoverCell.buildingOnCell != null) initialHoverCell.buildingOnCell.UnShakeBuilding();
            initialHoverCell = null;
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
