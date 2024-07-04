using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveNumUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void OnEnable()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetWaveNum(int waveNum)
    {
        text.text = "Wave " + (waveNum + 1).ToString();
    }
}
