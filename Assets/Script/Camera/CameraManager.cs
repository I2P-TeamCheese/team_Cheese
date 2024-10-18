using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Player player;

    public Vector3 pos;
    private Vector3 velocity = Vector3.zero;
    private string lastGameState;

    void Update()
    {
        pos = transform.position;
        bool isGameStateChanged = GameManager.GameState != lastGameState;

        switch (GameManager.GameState)
        {
            case "Ʃ�丮��":
                pos.x = -77f;
                pos.y = Mathf.Clamp(player.transform.position.y, 47.5f, 49f);
                break;

            case "Ʃ�丮�� �ƾ�":
                pos.x = -51.9f;
                pos.y = 47.5f;
                break;

            case "��Ƽ��":
                pos.x = 60f;
                pos.y = Mathf.Clamp(player.transform.position.y, 0, 1.3f);
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
                pos.x = Mathf.Clamp(player.transform.position.x, 65.5f, 73.8f);
                pos.y = Mathf.Clamp(player.transform.position.y, 16.4f, 19.5f);
                break;

            case "â�� �Ա�":
                pos.x = 12f;
                pos.y = player.transform.position.y;
                break;

            case "â��":
                pos.y = Mathf.Clamp(player.transform.position.y, 19.5f, 28.8f);
                break;

            default:
                pos.x = player.transform.position.x;
                pos.y = player.transform.position.y;
                break;
        }

        if (isGameStateChanged || GameManager.GameState == "Ʃ�丮�� �ƾ�") transform.position = pos;
        else transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, 0.1f);
        lastGameState = GameManager.GameState;
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
    }
}