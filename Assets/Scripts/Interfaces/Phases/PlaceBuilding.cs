using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuilding : IPhase
{
    public void EnterState()
    {
        if (BuildingManager.Instance.activeBuilding != null)
        {
            PlacementManager.Instance.inactive = false;
            UIManager.Instance.ShowDeleteBuildingButton();
        }
        
    }
    public void UpdateState()
    {
        if (PlacementManager.Instance.inactive)
        {
            GameManager.Instance.TransitionToPhase(new Preparation());
            ExitState();
        }
    }
    public void ExitState()
    {
        UIManager.Instance.HideDeleteBuildingButton();
    }
}
