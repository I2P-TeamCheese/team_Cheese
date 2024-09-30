using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting; // ������ ���� ���


public class PlayerControl : MonoBehaviour
{
    private Player player;
    private EnemyManager enemyManager;
    private Stamina stamina;

    public SpriteRenderer rend; // player ��������Ʈ (�ٶ󺸴� ���� ����)
    public Animator Player_control; // player �̵� �� ���� �ִϸ��̼�

    public static float moveSpeed = 2.5f;

    public static bool MoveX = false;
    public static bool MoveY = false;

    public bool isMove = true; // isMove�� false �϶��� ������ �� ����.
    public bool isPush = false; // isPush�� false �϶��� Push Object�� �� �� ����.

    private Vector3 playerCenterOffset; // player �����Ǻ� offset

    // ���Ÿ� ���� ����
    public GameObject bullet;
    public Transform bulletPos;
    public static int playerDirection = 2; // 1: ��, 2: ����, 3: ����, 4: ������
    public float fireCooltime;
    private float fireCurtime;

    // �������� �� enemy�� �浹
    [SerializeField] List<GameObject> hp = new List<GameObject>();
    private Collider2D[] meleeAttackableEnemies;
    private Vector2 meleeAttackBoxSize;
    private Vector2 nearEnemyBoxSize;

    // ���� ���ݿ��� enemy ������ �޾ƿ��� ���ؼ� ����
    private Collider2D enemyCollider;

    void OnTriggerStay2D(Collider2D other)
    {
        //������Ʈ �б�
        if (other.gameObject.tag == "Push_Object")
        {
            isPush = true;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (isPush && Input.GetKey(KeyCode.LeftArrow))
                    Player_control.Play("LeftPush");
                else if (isPush && Input.GetKey(KeyCode.RightArrow))
                    Player_control.Play("RightPush");
                else if (isPush && Input.GetKey(KeyCode.UpArrow))
                    Player_control.Play("UpPush");
                else if (isPush && Input.GetKey(KeyCode.DownArrow))
                    Player_control.Play("DownPush");
            }

            else
                isPush = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Push_Object")
            isPush = false;
    }

    void Control() //�÷��̾��� �̵�
    {
        Player_control.speed = 1;

        if (!isMove)
        {
            Player_control.Play("PlayerBack_Stop");
        }

        if (isMove)
        {
            //���� �̵�
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (!isPush)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                        Player_control.Play("PlayerLeft");
                    else if (Input.GetKey(KeyCode.RightArrow))
                        Player_control.Play("PlayerRight");
                    else
                        Player_control.Play("PlayerUp");

                    if (Input.GetKey(KeyCode.DownArrow))
                        Player_control.Play("PlayerUp");
                }

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                playerDirection = 1;  // �� ����
            }

            //�Ʒ��� �̵�
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (!isPush)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                        Player_control.Play("PlayerLeft");
                    else if (Input.GetKey(KeyCode.RightArrow))
                        Player_control.Play("PlayerRight");
                    else
                        Player_control.Play("PlayerBack");

