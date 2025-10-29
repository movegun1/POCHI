using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    public SlotPlus SP;

    Inventory inven;

    public Slot[] slots;
    public Transform slotHorder;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        inven = Inventory.instance;
        slots = slotHorder.GetComponentsInChildren<Slot>();
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        for (int i = 0; i < slots.Length; i++)
            slots[i].slotNum = i;
    }

    private void SlotChange(int val) // 원래는 private였다.
    {
        for (int i = 0; i <slots.Length; i++)
        {
            slots[i].slotNum = i;
            if (i < inven.SlotCnt)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void AddSlot()
    {
        inven.SlotCnt += 8;
        for (int i = 0; i < 8; i++)
            SP.PlusSlot();
        slots = slotHorder.GetComponentsInChildren<Slot>();
        SlotChange(1);
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < inven.items.Count; i++) 
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }

}
