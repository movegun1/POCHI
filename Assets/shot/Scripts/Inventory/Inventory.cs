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

        LoadInventory(); // ����� �κ��丮 �ҷ�����
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
            Debug.LogWarning("CatchManager�� ã�� �� �����ϴ�.");
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
            SaveInventory(); // ������ �߰� �� ����
            return true;
        }

        return false;
    }

    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem?.Invoke();
        UpdateTotalPrice();
        SaveInventory(); // ������ ���� �� ����
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
        SaveInventory(); // �κ��丮 ���� �� ����
    }

    // �κ��丮�� ����
    public void SaveInventory()
    {
        List<int> itemIDs = items.Select(item => item.itemId).ToList();
        string serializedIDs = string.Join(",", itemIDs);
        PlayerPrefs.SetString("InventoryItems", serializedIDs);

        Debug.Log("�κ��丮�� ����Ǿ����ϴ�: " + serializedIDs);
    }

    // �κ��丮�� �ҷ�����
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
                    // ItemDatabase���� itemId�� ������ �˻�
                    Item item = ItemDatabase.instance.itemDB.Find(i => i.itemId == itemId);
                    if (item != null)
                    {
                        items.Add(item);
                    }
                }
            }

            Debug.Log("�κ��丮�� �ε�Ǿ����ϴ�: " + serializedIDs);
            onChangeItem?.Invoke(); // UI ������Ʈ
        }
    }
}
