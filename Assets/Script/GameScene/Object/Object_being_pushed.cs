using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Being_Pushed : MonoBehaviour
{
    public Rigidbody2D rb;

    private bool FreezeX = false;
    private bool FreezeY = false;

    void Update()
    {
        if (Player.moveSpeed == 5)
        {
            if (Player.MoveX && !Player.MoveY && !FreezeX) // ���η� �о��� ��
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY; // Y�� ����
                rb.freezeRotation = true;
            }

            else if (Player.MoveY && !Player.MoveX && !FreezeY) // ���η� �о��� ��
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX; // X�� ����
                rb.freezeRotation = true;
            }

            else if (!FreezeX && !FreezeY)
            {
                rb.constraints = RigidbodyConstraints2D.None;
            }

            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) return;

        FreezeX = Mathf.Abs(collider.transform.position.x - transform.position.x) > Mathf.Abs(collider.transform.position.y - transform.position.y);
        FreezeY = !FreezeX;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) return;

        FreezeX = false;
        FreezeY = false;
    }
}