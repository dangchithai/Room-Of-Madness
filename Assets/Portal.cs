using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    [SerializeField] Transform[] portalPoints;

    protected override void OnCollide(Collider2D hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            int randTransform = Random.Range(0, portalPoints.Length);
            Transform teleportTransform = portalPoints[randTransform];
            hit.gameObject.transform.position = teleportTransform.position;
        }
    }
}
