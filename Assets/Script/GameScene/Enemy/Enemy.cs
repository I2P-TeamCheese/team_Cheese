using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject enemy;
    // EnemyManager���� ����ؼ� public ���� ����
    public int hp = 3;

     void destroyEnemy(GameObject enemy)
    {
        if(hp == 0)
        {
            Destroy(enemy);
        }
    }

    void Start()
    {
        enemy = this.gameObject;
    }
    void Update()
    {
        destroyEnemy(enemy);
    }
}
