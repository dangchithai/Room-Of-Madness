using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Health
{
    private WeaponHolder weaponHolder;
    private Mover mover;

    // Start is called before the first frame update
    void Start()
    {
        this.weaponHolder = this.GetComponentInChildren<WeaponHolder>();
        this.mover = this.GetComponent<Mover>();
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
}
