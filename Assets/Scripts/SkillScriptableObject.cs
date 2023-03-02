using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScriptableObject : ScriptableObject
{
    public float Cooldown = 10f;
    public int dmgAmount = 8;

    public bool IsActivating;
    public bool UseWhenStart;

    protected float UseTime;

    public virtual void UseSkill(Enemy enemy, Player player)
    {
        IsActivating = true;
    }

    public virtual bool CanUseSkill(Enemy enemy, Player player)
    {
        return true;
    }
}
