using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_being_pushed : MonoBehaviour
{
    public Rigidbody2D rb;
    void Update()
    {
        if (Player.moveSpeed == 5)
        {
            if(Player.MoveX == true && Player.MoveY == false) //���η� �о�����
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.freezeRotation = true;
            }

            else if(Player.MoveY == true && Player.MoveX == false) //���η� �о�����
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
}
