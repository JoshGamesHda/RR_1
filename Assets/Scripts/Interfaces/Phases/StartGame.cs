using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : IPhase
{
    public void EnterState()
    {
        Debug.Log("Game Loaded, hit Space to start");
    }
    public void UpdateState()
    {

        if (Input.GetKeyDown(Constants.KEY_BEGIN_WAVE))
        {
            ExitState();
            GameManager.Instance.TransitionToPhase(new SelectBuilding());
        }
    }

    public void ExitState()
    {

    }
}
