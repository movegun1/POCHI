using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPlus : MonoBehaviour
{
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] Transform itemsParent;

    public void PlusSlot()
    {
          GameObject itemSlotGameObj = Instantiate(itemSlotPrefab);
          itemSlotGameObj.transform.SetParent(itemsParent, worldPositionStays: false);
    }
}
