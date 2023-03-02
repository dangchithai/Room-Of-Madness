using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Asset/Skills/Charge")]
public class ChargeSkill : SkillScriptableObject
{
    public override bool CanUseSkill(Enemy enemy, Player player)
    {
        if (UseWhenStart)
        {
            UseWhenStart = false;
            return true;
        }

        return !IsActivating && UseTime + Cooldown < Time.time;
    }

    public override void UseSkill(Enemy enemy, Player player)
    {
        base.UseSkill(enemy, player);
        enemy.StartCoroutine(Charge(enemy, player));
    }

    IEnumerator Charge(Enemy enemy, Player player)
    {
        enemy.Mover.StopCharging = false;
        enemy.Mover.StopMoving = true;
        float randomWait = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(randomWait);
        var destFacing = (player.transform.position - enemy.transform.position).normalized;
        if (destFacing.x > 0)
        {
            enemy.transform.localScale = Vector3.one;
        }
        else
        {
            enemy.transform.localScale = new Vector3(-1, 1, 1);
        }
        int tmpDmg = enemy.DmgCollide;
        enemy.DmgCollide = this.dmgAmount;
        float tmpXSpeed = enemy.Mover.xSpeed;
        float tmpYSpeed = enemy.Mover.ySpeed;
        enemy.Mover.xSpeed = 5;
        enemy.Mover.ySpeed = 5;
        enemy.Mover.ChargeDirection = destFacing;
        enemy.Mover.CurrentState = Mover.State.Charging;
        yield return new WaitForSeconds(0.8f);
        enemy.DmgCollide = tmpDmg;
        enemy.Mover.xSpeed = tmpXSpeed;
        enemy.Mover.ySpeed = tmpYSpeed;
        UseTime = Time.time;
        IsActivating = false;
        enemy.Mover.StopMoving = false;
        enemy.Mover.CurrentState = Mover.State.Moving;
    }
}
