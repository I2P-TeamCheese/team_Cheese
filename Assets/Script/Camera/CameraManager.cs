using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Player player;

    public Vector3 pos;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        pos = transform.position;

        switch (GameManager.GameState)
        {
            case "Ʃ�丮��":
                pos.x = -77f;
                if (player.transform.position.y >= 47.5f && player.transform.position.y <= 49f) pos.y = player.transform.position.y;
                break;

            case "Ʃ�丮�� �ƾ�":
                pos.x = -51.9f;
                pos.y = 47.5f;
                break;

            case "��Ƽ��":
                pos.x = 60f;
                if (player.transform.position.y >= 0f && player.transform.position.y <= 1.3f) pos.y = player.transform.position.y;
                break;

            case "���� #F":
                if (player.transform.position.y >= -7.5f)
                {
                    pos.x = 44f;
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
                bool posY = (player.transform.position.y <= 21f && player.transform.position.y >= 16f);
                if (posX) pos.x = player.transform.position.x;
                if (posY) pos.y = player.transform.position.y;
                if (!posX && !posY) pos = transform.position;
                break;

            case "â�� �Ա�":
                pos.x = 12f;
                pos.y = player.transform.position.y;
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