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
        if(!WaveManager.Instance.waveActive)
        {
            if (WaveManager.Instance.GetWaveSurvived())
            {
                ExitState();
                GameManager.Instance.waveNum++;
                GameData.Instance.UpdateOnceAfterEachWave();
                GameManager.Instance.TransitionToPhase(new SelectBuilding());
            }
            else
            {
                ExitState();
                GameManager.Instance.TransitionToPhase(new DefeatScreen());
            }
        }
    }
    public void ExitState()
    {

    }
}
