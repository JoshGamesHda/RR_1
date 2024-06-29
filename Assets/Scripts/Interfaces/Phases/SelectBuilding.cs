using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBuilding : IPhase
{
    bool choiceMade;
    public void EnterState()
    {
        CameraManager.Instance.cameraMovementActive = false;

        foreach (GameObject button in UIManager.Instance.buttons)
        {
            button.GetComponent<Button>().onClick.AddListener(ChoiceMade);
        }

        choiceMade = false;
        UIManager.Instance.RandomizeButtons();

        GameManager.Instance.CreateWave();
        List<SpawningArea> areas = GameManager.Instance.curWave.areas;

        for (int i = 0; i < areas.Count; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = areas[i].pos;
            sphere.GetComponent<Renderer>().material.color = Color.red;

            GameManager.Instance.indicators.Add(sphere);
        }
    }
    public void UpdateState()
    {
        if (Input.GetKey(Constants.KEY_HIDE_SHOP))
        {
            UIManager.Instance.HideButtons();
            CameraManager.Instance.cameraMovementActive = true;
        }
        if (!Input.GetKey(Constants.KEY_HIDE_SHOP) && !UIManager.Instance.buttonsShown)
        {
            CameraManager.Instance.cameraMovementActive = false;
            UIManager.Instance.ShowButtons();
        }

        if (choiceMade)
        {
            ExitState();
            GameManager.Instance.TransitionToPhase(new PlaceBuilding());
        }
    }
    public void ExitState()
    {
        UIManager.Instance.HideButtons();
        CameraManager.Instance.cameraMovementActive = true;
    }

    public void ChoiceMade()
    {
        choiceMade = true;
    }
}
