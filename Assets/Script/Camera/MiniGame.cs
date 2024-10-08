using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    public static bool is_take_photo;
    public bool is_minigame = false;
    public GameObject mainCamera;
    public GameObject photoCamera;
    public Slider x_Axis;
    public Slider y_Axis;
    public GameObject minigamePanel;
    public GameObject ingameUIPanel;
    public GameObject player;
    public GameObject NPC;

    // E키 카메라앨범 이미지에 어떤 이미지가 들어가야 할 지 판단하는 변수들
    public static bool isImageChange = false;

    private DialogueManager dialogueManager;
    private DialogueContentManager dialogueContentManager;
    private FadeManager fadeManager;

    //private bool is_next_stage = false;
    //private bool is_transition = false;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueContentManager = FindObjectOfType<DialogueContentManager>();
        fadeManager = FindObjectOfType<FadeManager>();
    }

    private void Update()
    {
        PhotoMode();
        TakePhoto();
    }

    private void PhotoMode()
    {
        if (is_take_photo == true && Input.GetKeyDown(KeyCode.P))
        {
            player.SetActive(false);
            mainCamera.GetComponent<Camera>().enabled = false;
            photoCamera.GetComponent<Camera>().enabled = true;
            ingameUIPanel.SetActive(false);
            minigamePanel.SetActive(true);
            is_minigame = true;
        }
    }

    private void TakePhoto()
    {
        if (is_minigame)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) photoCamera.transform.Translate(-1f * 1.5f * Time.deltaTime, 0f, 0f);
            if (Input.GetKey(KeyCode.RightArrow)) photoCamera.transform.Translate(1f * 1.5f * Time.deltaTime, 0f, 0f);
            if (Input.GetKey(KeyCode.UpArrow)) photoCamera.transform.Translate(0f, 1f * 1.5f * Time.deltaTime, 0f);
            if (Input.GetKey(KeyCode.DownArrow)) photoCamera.transform.Translate(0f, -1f * 1.5f * Time.deltaTime, 0f);

            //GameState가 Tutorial일 때.
            if (GameManager.GameState == "Tutorial")
            {
                float ClampX = Mathf.Clamp(photoCamera.transform.position.x, -78.2f, -76.2f);
                float ClampY = Mathf.Clamp(photoCamera.transform.position.y, 48f, 50f);

                photoCamera.transform.position = new Vector3(ClampX, ClampY, -1f);

                x_Axis.value = photoCamera.transform.position.x;
                y_Axis.value = photoCamera.transform.position.y;

                if (Input.GetKey(KeyCode.F)
                    && x_Axis.value <= -76.9f && x_Axis.value >= -77.1f
                    && y_Axis.value <= 48.7f && y_Axis.value >= 48.35f)
                {
                    // 다음 스테이지로 넘어가는 로직
                    UIManager.is_cake = false;
                    UIManager.is_bear = false;
                    UIManager.tutorialTrigger = false;
                    is_take_photo = false;
                    is_minigame = false;
                    isImageChange = true;

                    GameManager.GameState = "Tutorial Cut Scene";
                    StartCoroutine(NextStage1());
                }
            }

            //GameState가 Stage1일 때.
            if (GameManager.GameState == "Stage1")
            {
                x_Axis.minValue = 11.8f;
                x_Axis.maxValue = 13.8f;

                y_Axis.minValue = 29.5f;
                y_Axis.maxValue = 31.5f;

                float ClampX = Mathf.Clamp(photoCamera.transform.position.x, 11.8f, 13.8f);
                float ClampY = Mathf.Clamp(photoCamera.transform.position.y, 29.5f, 31.5f);

                photoCamera.transform.position = new Vector3(ClampX, ClampY, -1f);

                x_Axis.value = photoCamera.transform.position.x;
                y_Axis.value = photoCamera.transform.position.y;

                if (Input.GetKey(KeyCode.F)
                    && x_Axis.value >= 12.8f && x_Axis.value <= 13.1f
                    && y_Axis.value >= 29.8f && y_Axis.value <= 30.2f)
                {
                    // CutScene으로 넘어가는 로직
                    UIManager.is_NPC = false;
                    is_take_photo = false;
                    is_minigame = false;
                    isImageChange = true;

                    StartCoroutine(DemoCutScene());
                }
            }
        }
    }

    void ClearPhotoMode()
    {
        player.SetActive(true);
        mainCamera.GetComponent<Camera>().enabled = true;
        photoCamera.GetComponent<Camera>().enabled = false;
        ingameUIPanel.SetActive(true);
        minigamePanel.SetActive(false);
        is_minigame = false;
    }

    IEnumerator NextStage1()
    {
        yield return StartCoroutine(fadeManager.FadeOut());
        player.transform.position = new Vector3(60, 0, 0);
        mainCamera.transform.position = new Vector3(60, 0, -10);
        ClearPhotoMode();
        yield return StartCoroutine(fadeManager.FadeIn());
        dialogueManager.ShowDialogue(dialogueContentManager.d_cutScene);
    }

    IEnumerator DemoCutScene()
    {
        yield return StartCoroutine(fadeManager.FadeOut());
        player.transform.position = new Vector3(12.5f, 28, 0);
        Destroy(NPCItem.Instance.gameObject);
        ClearPhotoMode();
        NPC.SetActive(true);
        yield return StartCoroutine(fadeManager.FadeIn());
        dialogueManager.ShowDialogue(dialogueContentManager.d_Demo_1);
    }
}
