using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TeddyBearChair : MonoBehaviour
{

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BrownTeddyBear") //����ũ�� ���̺� �������� ����� �̺�Ʈ ������Ʈ
        {
            UIManager.is_bear = true;
        }
    }
}
