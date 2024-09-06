using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hp = 3;
    private bool isCoroutining = false; // ��ø�Ǽ� ����Ǵ� ���� ����
    public Bullet bullet;
    public GameObject enemy;
    public SpriteRenderer enemySprite;

    bool isDead()
    {
        if (hp == 0)
        {
            Destroy(enemy);
            return true;
        }
        return false;
    }

    bool isDamaged()
    {
        if (isDead() != true)
        {
            if (bullet.isHit() == true && !isCoroutining  /* || ���� ���ݿ� �¾��� �� */)
            {
                hp--;
                StartCoroutine(changeColorToRed());
                return true;
            }
            else
                return false;
        }
        return false;
    }

    IEnumerator changeColorToRed()
    {
        isCoroutining = true;
        enemySprite.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        enemySprite.color = Color.clear;
        isCoroutining = false;
    }


    void Update()
    {
        isDamaged();
    }
}
