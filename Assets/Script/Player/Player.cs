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
    private UIManager uiManager;
    private TutorialManager tutorialManager;

    private Collider2D playerCollider;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Cake Event":
                StartCoroutine(tutorialManager.ShowTutorialUI(false, dialogueContentManager.d_cake));
                Destroy(other.gameObject);
                break;

            case "Camera Event":
                StartCoroutine(tutorialManager.ShowTutorialUI(false, dialogueContentManager.d_photo));
                Destroy(other.gameObject);
                break;
        }

        switch(other.gameObject.tag)
        {
            case "��Ƽ�� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "��Ƽ�� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "��ȸ�� �Ա� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "��ȸ�� �Ա� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "��ȸ�� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "��ȸ�� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "â�� �Ա� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "â�� �Ա� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "â�� (�Ա�)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "â�� (�ⱸ)": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "RoomE Go": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "RoomE Exit": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;

            case "RoomF Go": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
            case "RoomF Exit": teleportManager.Teleport(other.gameObject.tag, playerCollider); break;
        }
    }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueContentManager = FindObjectOfType<DialogueContentManager>();
        teleportManager = FindObjectOfType<TeleportManager>();
        playerCollider = GetComponent<Collider2D>();
        uiManager = FindObjectOfType<UIManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();
    }
}