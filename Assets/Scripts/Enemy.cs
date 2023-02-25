using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health
{
    [SerializeField] private int dmgCollide = 5;
    [SerializeField] private int lifeSteal = 0;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider;
    private Transform playerTransform;
    private Mover mover;

    bool collidingWithPlayer = false;
    private Collider2D[] hits = new Collider2D[10];
    [SerializeField] private ContactFilter2D contactFilter;

    // Start is called before the first frame update
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        playerTransform = FindObjectOfType<Player>().transform;
        mover = this.GetComponent<Mover>();
        boxCollider = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!collidingWithPlayer)
        {
            mover.UpdateMotor((playerTransform.position - this.transform.position).normalized);
        }

        collidingWithPlayer = false;

        //Check colliding with player
        boxCollider.OverlapCollider(contactFilter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            if (hits[i].gameObject.tag == "Player")
            {
                collidingWithPlayer = true;
                if (hits[i].gameObject.GetComponent<Health>().GetHit(dmgCollide, this.gameObject))
                {
                    this.LifeSteal(lifeSteal);
                }
            }

            hits[i] = null;
        }
    }

    protected override void Death()
    {
        if (GameManager.Instance.ActiveRoom != null)
        {
            GameManager.Instance.ActiveRoom.NumberOfMonsterAlive--;
        }

        Destroy(this.gameObject);
    }

    protected override IEnumerator GetHitAnimation()
    {
        Color baseColor = sprite.color;
        sprite.color = Color.red;
        yield return new WaitForSeconds(immuneDuration);
        sprite.color = baseColor;
    }
}
