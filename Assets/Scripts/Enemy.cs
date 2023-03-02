using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health
{
    public int DmgCollide = 5;
    [SerializeField] private int lifeSteal = 0;
    [SerializeField] private float knockbackForce = 3f;
    [SerializeField] private SkillScriptableObject[] skills;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider;
    private Player player;
    private Color baseColor;
    public Mover Mover;

    bool collidingWithPlayer = false;
    private Collider2D[] hits = new Collider2D[5];
    [SerializeField] private ContactFilter2D contactFilter;
    private List<SkillScriptableObject> ourSkills;

    // Start is called before the first frame update
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>();
        Mover = this.GetComponent<Mover>();
        boxCollider = this.GetComponent<BoxCollider2D>();

        ourSkills = new List<SkillScriptableObject>();
        for (int i = 0; i < skills.Length; i++)
        {
            var newOurSkill = Instantiate(skills[i]);
            ourSkills.Add(newOurSkill);
        }

        baseColor = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ourSkills.Count; i++)
        {
            if (ourSkills[i].CanUseSkill(this, player))
            {
                ourSkills[i].UseSkill(this, player);
            }
        }

        if (!collidingWithPlayer)
        {
            if (Mover.CurrentState == Mover.State.Moving)
            {
                Mover.UpdateMotor((player.transform.position - this.transform.position).normalized);
            }
            else
            {
                Mover.Charge();
            }
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
                if (hits[i].gameObject.GetComponent<Health>().GetHit(DmgCollide, this.gameObject))
                {
                    this.LifeSteal(lifeSteal);

                    if (Mover.CurrentState == Mover.State.Charging)
                    {
                        Vector3 direction = hits[i].transform.position - this.transform.position;
                        this.GetComponent<Knockback>().ApplyKnockback(hits[i].gameObject, direction, knockbackForce, 0.3f);
                        Mover.StopCharging = true;
                    }
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
        sprite.color = Color.red;
        yield return new WaitForSeconds(immuneDuration - 0.1f);
        sprite.color = baseColor;
    }
}
