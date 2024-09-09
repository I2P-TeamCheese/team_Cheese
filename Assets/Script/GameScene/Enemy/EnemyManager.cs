using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemyList = new();
    [SerializeField] List<SpriteRenderer> enemySprites = new();
    [SerializeField] List<Animator> enemyEffects = new();  // Animator override �ؼ� �ϰ� ������ ���� ���� ���� ����

    private Bullet bullet;
    private bool isCoroutining = false; // ��ø�Ǽ� ����Ǵ� ���� ����


    public virtual void takeDamage(string name)
    {
        int index =  enemyList.FindIndex(x => x.name.Equals(name));
        //addEnemyInList(index);

        Enemy enemy = enemyList[index];
        SpriteRenderer enemySprite = enemySprites[index];
        Animator enemyAni = enemyEffects[index];
        string enemyTag = enemyList[index].tag;

        enemy.hp -= 1;
        if (enemy.isDead != true && !isCoroutining && enemy.hp > 0)
        {
            Debug.Log("hp ����");
            enemyAni.Play(enemy.tag + "Hit");
            StartCoroutine(changeColor(enemySprite));
            StartCoroutine(ResetToDefaultState(enemyAni));
        }

        if (!isCoroutining && enemy.hp ==0)
        {
            enemy.isDead = true;
            destroyEnemy(enemy, enemySprite, index, enemyAni);  // �� �ı�
        }
    }

    // ���Ŀ� ��������
    //void addEnemyInList(int index)
    //{
    //    Enemy enemy = enemyList[index];
    //    SpriteRenderer enemySprite = enemySprites[index];
    //    Animator enemyAni = enemyEffects[index];
    //    string enemyTag = enemyList[index].tag;
    //}

    
    void destroyEnemy(Enemy enemy, SpriteRenderer enemySprite, int index, Animator enemyAni)
    {
        string enemyTag = enemy.tag;
        switch (enemyTag)
        {
            case "pinkDollEnemy":
                {
                    deleteEnemyInLists(enemy, enemySprite, enemyAni);
                    enemyAni.Play(enemyTag + "Hit");
                    break;
                }
            case "rabbitDollEnemy":
                {
                    deleteEnemyInLists(enemy, enemySprite, enemyAni);
                    enemyAni.Play(enemyTag + "Hit");
                    break;
                }
        }
    }

  
    void deleteEnemyInLists(Enemy enemy, SpriteRenderer enemySprite, Animator enemyAni)
    {
        enemyList.Remove(enemy);
        enemySprites.Remove(enemySprite);
        enemyEffects.Remove(enemyAni);
    }

    IEnumerator changeColor(SpriteRenderer enemySprite)
    {
        isCoroutining = true;
        enemySprite.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        enemySprite.color = Color.white;
        isCoroutining = false;
    }

    IEnumerator ResetToDefaultState(Animator enemyAni)   // �ִϸ��̼��� �⺻ ���·� ��ȯ
    {
        yield return new WaitForSeconds(0.4f);
        enemyAni.Play("None");
    }


    void Start()
    {
        bullet = FindObjectOfType<Bullet>();

    }

     void Update()
    {
    }
}
