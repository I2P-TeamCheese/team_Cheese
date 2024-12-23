using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private ItemManager itemManager;
    private DialogueManager dialogueManager;

    private Collider2D objectCollider;

    public bool canPickUp = false;
    public bool isInstalled = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueManager.dialogue_continue)
        {
            canPickUp = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canPickUp)
        {
            inventoryManager.PickUpItem(objectCollider);
        }
    }

    void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        itemManager = FindFirstObjectByType<ItemManager>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        objectCollider = GetComponent<Collider2D>();
    }
}
