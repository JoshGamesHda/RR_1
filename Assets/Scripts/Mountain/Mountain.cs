using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mountain : MonoBehaviour
{
    private float hp;
    public bool invulnerable { get; set; }

    void Start()
    {
        hp = GameData.Instance.initialMountainHP;
        UIManager.Instance.UpdateMountainHealthDisplay((int)hp);
    }

    public void DamageMountain(float dmg)
    {
        if (!invulnerable)
        {
            hp -= dmg;
            UIManager.Instance.UpdateMountainHealthDisplay((int)hp);
        }
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

    public void KillMountain()
    {
        GameManager.Instance.mountain.GetComponent<Mountain>().invulnerable = true;
        hp = 0;
    }
}
