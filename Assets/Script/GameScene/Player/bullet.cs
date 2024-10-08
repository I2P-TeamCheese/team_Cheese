using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;
    private Vector2 direction;

    private EnemyManager enemyList;


    void bulletDirectionSettings()
    {
        if (Player.playerDirection == 1)
        {
            direction = Vector2.up;
        }
        else if (Player.playerDirection == 2)
        {
            direction = Vector2.down;
        }
        else if (Player.playerDirection == 3)
        {
            direction = Vector2.left;
        }
        else if (Player.playerDirection == 4)
        {
            direction = Vector2.right;
        }

        Invoke("DestroyBullet", 2);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("enemy"))  // 적 레이어와 충돌했을 때
        {
            Enemy enemy = other.GetComponent<Enemy>(); // Enemy 스크립트 참조
            if (enemy != null)
            {
                Debug.Log(enemy.tag + " 명중!");
                enemyList.takeDamage(enemy.tag); // 적에게 데미지를 입힘
            }
            DestroyBullet(); // 충돌 후 총알 파괴
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        enemyList = FindObjectOfType<EnemyManager> ();
        bulletDirectionSettings();
    }

    // Update is called once per frame
    void Update()
    {
        // 총알을 설정된 방향으로 이동
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
