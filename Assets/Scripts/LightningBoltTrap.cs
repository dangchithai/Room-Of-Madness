using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltTrap : Collidable
{
    private float ySpeed = 0.015f;
    private float minY = -0.579f;
    private float maxY = 0.790f;
    private bool upDirection = true;
    private bool canDealDmg = true;
    private LineRenderer lineRender;
    private float changeStateDuration;
    private float currentTime;

    protected override void Start()
    {
        base.Start();
        lineRender = this.GetComponent<LineRenderer>();
        currentTime = Time.time;
        changeStateDuration = Random.Range(0.4f, 1.5f);
    }

    private void FixedUpdate()
    {
        if (upDirection)
        {
            transform.Translate(new Vector3(0, ySpeed, 0));
        }
        else
        {
            transform.Translate(new Vector3(0, -ySpeed, 0));
        }

        if (this.transform.localPosition.y > maxY)
        {
            upDirection = false;
        }
        else if (this.transform.localPosition.y < minY)
        {
            upDirection = true;
        }

        if (Time.time - currentTime > changeStateDuration)
        {
            currentTime = Time.time;
            changeStateDuration = Random.Range(0.4f, 1.5f);
            lineRender.enabled = !lineRender.enabled;
            canDealDmg = lineRender.enabled;
        }
    }

    protected override void OnCollide(Collider2D hit)
    {
        if (hit.gameObject.tag == "Player" && canDealDmg)
        {
            Health health = hit.gameObject.GetComponent<Health>();
            health.GetHit(20, this.gameObject);
        }
    }


}
