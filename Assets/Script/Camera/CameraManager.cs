using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;

    public Vector3 pos;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    public float cameraSpeed = 20f; 

    void LateUpdate()
    {
        pos = transform.position;

        switch (GameManager.GameState)
        {
            case "Ʃ�丮��":
                pos.y = Mathf.SmoothDamp(pos.y, player.position.y + offset.y, ref velocity.y, cameraSpeed * Time.deltaTime);
                break;

            case "Ʃ�丮�� �ƾ�":
                pos.x = -51.9f;
                pos.y = 47f;
                break;

            case "��Ƽ��":
                pos.x = Mathf.SmoothDamp(pos.x, player.position.x + offset.x, ref velocity.x, cameraSpeed * Time.deltaTime);
                pos.y = Mathf.SmoothDamp(pos.y, player.position.y + offset.y, ref velocity.y, cameraSpeed * Time.deltaTime);
                break;

            case "���� #F":
                if (player.position.y >= -7.5f)
                    pos.y = Mathf.SmoothDamp(pos.y, player.position.y + offset.y, ref velocity.y, cameraSpeed * Time.deltaTime);
                else
                {
                    pos.y = Mathf.SmoothDamp(pos.y, -12.5f, ref velocity.y, cameraSpeed * Time.deltaTime);
                    pos.x = Mathf.SmoothDamp(pos.x, player.position.x + offset.x, ref velocity.x, cameraSpeed * Time.deltaTime);
                }
                break;

            case "��ȸ�� �Ա�":
                pos.x = Mathf.SmoothDamp(pos.x, 44f, ref velocity.x, cameraSpeed * Time.deltaTime);
                pos.y = Mathf.SmoothDamp(pos.y, player.position.y + offset.y, ref velocity.y, cameraSpeed * Time.deltaTime);
                break;

            case "â��":
                pos.y = Mathf.SmoothDamp(pos.y, player.position.y + offset.y, ref velocity.y, cameraSpeed * Time.deltaTime);
                break;

            default:
                pos.x = Mathf.SmoothDamp(pos.x, player.position.x + offset.x, ref velocity.x, cameraSpeed * Time.deltaTime);
                pos.y = Mathf.SmoothDamp(pos.y, player.position.y + offset.y, ref velocity.y, cameraSpeed * Time.deltaTime);
                break;
        }

        transform.position = pos;
    }
}