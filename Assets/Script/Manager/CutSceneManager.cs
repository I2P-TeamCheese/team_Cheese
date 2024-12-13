using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour
{
    private PlayerControl playerControl;
    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private FadeManager fadeManager;
    private MainCamera mainCamera;
    private TutorialManager tutorialManager;
    private MiniGame miniGame;
    private InventoryManager inventoryManager;
    private UIManager uiManager;
    private NPCEnemy npcEnemy;
    private NPC npc;

    public GameObject Effect;
    public GameObject Blocking;
    public GameObject NPC;

    public GameObject Stage1;
    public GameObject BlockedWay;

    public GameObject VibrationEvent;

    public GameObject BigTeddyBearBos;
    public Animator BigTeddyBearBosAnimation;

    public GameObject BlackBackground;
    public GameObject WhiteBackground;

    public Text BosUI;

    public bool isCutScene = false;

    public IEnumerator CutScene_1()
    {
        isCutScene = true;
        yield return StartCoroutine(fadeManager.FadeOut());
        playerControl.transform.position = new Vector3(-1.5f, -1.5f, 0);
        mainCamera.transform.position = new Vector3(-1.5f, -1.5f, -10);
        miniGame.ClearPhotoMode();
        GameManager.GameState = "튜토리얼 컷씬";
        yield return StartCoroutine(fadeManager.FadeIn());
        playerControl.isMove = true;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_1_1);
        while (dialogueManager.dialogue_continue) yield return null;
        yield return StartCoroutine(fadeManager.ChangeStateFade("파티룸"));
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_1_2);
        isCutScene = false;
    }

    public IEnumerator CutScene_2()
    {
        isCutScene = true;
        yield return StartCoroutine(fadeManager.FadeOut());
        playerControl.transform.position = new Vector3(-49f, 27, 0);
        Destroy(NPCItem.Instance.gameObject);
        miniGame.ClearPhotoMode();
        NPC.SetActive(true);
        playerControl.isMove = false;
        playerControl.Direction = "Up";
        inventoryManager.Clean();
        yield return StartCoroutine(fadeManager.FadeIn());
        dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_1);
        while (GameManager.GameState != "CutScene2") yield return null;

        yield return StartCoroutine(fadeManager.FadeOut());
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(fadeManager.FadeIn());
        yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Stage_1);
        while (dialogueManager.dialogue_continue) yield return null;
        Effect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Effect.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Effect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        dialogueManager.ShowDialogue(dialogueContentManager.d_Stage_2);
        while (dialogueManager.dialogue_continue) yield return null;
        Blocking.SetActive(true);
        BlockedWay.SetActive(false);
        Stage1.SetActive(true);
        VibrationEvent.SetActive(true);
        isCutScene = false;
    }

    public IEnumerator CutScene_3()
    {
        isCutScene = true;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Bos1);
        yield return StartCoroutine(mainCamera.VibrationEffect(1f, 0.1f));
        while (dialogueManager.dialogue_continue) yield return null;

        dialogueManager.ShowDialogue(dialogueContentManager.d_Bos2);
        yield return StartCoroutine(mainCamera.VibrationEffect(1f, 0.1f));
        while (dialogueManager.dialogue_continue) yield return null;

        Vector2 targetPosition = new Vector2(playerControl.transform.position.x, BigTeddyBearBos.transform.position.y);
        float moveSpeed = 13f;
        BigTeddyBearBosAnimation.speed = 2f;
        BigTeddyBearBosAnimation.Play("BigTeddyBearMove");

        while ((Vector2)BigTeddyBearBos.transform.position != targetPosition)
        {
            BigTeddyBearBos.transform.position = Vector2.MoveTowards(
                BigTeddyBearBos.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        BigTeddyBearBosAnimation.Play("BigTeddyBearStop");
        BlackBackground.SetActive(true);
        StartCoroutine(CutScene_4());
    }

    public IEnumerator CutScene_4()
    {
        GameManager.GameState = "CutScene4";
        UIManager.is_CutScene_4 = true;
        yield return new WaitForSeconds(3);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_4_1);
        while (dialogueManager.dialogue_continue) yield return null;
        yield return new WaitForSeconds(2);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_4_2);
        while (dialogueManager.dialogue_continue) yield return null;
        yield return new WaitForSeconds(2);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_4_3);
    }

    public IEnumerator CutScene_5()
    {
        miniGame.ClearPhotoMode();
        GameManager.GameState = "CutScene5";
        WhiteBackground.SetActive(true);
        playerControl.Direction = "Up";
        playerControl.transform.position = new Vector3(-49f, 22f, 0);
        npc.transform.position = new Vector3(-49f, 25f, 0);
        yield return new WaitForSeconds(2);
        Effect.SetActive(false);
        BlackBackground.SetActive(false);
        UnityEngine.UI.Image image = WhiteBackground.GetComponent<UnityEngine.UI.Image>();
        yield return StartCoroutine(FadeOutImage(image, 1f));
        yield return new WaitForSeconds(1);
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_5_1);
        while (dialogueManager.dialogue_continue) yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.cutScene_5_2);
        dialogueManager.ChoiceButton(true, "난쟁이", "인형");
        while (dialogueManager.dialogue_continue) yield return null;
        yield return new WaitForSeconds(1);
        BlackBackground.SetActive(true);
        yield return new WaitForSeconds(1);
        BlackBackground.SetActive(false);
        yield return new WaitForSeconds(1);
        BlackBackground.SetActive(true);
        yield return new WaitForSeconds(1);
        BlackBackground.SetActive(false);
        StartCoroutine(mainCamera.VibrationEffect(4, 0.1f));
        npc.Transformation(true);
        yield return new WaitForSeconds(2f);
        BosUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        BosUI.gameObject.SetActive(false);
        yield return null;
        isCutScene = false;

        npc.attack = true; //공격 1번
        yield return new WaitForSeconds(1f);
        npc.attack = false;
        npc.walking = true;
        yield return new WaitForSeconds(1f);
        npc.walking = false;
        npc.attack = true; //공격 2번
        yield return new WaitForSeconds(1f);
        npc.attack = false;
        npc.walking = true;
        yield return new WaitForSeconds(1f);
        npc.walking = false;
        npc.attack = true; //공격 3번
        yield return new WaitForSeconds(1f);
        npc.attack = false;
        npc.walking = true;
        yield return new WaitForSeconds(1f);
        npc.walking = false;
        npc.attack = true; //공격 4번
        yield return new WaitForSeconds(1f);
        npc.attack = false;

        //보스 돌진 후 3번 충돌
        npc.isRush = true;
        while(npc.isRush) yield return null;
        yield return new WaitForSeconds(3f);
        npc.isRush = true;
        while (npc.isRush) yield return null;
        yield return new WaitForSeconds(3f);
        npc.isRush = true;
        while (npc.isRush) yield return null;
        yield return new WaitForSeconds(3f);

        //가운데 위치 후 원거리 공격 시작
        StartCoroutine(npc.RangedAttack());
    }

    public IEnumerator isVibrationEvent()
    {
        isCutScene = true;
        yield return StartCoroutine(mainCamera.VibrationEffect(1, 0.1f));
        yield return null;
        dialogueManager.ShowDialogue(dialogueContentManager.d_Stage_3);
        while (dialogueManager.dialogue_continue) yield return null;
        isCutScene = false;
    }

    IEnumerator FadeOutImage(UnityEngine.UI.Image image, float duration)
    {
        Color color = image.color;
        float startAlpha = color.a;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(startAlpha, 0f, t / duration);
            image.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        image.color = new Color(color.r, color.g, color.b, 0f);
        image.gameObject.SetActive(false);
    }


    void Start()
    {
        playerControl = FindFirstObjectByType<PlayerControl>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueContentManager = FindFirstObjectByType<DialogueContentManager>();
        fadeManager = FindFirstObjectByType<FadeManager>();
        mainCamera = FindFirstObjectByType<MainCamera>();
        tutorialManager = FindFirstObjectByType<TutorialManager>();
        miniGame = FindFirstObjectByType<MiniGame>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        uiManager = FindFirstObjectByType<UIManager>();
        npcEnemy = FindFirstObjectByType<NPCEnemy>();
        npc = FindFirstObjectByType<NPC>();
    }
}