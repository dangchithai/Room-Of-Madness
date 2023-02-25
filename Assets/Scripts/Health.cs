using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int currentHealth, maxHealth;

    [SerializeField] protected float immuneDuration = 0.5f;
    private float currentImmuneTime;

    private bool isDead = false;

    public void GetHit(int dmg, GameObject sender)
    {
        if (isDead)
        {
            return;
        }

        if (sender.CompareTag(this.gameObject.tag))
        {
            return;
        }

        if (Time.time - currentImmuneTime < immuneDuration)
        {
            return;
        }

        currentImmuneTime = Time.time;

        currentHealth -= dmg;

        if (currentHealth > 0)
        {
            Debug.Log(this.name + " Health: " + this.currentHealth);
            StartCoroutine(nameof(GetHitAnimation));
        }
        else
        {
            isDead = true;
            Death();
            Debug.Log(this.name + " Dealth ");
        }
    }

    protected virtual void Death()
    {
        // Need implement death function in children
    }

    protected virtual IEnumerator GetHitAnimation()
    {
        // Need implement GetHit animation in children
        yield return null;
    }
}
