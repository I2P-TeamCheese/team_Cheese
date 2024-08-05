using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        // player�� ��������Ʈ�� �޾ƿ��� ���� Find �Լ� ��� 
        GameObject otherObject = GameObject.Find("Player");
        SpriteRenderer playerRenderer = otherObject.GetComponent<SpriteRenderer>();

        // �÷��̾��� SpriteRenderer �� flip X�� �̿��ؼ� �Ѿ� ���� ����
        if (playerRenderer.flipX == true)   // �÷��̾ �������� ���� ���
        {
            direction = Vector2.right;
        }
        else  // �÷��̾ ������ ���� ���
        {
            direction = Vector2.left;
        }

        Invoke("DestroyBullet", 2);
    }

    // Update is called once per frame
    void Update()
    {
        // �Ѿ��� ������ �������� �̵�
        transform.Translate(direction * speed * Time.deltaTime);

        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, distance, isLayer);
        if (ray.collider != null)
        {
            if (ray.collider.tag == "Enemy")
            {
                Debug.Log("����!");
            }
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
