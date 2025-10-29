using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
    public int slotNum;
    public Item item;
    public Image itemIcon;

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
        itemIcon.preserveAspect = true; // 이미지 비율 유지
    }

    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }
}
