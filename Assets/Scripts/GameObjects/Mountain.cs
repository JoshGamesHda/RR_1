using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mountain : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    private float hp;

    void Start()
    {
        hp = GameData.Instance.initialMountainHP;

    }

    void Update()
    {
        hpText.text = hp.ToString();
    }

    public void DamageMountain(float dmg)
    {
        hp -= dmg;
    }

    public bool IsAlive()
    {
        if (hp > 0) return true;
        return false;
    }
    public void ResetHP()
    {
        hp = GameData.Instance.initialMountainHP;
    }
}
