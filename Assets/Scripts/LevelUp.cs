using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private RectTransform rect;
    private ItemLevelUp[] items;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<ItemLevelUp>();
    }

    public void ShowLevelUp()
    {
        RandomLevelUp();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void HideLevelUp()
    {
        GameManager.instance.levelUpCount--;
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        if(GameManager.instance.levelUpCount > 0)
        {
            ShowLevelUp();
        }
    }

    public void RandomLevelUp()
    {
        foreach(ItemLevelUp item in items)
        {
            item.gameObject.SetActive(false);
        }
        int[] random = new int[3];
        while(true)
        {
            random[0] = Random.Range(0, items.Length);
            random[1] = Random.Range(0, items.Length);
            random[2] = Random.Range(0, items.Length);

            if(random[0] != random[1] && random[0] != random[2] && random[1] != random[2])
            {
                break;
            }
        }

        for(int i = 0; i < random.Length; i++)
        {
            ItemLevelUp randomItem = items[random[i]];

            if(randomItem.level == randomItem.itemData.damages.Length)
            {
                //items[Random.Range(4, 7)].gameObject.SetActive(true);  //¿©·¯°³ ·£´ý
                items[8].gameObject.SetActive(true);
            }
            else
            {
                randomItem.gameObject.SetActive(true);
            }
        }
    }
}
