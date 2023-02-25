using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private float timerAttackDelay = 0.5f;

    private Animator anim;
    private float currentAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponentInChildren<Animator>();
    }

    public void Attack()
    {
        if (Time.time - currentAttackTime < timerAttackDelay)
        {
            return;
        }

        currentAttackTime = Time.time;
        anim.SetTrigger("Attack");
    }
}
