using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    protected bool isCollected;

    protected override void OnCollide(Collider2D hit)
    {
        if (hit.CompareTag("Actor"))
        {
            OnCollect(hit);
        }
    }

    protected virtual void OnCollect(Collider2D hit)
    {
        // To do implement collect logic on child
    }
}
