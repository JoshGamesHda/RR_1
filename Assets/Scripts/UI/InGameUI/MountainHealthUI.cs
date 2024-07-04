using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MountainHealthUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void OnEnable()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetMountainHealth(int health)
    {
        text.text = "Health " + health.ToString();
    }
}
