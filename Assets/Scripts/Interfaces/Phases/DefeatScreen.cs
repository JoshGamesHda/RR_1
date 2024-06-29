using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatScreen : IPhase
{
    public void EnterState()
    {
        Debug.Log("The Candy Kingdom is forever doomed, you did this");
        Debug.Log("Press Space to start over");
    }
    public void UpdateState()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExitState();
            GameManager.Instance.TransitionToPhase(new StartGame());
        }

    }
    public void ExitState()
    {
        WaveManager.Instance.KillAllEnemies();
        GameManager.Instance.mountain.GetComponent<Mountain>().ResetHP();
        Debug.Log("Starting New Game");
    }
}
