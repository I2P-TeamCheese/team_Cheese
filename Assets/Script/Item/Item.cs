using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { �Ϲ�, ȸ��, ��� }

[System.Serializable]
public class Item
{
    public string id;
    public Type type;
    public GameObject prefab;
    public Sprite sprite;
}
