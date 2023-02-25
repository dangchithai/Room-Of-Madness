using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public void ApplyKnockback (GameObject target, Vector3 direction, float length, float overTime)
    {
        direction = direction.normalized;
        StartCoroutine(KnockbackCoroutine(target, direction, length, overTime));
    }

    private IEnumerator KnockbackCoroutine(GameObject target, Vector3 direction, float length, float overTime)
    {
        float timeLeft = overTime;
        while (timeLeft > 0)
        {
            if (!target.IsNullOrDestroyed())
            {
                if (timeLeft > Time.deltaTime)
                    target.transform.Translate(direction * Time.deltaTime / overTime * length);
                else
                    target.transform.Translate(direction * timeLeft / overTime * length);
                timeLeft -= Time.deltaTime;
            }

            yield return null;
        }
    }
}
