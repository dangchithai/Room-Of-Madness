using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public enum State
    {
        Moving,
        Charging
    }

    public State CurrentState = State.Moving;
    Vector3 moveDelta;
    BoxCollider2D boxCollider;
    RaycastHit2D hit;
    public LayerMask BlockingLayer;

    public float xSpeed = 0.8f;
    public float ySpeed = 1;
    public Vector3 ChargeDirection;
    public bool StopMoving = false;
    public bool StopCharging = false;

    private void Start()
    {
        this.boxCollider = this.GetComponent<BoxCollider2D>();
    }

    public void UpdateMotor(Vector3 input)
    {
        if (CurrentState == State.Charging || StopMoving) return;

        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, input.z);

        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), BlockingLayer);

        if (hit.collider == null)
        {
            transform.Translate(new Vector3(0, moveDelta.y * Time.deltaTime, 0));
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), BlockingLayer);

        if (hit.collider == null)
        {
            transform.Translate(new Vector3(moveDelta.x * Time.deltaTime, 0, 0));
        }
    }

    public void Charge()
    {
        if (CurrentState != State.Charging || StopCharging) { return; }

        moveDelta = new Vector3(ChargeDirection.x * xSpeed, ChargeDirection.y * ySpeed, ChargeDirection.z);

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), BlockingLayer);

        if (hit.collider == null)
        {
            transform.Translate(new Vector3(0, moveDelta.y * Time.deltaTime, 0));
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), BlockingLayer);

        if (hit.collider == null)
        {
            transform.Translate(new Vector3(moveDelta.x * Time.deltaTime, 0, 0));
        }
    }
}
