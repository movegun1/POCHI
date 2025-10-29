using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaUI : MonoBehaviour
{
    [Header("Slot Settings")]
    [SerializeField] private GameObject slotPrefab; // 슬롯 프리팹
    [SerializeField] private Transform slotParent; // 슬롯이 배치될 부모 오브젝트

    [Header("Filter Buttons")]
    [SerializeField] private Button mammaliaButton; // 포유류 버튼
    [SerializeField] private Button birdButton; // 조류 버튼
    [SerializeField] private Button amphibianButton; // 양서류 버튼

    private void Start()
    {
        // 버튼 클릭 이벤트에 메서드 연결
        mammaliaButton.onClick.AddListener(() => OnFilterButtonClicked(ItemType.Mammalia));
        birdButton.onClick.AddListener(() => OnFilterButtonClicked(ItemType.Birds));
        amphibianButton.onClick.AddListener(() => OnFilterButtonClicked(ItemType.Amphibia));

        // 초기 필터: Mammalia
        FilterItems(ItemType.Mammalia);
    }

    private void OnFilterButtonClicked(ItemType itemType)
    {
        // 효과음 재생
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);

        // 아이템 필터링
        FilterItems(itemType);
    }

    private void FilterItems(ItemType itemType)
    {
        // 기존 슬롯 삭제
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        // 필터링된 아이템 리스트 가져오기
        List<Item> filteredItems = ItemDatabase.instance.itemDB.FindAll(item => item.itemType == itemType);

        // 필터링된 아이템으로 슬롯 생성
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
