using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;

    public Vector3 pos;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        pos = transform.position;

        switch (GameManager.GameState)
        {
            case "Ʃ�丮��":
                if(player.position.y >= 47 && player.position.y <= 50)
                    pos.y = player.position.y;
                break;

            case "Ʃ�丮�� �ƾ�":
                pos.x = -51.9f;
                pos.y = 47f;
                break;

            case "��Ƽ��":
                pos.x = 60f;
                pos.y = player.position.y;
                break;

            case "���� #F":
                if (player.position.y >= -7.5f)
                {
                    pos.x = player.position.x;
                    pos.y = player.position.y;
                }
                    
                else
                {
                    pos.x = player.position.x;
                    pos.y = -12.5f;
                }
                break;

            case "��ȸ�� �Ա�":
                pos.x = 44f;
                pos.y = player.position.y;
                break;

            case "â��":
                pos.y = player.position.y;
                break;

            default:
                pos.x = player.position.x;
                pos.y = player.position.y;
                break;
        }

        transform.position = pos;
    }
}