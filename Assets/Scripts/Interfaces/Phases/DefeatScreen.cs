using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatScreen : IPhase
{
    public void EnterState()
    {
        UIManager.Instance.ShowDefeatScreen();
        GameManager.Instance.mountain.GetComponent<Mountain>().KillMountain();
        GameManager.Instance.mountain.GetComponent<Mountain>().invulnerable = true;
    }
    public void UpdateState()
    {

    }
    public void ExitState()
    {
        Debug.Log("Starting New Game");
    }
}
