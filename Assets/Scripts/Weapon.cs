using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    [SerializeField] int dmgAmount = 5;
    [SerializeField] float knockbackForce = 1f;

    protected override void OnCollide(Collider2D hit)
    {
        Health health;
        health = hit.GetComponent<Health>();
        if (health != null)
        {
            if (health.GetHit(dmgAmount, transform.root.gameObject))
            {
                if (hit.gameObject.tag == "Enemy")
                {
                    Vector3 direction = hit.transform.position - this.transform.position;
                    this.gameObject.GetComponent<Knockback>()?.ApplyKnockback(hit.gameObject, direction, knockbackForce, 0.3f);
                }
            }
        }
    }
}
