using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private Player player;
    private PlayerAttack playerAttack;
    private Stamina stamina;

    public Animator animator; // player �̵� �� ���� �ִϸ��̼�

    public static float speed = 2.5f;

    public static bool MoveX = false;
    public static bool MoveY = false;

    public bool isMove = true; // isMove�� false �϶��� ������ �� ����.
    public bool isPush = false; // isPush�� false �϶��� Push Object�� �� �� ����.

    public Vector3 CenterOffset; // player �����Ǻ� offset
    public int Direction = 2; // 1: ��, 2: ����, 3: ����, 4: ������

    void OnTriggerStay2D(Collider2D other)
    {
        //������Ʈ �б�
        if (other.gameObject.CompareTag("Push_Object"))
        {
            isPush = true;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (isPush && Input.GetKey(KeyCode.LeftArrow)) animator.Play("LeftPush");
                else if (isPush && Input.GetKey(KeyCode.RightArrow)) animator.Play("RightPush");
                else if (isPush && Input.GetKey(KeyCode.UpArrow)) animator.Play("UpPush");
                else if (isPush && Input.GetKey(KeyCode.DownArrow)) animator.Play("DownPush");
            }

            else isPush = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Push_Object")) isPush = false;
    }

    void MoveControl() //�÷��̾��� �̵�
    {
        if (!isMove && !playerAttack.isChangingSprite)
        {
            switch(GameManager.GameState)
            {
                case "â��": animator.Play("PlayerUp_Stop"); break;
                default: animator.Play("PlayerBack_Stop"); break;
            }

            animator.speed = 1;
        }

        // �ǰ� ������ �� ������
        if (isMove && playerAttack.isChangingSprite == true)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Direction = 1;
                animator.Play("playerDamagedBack");
                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                Direction = 2;
                animator.Play("playerDamagedFront");

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Direction = 3;
                animator.Play("playerDamagedLeft");
                MoveX = true;
                MoveY = false;

                CenterOffset.x = -0.05f;
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                Direction = 4;
                animator.Play("playerDamagedRight");
                MoveX = true;
                MoveY = false;

                CenterOffset.x = 0.05f;
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }

        if (isMove && !playerAttack.isChangingSprite)
        {
            //���� �̵�
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (!isPush)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                        animator.Play("PlayerLeft");
                    else if (Input.GetKey(KeyCode.RightArrow))
                        animator.Play("PlayerRight");
                    else if (Input.GetKey(KeyCode.DownArrow))
                        animator.Play("PlayerBack_Stop");
                    else
                        animator.Play("PlayerUp");
                }

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.up * speed * Time.deltaTime);
                Direction = 1;  // �� ����
            }

            //�Ʒ��� �̵�
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (!isPush)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                        animator.Play("PlayerLeft");
                    else if (Input.GetKey(KeyCode.RightArrow))
                        animator.Play("PlayerRight");
                    else if (Input.GetKey(KeyCode.UpArrow))
                        animator.Play("PlayerBack_Stop");
                    else
                        animator.Play("PlayerBack");
                }

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.down * speed * Time.deltaTime);
                Direction = 2;  // �Ʒ� ����
            }

            //�������� �̵�
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (!isPush)
                {
                    if (Input.GetKey(KeyCode.RightArrow)) animator.Play("PlayerBack_Stop");
                    else animator.Play("PlayerLeft");
                }

                MoveX = true;
                MoveY = false;

                CenterOffset.x = -0.05f;
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                Direction = 3;  // ���� ����
            }

            //���������� �̵�
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!isPush)
                {
                    if (Input.GetKey(KeyCode.LeftArrow)) animator.Play("PlayerBack_Stop");
                    else animator.Play("PlayerRight");
                }
                    

                MoveX = true;
                MoveY = false;

                CenterOffset.x = 0.05f;
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                Direction = 4;  // ������ ����
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) && Direction == 1) animator.Play("PlayerUp_Stop");
            else if (Input.GetKeyUp(KeyCode.DownArrow) && Direction == 2) animator.Play("PlayerBack_Stop");

            else if (Input.GetKeyUp(KeyCode.LeftArrow) && Direction == 3 && !Input.GetKey(KeyCode.RightArrow))
            {
                CenterOffset = new Vector3(0f, -0.4f, 0f);
                animator.Play("PlayerLeft_Stop");
            }

            else if (Input.GetKeyUp(KeyCode.RightArrow) && Direction == 4 && !Input.GetKey(KeyCode.LeftArrow))
            {
                CenterOffset = new Vector3(0f, -0.4f, 0f);
                animator.Play("PlayerRight_Stop");
            }

            //�޸���
            if (Input.GetKey(KeyCode.LeftShift) && stamina.playerStaminaBar.value > 0.01f)
            {
                if (!isPush)
                {
                    speed = 5f;
                    animator.speed = 2f;
                    stamina.isPlayerRunning = true;
                }

                else
                {
                    speed = 2f;
                    animator.speed = 1f;
                    stamina.isPlayerRunning = false;
                }
            }

            else
            {
                animator.speed = 1;
                speed = 2.5f;
                stamina.isPlayerRunning = false;
            }
        }
    }

    public bool Minigame_PlayerPos()
    {
        //�Ʒ� ���ǹ����� �������� ���� || �����ڸ� �̿��Ͽ� ���ǽ��� �߰����� ��.
        if (transform.position.y <= 48.5 && transform.position.y >= 47.5 && transform.position.x <= -76 && transform.position.x >= -78) //Ʃ�丮�� Pos��.
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        MoveControl();
        UIManager.is_playerPos = Minigame_PlayerPos();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        stamina = FindObjectOfType<Stamina>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }
}