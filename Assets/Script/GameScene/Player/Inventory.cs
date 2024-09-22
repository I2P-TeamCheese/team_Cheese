using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // ���� �迭
    public string[] item_main_slot = new string[4];
    public Image[] item_main_slot_Image = new Image[4];

    // ������ ������Ʈ �� ��������Ʈ
    public GameObject BrownTeddyBear_Object;
    public GameObject PinkTeddyBear_Object;
    public GameObject YellowTeddyBear_Object;
    public GameObject Cake_Object;
    public GameObject NPC_Object;

    public Sprite BrownTeddyBear_Sprite;
    public Sprite PinkTeddyBear_Sprite;
    public Sprite YellowTeddyBear_Sprite;
    public Sprite Cake_Sprite;
    public Sprite NPC_Sprite;

    private DialogueManager dialogueManager;
    private UIManager uiManager;

    // ��ȭ����
    [SerializeField]
    public Dialogue d_camera;

    // ������ �α� / �÷��α�
    public void Slot1()
    {
        if (Player.object_collision == "��")
        {
            Instantiate(GetItemObject(item_main_slot[0]), Player.pos, Quaternion.identity);
            item_main_slot[0] = null;
            item_main_slot_Image[0].sprite = null;
        }

        if (Player.object_collision == "�繰")
        {
            Instantiate(GetItemObject(item_main_slot[0]), Object.pos, Quaternion.identity);
            item_main_slot[0] = null;
            item_main_slot_Image[0].sprite = null;
        }
    }

    public void Slot2()
    {
        if (Player.object_collision == "��")
        {
            Instantiate(GetItemObject(item_main_slot[1]), Player.pos, Quaternion.identity);
            item_main_slot[1] = null;
            item_main_slot_Image[1].sprite = null;
        }

        if (Player.object_collision == "�繰")
        {
            Instantiate(GetItemObject(item_main_slot[1]), Object.pos, Quaternion.identity);
            item_main_slot[1] = null;
            item_main_slot_Image[1].sprite = null;
        }
    }

    public void Slot3()
    {
        if (Player.object_collision == "��")
        {
            Instantiate(GetItemObject(item_main_slot[2]), Player.pos, Quaternion.identity);
            item_main_slot[2] = null;
            item_main_slot_Image[2].sprite = null;
        }

        if (Player.object_collision == "�繰")
        {
            Instantiate(GetItemObject(item_main_slot[2]), Object.pos, Quaternion.identity);
            item_main_slot[2] = null;
            item_main_slot_Image[2].sprite = null;
        }
    }

    public void Slot4()
    {
        if (Player.object_collision == "��")
        {
            Instantiate(GetItemObject(item_main_slot[3]), Player.pos, Quaternion.identity);
            item_main_slot[3] = null;
            item_main_slot_Image[3].sprite = null;
        }

        if (Player.object_collision == "�繰")
        {
            Instantiate(GetItemObject(item_main_slot[3]), Object.pos, Quaternion.identity);
            item_main_slot[3] = null;
            item_main_slot_Image[3].sprite = null;
        }
    }

    // ������ �ݱ� 
    GameObject GetItemObject(string item_name)
    {
        if (item_name == "BrownTeddyBear")
            return BrownTeddyBear_Object;
        else if (item_name == "PinkTeddyBear")
            return PinkTeddyBear_Object;
        else if (item_name == "YellowTeddyBear")
            return YellowTeddyBear_Object;
        else if (item_name == "Cake")
            return Cake_Object;
        else if (item_name == "NPC")
            return NPC_Object;

        return null;
    }

    Sprite GetItemSprite(string item_name)
    {
        if (item_name == "BrownTeddyBear")
            return BrownTeddyBear_Sprite;
        else if (item_name == "PinkTeddyBear")
            return PinkTeddyBear_Sprite;
        else if (item_name == "YellowTeddyBear")
            return YellowTeddyBear_Sprite;
        else if (item_name == "Cake")
            return Cake_Sprite;
        else if (item_name == "NPC")
            return NPC_Sprite;

        return null;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "DroppedBrownTeddyBear" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < item_main_slot.Length; i++)
            {
                if (item_main_slot[i] == "" || item_main_slot[i] == null)
                {
                    item_main_slot[i] = "BrownTeddyBear";
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]);
                    Destroy(other.gameObject);
                    dialogueManager.ShowDialogue(d_camera);
                    uiManager.CameraUI.SetActive(true);
                    break;
                }
            }
        }

        if (other.gameObject.tag == "Camera" && Input.GetKeyDown(KeyCode.Space))
        {
            dialogueManager.ShowDialogue(d_camera);
            uiManager.CameraUI.SetActive(true);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "BrownTeddyBear" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < item_main_slot.Length; i++)
            {
                if (item_main_slot[i] == "" || item_main_slot[i] == null)
                {
                    item_main_slot[i] = "BrownTeddyBear";
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }

        if (other.gameObject.tag == "PinkTeddyBear" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < item_main_slot.Length; i++)
            {
                if (item_main_slot[i] == "" || item_main_slot[i] == null)
                {
                    item_main_slot[i] = "PinkTeddyBear";
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }

        if (other.gameObject.tag == "YellowTeddyBear" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < item_main_slot.Length; i++)
            {
                if (item_main_slot[i] == "" || item_main_slot[i] == null)
                {
                    item_main_slot[i] = "YellowTeddyBear";
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }

        if (other.gameObject.tag == "Cake" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < item_main_slot.Length; i++)
            {
                if (item_main_slot[i] == "" || item_main_slot[i] == null)
                {
                    item_main_slot[i] = "Cake";
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }

        if (other.gameObject.tag == "NPC" && Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < item_main_slot.Length; i++)
            {
                if (item_main_slot[i] == "" || item_main_slot[i] == null)
                {
                    item_main_slot[i] = "NPC";
                    item_main_slot_Image[i].sprite = GetItemSprite(item_main_slot[i]);
                    Destroy(other.gameObject);
                    break;
                }
            }
        }
    }

     void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Slot1();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Slot2();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Slot3();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Slot4();
        }
    }
}