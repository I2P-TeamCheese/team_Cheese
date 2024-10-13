using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public Transform player;

    public Vector3 pos;
    public Vector3 offset;

    public float CameraSpeed = 2f;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void LateUpdate()
    {
        pos = transform.position;

        switch (GameManager.GameState)
        {
            case "Ʃ�丮��":
                if (player.position.y >= 47f && player.position.y <= 50f)
                    pos.y = player.position.y + offset.y;
                break;

            case "Ʃ�丮�� �ƾ�":
                pos.x = -51.9f;
                pos.y = 47f;
                break;

            case "��Ƽ��":
                pos.x = player.position.x + offset.x;
                pos.y = player.position.y + offset.y;
                break;

            case "���� #F":
                if(player.position.y >= -7.5f)
                    pos.y = Mathf.Lerp(pos.y, player.position.y + offset.y, Time.deltaTime * CameraSpeed);
                else
                {
                    pos.y = Mathf.Lerp(pos.y, -12.5f, Time.deltaTime * CameraSpeed);
                    pos.x = player.position.x + offset.x;
                }
                break;

            case "��ȸ�� �Ա�":
                pos.x = Mathf.Lerp(pos.x, 44f, Time.deltaTime * CameraSpeed);
                pos.y = Mathf.Lerp(pos.y, player.position.y + offset.y, Time.deltaTime * CameraSpeed);
                break;

            case "â��":
                pos.y = Mathf.Lerp(pos.y, player.position.y + offset.y, Time.deltaTime * CameraSpeed);
                break;

            default:
                pos.x = Mathf.Lerp(pos.x, player.position.x + offset.x, Time.deltaTime * CameraSpeed);
                pos.y = Mathf.Lerp(pos.y, player.position.y + offset.y, Time.deltaTime * CameraSpeed);
                break;
        }

        transform.position = pos;
    }
}
