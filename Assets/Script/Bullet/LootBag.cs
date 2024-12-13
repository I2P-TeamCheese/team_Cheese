using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();

    List<Loot> GetDroppedItems()    // item ������ ��� ����
    {
        int randomNum = Random.Range(1, 101);     // 1-100 �ۼ�Ʈ Ȯ�� ����
        List<Loot> possibleItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if (randomNum <= item.dropChance)   // ���� �� ���� < ������ ��� Ȯ�� -> �������� �����. 
            {
                possibleItems.Add(item);
            }
            // �� ����� ������ �� �������� 1���� �����ϴ� ���
            /*
            if (possibleItems.Count > 0)
            {
                Loot droppedItems = possibleItems[Random.Range(0, possibleItems.Count)];
                return droppeditem;
            }
            */
        }
        return possibleItems;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
       List<Loot> droppedItems = GetDroppedItems(); // Dropped Items
        if (droppedItems != null)    // If items are dropped
        {
            foreach (Loot droppedItem in droppedItems)
            {
                GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
                lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;
                lootGameObject.name = droppedItem.name;
            }
        }

    }
}

    