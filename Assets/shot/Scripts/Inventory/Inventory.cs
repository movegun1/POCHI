using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();
    private int slotCnt;

    public InventoryUI InvenUi;

    public int TotalPrice;
    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange?.Invoke(slotCnt);
        }
    }

    public Button totalPriceButton;
    public Button clearInventoryButton;
    public TMP_Text totalPriceTMP;

    private void Start()
    {
        slotCnt = 32;
        totalPriceButton.onClick.AddListener(UpdateTotalPrice);

        if (clearInventoryButton != null)
        {
            clearInventoryButton.onClick.AddListener(ClearInventory);
        }

        LoadInventory(); // 저장된 인벤토리 불러오기
        UpdateTotalPrice();
    }

    public bool AddItem(Item _item)
    {
        CatchManager catchManager = FindObjectOfType<CatchManager>();
        if (catchManager != null)
        {
            catchManager.UpdateCaptureUI(_item);
        }
        else
        {
            Debug.LogWarning("CatchManager를 찾을 수 없습니다.");
        }

        if (items.Count >= SlotCnt)
        {
            InvenUi.AddSlot();
        }

        if (items.Count < SlotCnt)
        {
            items.Add(_item);
            onChangeItem?.Invoke();
            UpdateTotalPrice();
            SaveInventory(); // 아이템 추가 시 저장
            return true;
        }

        return false;
    }

    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem?.Invoke();
        UpdateTotalPrice();
        SaveInventory(); // 아이템 제거 후 저장
    }

    public void UpdateTotalPrice()
    {
        TotalPrice = items.Sum(item => item.price);

        if (totalPriceTMP != null)
        {
            totalPriceTMP.text = $"{TotalPrice:N0} G";
        }
    }

    public void ClearInventory()
    {
        items.Clear();
        onChangeItem?.Invoke();
        UpdateTotalPrice();
        SaveInventory(); // 인벤토리 비우기 후 저장
    }

    // 인벤토리를 저장
    public void SaveInventory()
    {
        List<int> itemIDs = items.Select(item => item.itemId).ToList();
        string serializedIDs = string.Join(",", itemIDs);
        PlayerPrefs.SetString("InventoryItems", serializedIDs);

        Debug.Log("인벤토리가 저장되었습니다: " + serializedIDs);
    }

    // 인벤토리를 불러오기
    public void LoadInventory()
    {
        string serializedIDs = PlayerPrefs.GetString("InventoryItems", "");

        if (!string.IsNullOrEmpty(serializedIDs))
        {
            string[] idArray = serializedIDs.Split(',');
            foreach (string id in idArray)
            {
                if (int.TryParse(id, out int itemId))
                {
                    // ItemDatabase에서 itemId로 아이템 검색
                    Item item = ItemDatabase.instance.itemDB.Find(i => i.itemId == itemId);
                    if (item != null)
                    {
                        items.Add(item);
                    }
                }
            }

            Debug.Log("인벤토리가 로드되었습니다: " + serializedIDs);
            onChangeItem?.Invoke(); // UI 업데이트
        }
    }
}
