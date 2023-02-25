using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    [SerializeField] int dmgAmount = 5;

    protected override void OnCollide(Collider2D hit)
    {
        Health health;
        health = hit.GetComponent<Health>();
        if (health != null)
        {
            health.GetHit(dmgAmount, transform.root.gameObject);
        }
    }
}
