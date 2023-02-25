using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Motor : MonoBehaviour
{

    public Transform LookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = LookAt.position.x - this.transform.position.x;

        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < LookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        float deltaY = LookAt.position.y - this.transform.position.y;

        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < LookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
