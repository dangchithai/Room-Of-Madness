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
                var hitY = Physics2D.BoxCast(
                                    target.transform.position, 
                                    target.GetComponent<BoxCollider2D>().size, 
                                    0, 
                                    new Vector2(0, direction.y), 
                                    Mathf.Abs(direction.y * Time.deltaTime / overTime * length), 
                                    target.GetComponent<Mover>().BlockingLayer);

                var hitX = Physics2D.BoxCast(
                                    target.transform.position,
                                    target.GetComponent<BoxCollider2D>().size,
                                    0,
                                    new Vector2(direction.x, 0),
                                    Mathf.Abs(direction.x * Time.deltaTime / overTime * length),
                                    target.GetComponent<Mover>().BlockingLayer);

                if (timeLeft > Time.deltaTime)
                {
                    if (hitY.collider == null)
                    {
                        target.transform.Translate(new Vector3(0, direction.y * Time.deltaTime / overTime * length, 0));
                    }

                    if (hitX.collider == null)
                    {
                        target.transform.Translate(new Vector3(direction.x * Time.deltaTime / overTime * length, 0, 0));
                    }

                    ////target.transform.Translate(direction * Time.deltaTime / overTime * length);
                }
                else
                {
                    if (hitY.collider == null)
                    {
                        target.transform.Translate(new Vector3(0, direction.y * timeLeft / overTime * length, 0));
                    }

                    if (hitX.collider == null)
                    {
                        target.transform.Translate(new Vector3(direction.x * timeLeft / overTime * length, 0, 0));
                    }
                    ////target.transform.Translate(direction * timeLeft / overTime * length);
                }

                timeLeft -= Time.deltaTime;
            }

            yield return null;
        }
    }
}
