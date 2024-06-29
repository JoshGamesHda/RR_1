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
    public List<GameObject> indicators { get; set; }

    void OnEnable()
    {
        curPhase = new StartGame();
        curPhase.EnterState();

        waveNum = 0;

        indicators = new();
    }
    void Update()
    {
        curPhase.UpdateState();
    }

    public void TransitionToPhase(IPhase nextPhase)
    {
        curPhase = nextPhase;
        curPhase.EnterState();
    }
    public void CreateWave()
    {
        curWave = waveFactory.CreateWave(waveNum);
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
}
