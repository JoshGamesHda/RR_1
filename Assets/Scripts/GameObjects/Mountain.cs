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
        hp = GameData.INIT_HP;

    }

    void Update()
    {
        
    }

    public void DamageMountain(float dmg)
    {
        hp -= dmg;
        hpText.text = hp.ToString();
    }

    public bool IsAlive()
    {
        if (hp > 0) return true;
        return false;
    }
    public void ResetHP()
    {
        hp = GameData.INIT_HP;
        hpText.text = hp.ToString();
    }
}