                    if (Input.GetKey(KeyCode.UpArrow))
                        Player_control.Play("PlayerUp");
                }

                MoveX = false;
                MoveY = true;

                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                playerDirection = 2;  // �Ʒ� ����
            }

            //�������� �̵�
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (!isPush)
                {
                    Player_control.Play("PlayerLeft");
                }

                MoveX = true;
                MoveY = false;

                playerCenterOffset.x = -0.05f;
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                playerDirection = 3;  // ���� ����
            }

            //���������� �̵�
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!isPush)
                {
                    Player_control.Play("PlayerRight");
                }

                MoveX = true;
                MoveY = false;

                playerCenterOffset.x = 0.05f;
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                playerDirection = 4;  // ������ ����
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) && playerDirection == 1)
                Player_control.Play("PlayerUp_Stop");
            else if (Input.GetKeyUp(KeyCode.DownArrow) && playerDirection == 2)
                Player_control.Play("PlayerBack_Stop");

            else if (Input.GetKeyUp(KeyCode.LeftArrow) && playerDirection == 3 && !Input.GetKey(KeyCode.RightArrow))
            {
                playerCenterOffset = new Vector3(0f, -0.4f, 0f);
                Player_control.Play("PlayerLeft_Stop");
            }

            else if (Input.GetKeyUp(KeyCode.RightArrow) && playerDirection == 4 && !Input.GetKey(KeyCode.LeftArrow))
            {
                playerCenterOffset = new Vector3(0f, -0.4f, 0f);
                Player_control.Play("PlayerRight_Stop");
            }

            //�޸���
            if (Input.GetKey(KeyCode.LeftShift) && player.stamina.value > 0.01f)
            {
                moveSpeed = 5;

                if (!isPush) Player_control.speed = 2;
                else Player_control.speed = 1;

                if (!isPush) stamina.isPlayerRunning = true;
                else stamina.isPlayerRunning = false;
            }

            else
            {
                Player_control.speed = 1;
                moveSpeed = 2.5f;
                stamina.isPlayerRunning = false;
            }
        }
    }

    void PlayerAttack()
    {
        enemyCollider = meleeAttackableEnemy();

        // ���� ���� ó��
        if (Input.GetKeyDown(KeyCode.LeftControl) && enemyCollider != null)  // ���� ���� ���� ���� ������ �����Ǿ��ٸ�
        {
            if (enemyCollider.gameObject.layer == LayerMask.NameToLayer("enemy"))
            {
                meleeAttack();
                enemyManager.takeDamage(enemyCollider.tag);
            }
        }
        // ���Ÿ� ���� ó��
        else if (Input.GetKeyDown(KeyCode.LeftControl) && enemyCollider == null && fireCurtime <= 0) // ��Ÿ�� Ȯ��
        {
            rangedAttack();
        }

        attackStop();

        // bullet�� �ִ� �ڵ带 ����� , �ܹ� ���
        if (fireCurtime > 0)
        {
            fireCurtime -= Time.deltaTime;  // ��Ÿ�� ����
        }
    }

    void meleeAttack()
    {
        if (playerDirection == 1)
        {
            Player_control.Play("PlayerMeleeAttackBack");
        }
        if (playerDirection == 2)
        {
            Player_control.Play("PlayerMeleeAttackFront");
        }
        if (playerDirection == 3)
        {
            Player_control.Play("PlayerMeleeAttackLeft");
        }
        if (playerDirection == 4)
        {
            Player_control.Play("PlayerMeleeAttackRight");
        }
    }

    void rangedAttack()
    {
        // ���⿡ ���� �ִϸ��̼� ����
        if (playerDirection == 1) // ��
        {
            Player_control.Play("PlayerLongAttackBack");
        }
        else if (playerDirection == 2) // ����
        {
            Player_control.Play("PlayerLongAttackFront");
        }
        else if (playerDirection == 3) // ����
        {
            Player_control.Play("PlayerLongAttackLeft");
        }
        else if (playerDirection == 4) // ������
        {
            Player_control.Play("PlayerLongAttackRight");
        }


        // �߻� ��Ÿ���� ������ ���� �Ѿ� �߻�
        Instantiate(bullet, bulletPos.position, transform.rotation);  // �Ѿ� ����
        fireCurtime = fireCooltime; // ��Ÿ�� �ʱ�ȭ
    }

    void attackStop()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (playerDirection == 1)
            {
                Player_control.Play("PlayerUp_Stop");
            }
            else if (playerDirection == 2)
            {
                Player_control.Play("PlayerBack_Stop");
            }
            else if (playerDirection == 3)
            {
                Player_control.Play("PlayerLeft_Stop");
            }
            else if (playerDirection == 4)
            {
                Player_control.Play("PlayerRight_Stop");
            }
        }
    }


    // ���� ����   -------------------------------------------------------------------------------------------

    public bool showRangeGizmo = false;
    /* Player�� enemy Ž�� Gizmo */
    private void OnDrawGizmosSelected()
    {
        if (showRangeGizmo)
        {
            Gizmos.color = new Color(1.0f, 0f, 0f, 0.8f);
            Gizmos.DrawCube(this.transform.position + playerCenterOffset, new Vector2(meleeAttackBoxSize.x, meleeAttackBoxSize.y));
        }
    }

    /*  ���� ���� ����
     linq(������ ���� ���)�� �̿��ؼ� ���� ����
     Gizmo�� ���� �ȿ� �����ϴ� ��� 2D �ݶ��̴��� ������
     => : ����
        Where : ������ �����ϴ� ��� ���͸�
        OrderBy: �������� ����
        oArray: �迭�� ��ȯ
     'enemy' �±׸� ���� PolygonCollider2D�� ���͸�

     */

    private Collider2D meleeAttackableEnemy()
    {
        Collider2D[] enemyArray = Physics2D.OverlapBoxAll((Vector2)(this.transform.position) + (Vector2)playerCenterOffset, meleeAttackBoxSize, 0f);

        meleeAttackableEnemies = enemyArray
        .Where(collider => collider.gameObject.layer == 6 /*6�� Layer�� enemy, LayerMask.NameToLayer("enemy")*/ && collider is PolygonCollider2D)
        .OrderBy(collider => Vector2.Distance(this.transform.position, collider.transform.position))
        .ToArray();


        if (meleeAttackableEnemies.Length > 0)
        {
            Debug.Log("Melee Attackable Enemy: " + meleeAttackableEnemies[0].name);
            return meleeAttackableEnemies[0];
        }
        else
            return null;
    }


    // Player HP ---------------------------------------------------------------------
    void getPlayerHP()
    {
        int numHp = GameObject.Find("playerHP").transform.childCount;
        for (int i = 0; i < numHp; i++)
        {
            GameObject hpObj = GameObject.Find("playerHP").transform.GetChild(i).gameObject;
            hp.Add(hpObj);
        }
    }

    /* HP ���� Gizmo */
    public bool showHPGizmo = false;
    private void OnDrawGizmos()
    {
        if (showHPGizmo)
        {
            Gizmos.color = new Color(0f, 3f, 0f, 0.7f);
            Gizmos.DrawCube(this.transform.position + playerCenterOffset, new Vector2(nearEnemyBoxSize.x, nearEnemyBoxSize.y));
        }
    }


    private Collider2D[] nearEnemies;
    private float elapsedTime = 0f;
    private float destroyTime = 1f;
    private bool isCollidingWithEnemy = false;

    /* CollideWithEnemy �Լ� ����
   'enemy' �±׸� ���� polygonCollider2D�� ���͸�
    => : ����
     Where : ������ �����ϴ� ��� ���͸�
     OrderBy: �������� ����
     oArray: �迭�� ��ȯ
  */

    public bool CollideWithEnemy()
    {
        Collider2D[] enemyArray = Physics2D.OverlapBoxAll((Vector2)(this.transform.position) + (Vector2)playerCenterOffset, nearEnemyBoxSize, 0f);

        nearEnemies = enemyArray
            .Where(collider => collider.gameObject.layer == 6 /*LayerMask.NameToLayer("enemy")*/ && collider is PolygonCollider2D)
            .OrderBy(collider => Vector2.Distance(this.transform.position, collider.transform.position))
            .ToArray();

        if (nearEnemies.Length > 0)
        {
            Debug.Log("Near Enemy: " + nearEnemies[0].name);
            return true;
        }
        else
            return false;
    }

    public void Player_Collision()
    {
        if (hp != null)
        {
            if (CollideWithEnemy() == true)
            {
                isCollidingWithEnemy = true;
            }
            else
            {
                isCollidingWithEnemy = false;
                elapsedTime = 0f;
            }

            if (isCollidingWithEnemy == true)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= destroyTime && hp.Count > 0)
                {
                    GameObject lastHp = hp[hp.Count - 1];
                    Destroy(lastHp);
                    hp.RemoveAt(hp.Count - 1); 
                    elapsedTime = 0f; // �ٽ� �ð� �ʱ�ȭ
                }
            }
        }
    }

    public bool Minigame_PlayerPos()
    {
        //�Ʒ� ���ǹ����� �������� ���� || �����ڸ� �̿��Ͽ� ���ǽ��� �߰����� ��.
        if (transform.position.y <= 48.5 && transform.position.y >= 47.5 && transform.position.x <= -76 && transform.position.x >= -78 //Ʃ�丮�� Pos��.
            )
        {
            return true;
        }
        return false;
    }

    void Start()
    {
        getPlayerHP();

        player = FindObjectOfType<Player>();
        enemyManager = FindObjectOfType<EnemyManager>();
        stamina = FindObjectOfType<Stamina>();

        // ���� ���� offset ��
        meleeAttackBoxSize = new Vector2(2.8f, 2.3f);
        nearEnemyBoxSize = new Vector2(1.2f, 1.7f);
        fireCooltime = 0.2f;
    }

    void Update()
    {
        Control();
        PlayerAttack();
        Player_Collision();
        UIManager.is_playerPos = Minigame_PlayerPos();
    }
}
