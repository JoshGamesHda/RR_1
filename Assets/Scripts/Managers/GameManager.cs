using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public struct Block
{
    public Vector2Int pos { get; set; }
    public Building towerType { get; set; }
    public Cell cell { get; set; }
    public Block(int posX, int posY, Building towerType_)
    {
        pos = new Vector2Int(posX, posY);
        towerType = towerType_;
        cell = null;
    }
}

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    private GameManager() { }
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("CameraManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    private IPhase curPhase;
    [SerializeField] private WaveFactory waveFactory;

    public int waveNum { get; set; }
    public Wave curWave { get; set; }

    // Kinda temporary until I find a better solution:
    public GameObject mountain;

    // Very temporary:
    [SerializeField] private GameObject indicator;
    private const float weewoo = 0.2f;
    public List<GameObject> indicators { get; set; }

    [SerializeField] private InfoBox infoBox;

    void OnEnable()
    {
        mountain.GetComponent<Mountain>().invulnerable = false;
        mountain.GetComponent<Mountain>().ResetHP();

        curPhase = new StartGame();
        curPhase.EnterState();

        waveNum = 0;

        indicators = new();
    }
    void Update()
    {
        curPhase.UpdateState();

        if(indicators.Count > 0)
        {
            foreach(GameObject indicator in indicators)
            {
                Transform modelTransform = indicator.transform.GetChild(0);

                modelTransform.localPosition = new Vector3 (modelTransform.localPosition.x, weewoo * Mathf.Sin(Time.time * 2f) , modelTransform.localPosition.z);
            }
        }
    }

    public void TransitionToPhase(IPhase nextPhase)
    {
        curPhase = nextPhase;
        curPhase.EnterState();
        infoBox.UpdateInfoBox();
    }
    public void CreateWave()
    {
        curWave = waveFactory.CreateWave(waveNum);
    }

    public void CreateIndicator(Vector3 pos)
    {
        GameObject arrow = Instantiate(indicator);
        arrow.transform.position = pos;
        arrow.transform.LookAt(mountain.transform.position);

        indicators.Add(arrow);
    }
    public void ClearIndicators()
    {
        foreach(GameObject indicator in indicators)
        {
            Destroy(indicator);
        }
        // double tap things you dont understand
        indicators.Clear();
    }

    public void ResetGame()
    {
        GameSceneManager.Instance.ReloadScene();
    }

    public IPhase GetCurPhase()
    {
        return curPhase;
    }
}
