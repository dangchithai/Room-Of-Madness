using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D ContactFilter;
    protected BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        boxCollider.OverlapCollider(ContactFilter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            this.OnCollide(hits[i]);
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D hit)
    {
        // To do implement on child class
    }
}
