using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaUI : MonoBehaviour
{
    [Header("Slot Settings")]
    [SerializeField] private GameObject slotPrefab; // ���� ������
    [SerializeField] private Transform slotParent; // ������ ��ġ�� �θ� ������Ʈ

    [Header("Filter Buttons")]
    [SerializeField] private Button mammaliaButton; // ������ ��ư
    [SerializeField] private Button birdButton; // ���� ��ư
    [SerializeField] private Button amphibianButton; // �缭�� ��ư

    private void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ�� �޼��� ����
        mammaliaButton.onClick.AddListener(() => OnFilterButtonClicked(ItemType.Mammalia));
        birdButton.onClick.AddListener(() => OnFilterButtonClicked(ItemType.Birds));
        amphibianButton.onClick.AddListener(() => OnFilterButtonClicked(ItemType.Amphibia));

        // �ʱ� ����: Mammalia
        FilterItems(ItemType.Mammalia);
    }

    private void OnFilterButtonClicked(ItemType itemType)
    {
        // ȿ���� ���
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);

        // ������ ���͸�
        FilterItems(itemType);
    }

    private void FilterItems(ItemType itemType)
    {
        // ���� ���� ����
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        // ���͸��� ������ ����Ʈ ��������
        List<Item> filteredItems = ItemDatabase.instance.itemDB.FindAll(item => item.itemType == itemType);

        // ���͸��� ���������� ���� ����
        foreach (Item item in filteredItems)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            Slot2 slotComponent = newSlot.GetComponent<Slot2>();

            if (slotComponent != null)
            {
                slotComponent.SetItem(item);
            }
        }
    }
}
