using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Vector3 moveDelta;
    BoxCollider2D boxCollider;
    RaycastHit2D hit;
    public LayerMask BlockingLayer;

    [SerializeField] private float xSpeed = 0.8f;
    [SerializeField] private float ySpeed = 1;

    private void Start()
    {
        this.boxCollider = this.GetComponent<BoxCollider2D>();
    }

    public void UpdateMotor(Vector3 input)
    {
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
}
