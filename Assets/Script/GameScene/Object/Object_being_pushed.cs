using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBeingPushed : MonoBehaviour
{
    public Rigidbody2D rb;

    void Update()
    {
        if (Player.moveSpeed == 5)
        {
            if (Player.MoveX == true && Player.MoveY == false) //���η� �о��� ��
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.freezeRotation = true;
            }

            else if (Player.MoveY == true && Player.MoveX == false) //���η� �о��� ��
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.freezeRotation = true;
            }
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            return;

        Vector2 collisionPoint = collision.contacts[0].point;
        Vector2 objectPosition = transform.position;
        Vector2 direction = collisionPoint - objectPosition;
        direction.Normalize();

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) //X�� �浹
        {
            if (direction.x > 0)
            {
                Debug.Log("�����ʿ��� �浹");
            }

            else
            {
                Debug.Log("���ʿ��� �浹");
            }
        }

        else
        {
            if (direction.y > 0) //Y�� �浹
            {
                Debug.Log("���ʿ��� �浹");
            }

            else
            {
                Debug.Log("�Ʒ��ʿ��� �浹");
            }
        }
    }
}
