using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Health
{
    private WeaponHolder weaponHolder;
    private Mover mover;
    private SpriteRenderer sprite;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        this.anim = this.GetComponent<Animator>();
        this.weaponHolder = this.GetComponentInChildren<WeaponHolder>();
        this.mover = this.GetComponent<Mover>();
        this.sprite = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weaponHolder.Attack();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        mover.UpdateMotor(new Vector3(x, y, 0));
    }

    protected override IEnumerator GetHitAnimation()
    {
        this.anim.SetTrigger("Hit");
        yield return null;
    }
}
