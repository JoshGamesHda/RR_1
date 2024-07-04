using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mountain : MonoBehaviour
{
    private float hp;

    void Start()
    {
        hp = GameData.Instance.initialMountainHP;
        UIManager.Instance.UpdateMountainHealthDisplay((int)hp);
    }

    public void DamageMountain(float dmg)
    {
        hp -= dmg;
        UIManager.Instance.UpdateMountainHealthDisplay((int)hp);
    }

    public bool IsAlive()
    {
        if (hp > 0) return true;
        return false;
    }
    public void ResetHP()
    {
        hp = GameData.Instance.initialMountainHP;
        UIManager.Instance.UpdateMountainHealthDisplay((int)hp);
    }
}
