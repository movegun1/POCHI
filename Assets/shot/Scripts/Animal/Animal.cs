using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public GameObject prefab; // 소환할 프리팹

    public void Ani(int db)
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Catch);
        // 인벤토리에 아이템 추가
        Inventory.instance.AddItem(ItemDatabase.instance.itemDB[db]);

        // 파괴하기 전에 부모 관계 해제
        transform.SetParent(null);
    }
}
