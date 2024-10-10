using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static bool objectCollision = false;

    private TeleportManager teleportManager;
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;

    private Collider2D playerCollider;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Cake Event":
                dialogueManager.ShowDialogue(dialogueContentManager.d_cake);
                Destroy(other.gameObject);
                break;
            case "Camera Event":
                dialogueManager.ShowDialogue(dialogueContentManager.d_photo);
                Destroy(other.gameObject);
                break;
        }

        switch(other.gameObject.tag)
        {
            case "��Ƽ�� (�Ա�)": teleportManager.teleport(other.gameObject.tag, playerCollider); break;
            case "��Ƽ�� (�ⱸ)": teleportManager.teleport(other.gameObject.tag, playerCollider); break;

            case "��ȸ�� �Ա� (�Ա�)": teleportManager.teleport(other.gameObject.tag, playerCollider); break;
            case "��ȸ�� �Ա� (�ⱸ)": teleportManager.teleport(other.gameObject.tag, playerCollider); break;

            case "��ȸ�� (�Ա�)": teleportManager.teleport(other.gameObject.tag, playerCollider); break;
            case "��ȸ�� (�ⱸ)": teleportManager.teleport(other.gameObject.tag, playerCollider); break;

            case "â�� �Ա� (�Ա�)": teleportManager.teleport(other.gameObject.tag, playerCollider); break;
            case "â�� �Ա� (�ⱸ)": teleportManager.teleport(other.gameObject.tag, playerCollider); break;

            case "â�� (�Ա�)": teleportManager.teleport(other.gameObject.tag, playerCollider); break;
            case "â�� (�ⱸ)": teleportManager.teleport(other.gameObject.tag, playerCollider); break;

            case "RoomE Go": teleportManager.teleport(other.gameObject.tag, playerCollider); break;
            case "RoomE Exit": teleportManager.teleport(other.gameObject.tag, playerCollider); break;

            case "RoomF Go": teleportManager.teleport(other.gameObject.tag, playerCollider); break;
            case "RoomF Exit": teleportManager.teleport(other.gameObject.tag, playerCollider); break;
        }
    }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueContentManager = FindObjectOfType<DialogueContentManager>();
        teleportManager = FindObjectOfType<TeleportManager>();
        playerCollider = GetComponent<Collider2D>();
    }
}