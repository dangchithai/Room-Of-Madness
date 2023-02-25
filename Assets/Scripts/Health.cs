using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int currentHealth, maxHealth;

    [SerializeField] protected float immuneDuration = 0.5f;
    private float currentImmuneTime;

    public bool IsDead = false;

    public bool GetHit(int dmg, GameObject sender)
    {
        if (IsDead)
        {
            return false;
        }

        if (sender.CompareTag(this.gameObject.tag))
        {
            return false;
        }

        if (Time.time - currentImmuneTime < immuneDuration)
        {
            return false;
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
            IsDead = true;
            Death();
            Debug.Log(this.name + " Dealth ");
        }

        return true;
    }

    public void LifeSteal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
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
