using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISupportEffect
{
    public void ApplyEffect(AttackTower tower);
}

public class EffectDamageUp : ISupportEffect
{
    public void ApplyEffect(AttackTower tower)
    {
        tower.damage *= GameData.DamageUp_Multiplier;
    }
}

public class EffectSpeedUp : ISupportEffect
{
    public void ApplyEffect(AttackTower tower)
    {
        tower.fireRate *= GameData.SpeedUp_Multiplier;
    }
}

public class EffectRangeUp : ISupportEffect
{
    public void ApplyEffect(AttackTower tower)
    {
        tower.range *= GameData.RangeUp_Multiplier;
    }
}