using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot2 : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemImage; // 슬롯 내의 ItemImage (자식 Image 오브젝트)
    private Item currentItem; // 슬롯에 연결된 아이템 데이터

    private RectTransform slotRectTransform; // 슬롯 RectTransform
    private RectTransform imageRectTransform; // 이미지 RectTransform

    private void Awake()
    {
        if (itemImage == null)
        {
            itemImage = GetComponentInChildren<Image>(); // 자식 Image 자동 탐색
        }

        slotRectTransform = GetComponent<RectTransform>();
        imageRectTransform = itemImage.GetComponent<RectTransform>();
    }

    // 슬롯에 아이템을 설정하는 메서드
    public void SetItem(Item item)
    {
        currentItem = item;

        if (item != null && item.itemImage != null)
        {
            itemImage.sprite = item.itemImage; // 이미지 설정
            itemImage.color = Color.white; // 이미지 색상 초기화
            AdjustImageToFitSlot(item.itemImage); // 이미지 비율 조정 및 슬롯 크기 맞춤
        }
        else
        {
            itemImage.sprite = null; // 이미지 제거
            itemImage.color = new Color(1, 1, 1, 0); // 투명하게 처리
        }
    }

    // 이미지 비율을 유지하면서 슬롯 크기에 꽉 차도록 조정
    private void AdjustImageToFitSlot(Sprite sprite)
    {
        if (sprite == null || slotRectTransform == null || imageRectTransform == null)
        {
            return;
        }

        // 슬롯 크기 가져오기
        float slotWidth = slotRectTransform.rect.width;
        float slotHeight = slotRectTransform.rect.height;

        // 이미지의 원본 비율 계산
        float spriteWidth = sprite.rect.width;
        float spriteHeight = sprite.rect.height;
        float spriteAspectRatio = spriteWidth / spriteHeight;

        // 슬롯의 비율 계산
        float slotAspectRatio = slotWidth / slotHeight;

        // 슬롯 크기에 맞게 이미지 크기 조정
        if (spriteAspectRatio > slotAspectRatio)
        {
            // 이미지가 가로로 더 길 경우: 슬롯의 너비에 맞춤
            imageRectTransform.sizeDelta = new Vector2(slotWidth, slotWidth / spriteAspectRatio);
        }
        else
        {
            // 이미지가 세로로 더 길거나 비율이 같을 경우: 슬롯의 높이에 맞춤
            imageRectTransform.sizeDelta = new Vector2(slotHeight * spriteAspectRatio, slotHeight);
        }
    }

    // 슬롯 클릭 이벤트 처리
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Slot clicked!");

        if (currentItem != null)
        {
            UIManager2.instance.ShowItemDetails(currentItem); // 아이템 세부 정보 표시
        }
        else
        {
            Debug.LogWarning("No item assigned to this slot!");
        }
    }
}
