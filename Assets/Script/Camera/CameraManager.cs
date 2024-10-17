using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Player player;

    public Vector3 pos;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        pos = transform.position;

        switch (GameManager.GameState)
        {
            case "Ʃ�丮��":
                if(player.transform.position.y >= 47 && player.transform.position.y <= 50)
                    pos.y = player.transform.position.y;
                break;

            case "Ʃ�丮�� �ƾ�":
                pos.x = -51.9f;
                pos.y = 47f;
                break;

            case "��Ƽ��":
                pos.x = 60f;
                pos.y = player.transform.position.y;
                break;

            case "���� #F":
                if (player.transform.position.y >= -7.5f)
                {
                    pos.x = player.transform.position.x;
                    pos.y = player.transform.position.y;
                }
                    
                else
                {
                    pos.x = player.transform.position.x;
                    pos.y = -12.5f;
                }
                break;

            case "��ȸ�� �Ա�":
                pos.x = 44f;
                pos.y = player.transform.position.y;
                break;

            case "��ȸ��":
                bool posX = (player.transform.position.x >= 58.3f && player.transform.position.x <= 73.8f);
                bool posY = (player.transform.position.y <= 19.5f && player.transform.position.y >= 15f);
                if (posX) pos.x = player.transform.position.x;
                if (posY) pos.y = player.transform.position.y;
                if (!posX && !posY) pos = transform.position;
                break;

            case "â��":
                pos.y = player.transform.position.y;
                break;

            default:
                pos.x = player.transform.position.x;
                pos.y = player.transform.position.y;
                break;
        }

        transform.position = pos;
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
    }
}