using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendBase : IPhase
{
    public void EnterState()
    {
        GameManager.Instance.curWave.SendWave();

        GameManager.Instance.ClearIndicators();
    }
    public void UpdateState()
    {
        if (Input.GetKeyDown(Constants.KEY_PLACEMENT) && InputManager.Instance.hoverCell == null) UIManager.Instance.HideStatDisplay();

        if(!WaveManager.Instance.waveActive)
        {
            if (WaveManager.Instance.GetWaveSurvived())
            {
                ExitState();
                GameManager.Instance.waveNum++;
                UIManager.Instance.UpdateWaveNumDisplay(GameManager.Instance.waveNum);
                GameData.Instance.UpdateOnceAfterEachWave();
                GameManager.Instance.TransitionToPhase(new SelectBuilding());
            }
            else
            {
                ExitState();
                GameManager.Instance.TransitionToPhase(new DefeatScreen());
            }
        }

        if (Input.GetKeyDown(KeyCode.L)) GameManager.Instance.mountain.GetComponent<Mountain>().KillMountain();
    }
    public void ExitState()
    {

    }
}
