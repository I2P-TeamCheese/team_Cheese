using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public Text text;
    public SpriteRenderer sprite;

    public List<string> contentsList;
    public List<Sprite> spriteList;

    public GameObject ingameUiPanel;
    public GameObject DialoguePanel;

    public int count; // ��ȭ �����Ȳ ǥ�ÿ�, Ȯ�� �� private �� ���� �ʿ�

    public bool dialogue_continue = false;

    private void Start()
    {
        text.text = "";
        contentsList = new List<string>();
        spriteList = new List<Sprite>();
        count = 0;
    } 

    public void ShowDialogue(Dialogue dialogue) // dlalogue�� sprite������ contents ������ �޾ƿ��� �Լ�
    {
        dialogue_continue = true;
        for(int i = 0; i < dialogue.contents.Length; i++)  
        {
            contentsList.Add(dialogue.contents[i]);
            spriteList.Add(dialogue.sprites[i]);
        }
        ingameUiPanel.SetActive(false);
        DialoguePanel.SetActive(true);
        StartCoroutine(startDialogueCoroutine());
    }

    public void ExitDialogue()
    {
        DialoguePanel = transform.GetChild(0).gameObject;
        text.text = "";
        contentsList.Clear();
        spriteList.Clear();
        count = 0;
        ingameUiPanel.SetActive(true);
        DialoguePanel.SetActive(false);
        dialogue_continue = false;
    }

    IEnumerator startDialogueCoroutine()
    {
        if (count != 0) //�ε��� ������ ���� 0�϶��� �ƴҶ� ����
        {
            if (spriteList[count] != spriteList[count - 1])
            {
                sprite.GetComponent<SpriteRenderer>().sprite = spriteList[count];
            }
            for (int i = 0; i < contentsList[count].Length; i++)
            {
                text.text += contentsList[count][i];
                yield return new WaitForSeconds(0.03f);
            }
        }
        if (count == 0)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = spriteList[count];
            for (int i = 0; i < contentsList[count].Length; i++)
            {
                text.text += contentsList[count][i];
                yield return new WaitForSeconds(0.03f);
            }
        }

        yield return null;
    }

    public void NextSentence()
    {
        if (dialogue_continue)
        {
            count++;
            text.text = "";
            if (count == contentsList.Count - 1)
            {
                StopCoroutine(startDialogueCoroutine());
            }
            else
            {
                StopCoroutine(startDialogueCoroutine());
                StartCoroutine(startDialogueCoroutine());
            }
        }        
    }

    private void Update()
    {
        if (dialogue_continue)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                count++;
                text.text = "";
                if (count == contentsList.Count)
                {
                    StopCoroutine(startDialogueCoroutine());
                    ExitDialogue();

                }
                else
                {
                    StopCoroutine(startDialogueCoroutine());
                    StartCoroutine(startDialogueCoroutine());
                }
            }
        }
    }
}
