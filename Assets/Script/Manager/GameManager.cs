using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string GameState = "Ʃ�丮��";
    public string GameStatePrint;

    private DialogueManager dialogueManager;
    private FadeManager fadeManager;

    void Start()
    {
        GameState = "Ʃ�丮��"; /* Ʃ�丮�� -> Ʃ�丮�� �ƾ� -> ��Ƽ�� -> â�� */

        dialogueManager = FindObjectOfType<DialogueManager>();
        fadeManager = FindObjectOfType<FadeManager>();
    }

    void Update()
    {
        if (GameState == "Ʃ�丮�� �ƾ�")
            if (dialogueManager.button_text.text == "�ݱ�")
                if (Input.GetKeyDown(KeyCode.Z))
                    StartCoroutine(fadeManager.ChangeStateFade("��Ƽ��"));
        GameStatePrint = GameState;
    }
}