using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 네임스페이스 추가

public class UIManager2 : MonoBehaviour
{
    public static UIManager2 instance; // 싱글톤 패턴
    [SerializeField] private GameObject targetUI; // 활성화/비활성화할 대상 UI

    [Header("UI Elements")]
    [SerializeField] private GameObject detailPanel; // 아이템 상세 정보를 표시할 패널
    [SerializeField] private Image itemDetailImage; // 아이템 상세 이미지
    [SerializeField] private TMP_Text itemNameText; // 아이템 이름 텍스트
    [SerializeField] private TMP_Text itemPriceText; // 아이템 가격 텍스트
    [SerializeField] private TMP_Text itemDescriptionText; // 게임 내 설명 텍스트
    [SerializeField] private TMP_Text itemRealDescriptionText; // 현실 설명 텍스트

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 제거
        }
    }

    // 아이템 정보를 UI에 표시하고 패널을 활성화
    public void ShowItemDetails(Item item)
    {
        if (targetUI != null)
        {
            // 현재 상태를 반전하여 활성화/비활성화
            targetUI.SetActive(!targetUI.activeSelf);
        }

        if (item == null) return;

        // UI 요소에 아이템 데이터 설정
        if (item.itemImage != null)
        {
            itemDetailImage.sprite = item.itemImage;
            itemDetailImage.color = Color.white; // 이미지 표시
            itemDetailImage.preserveAspect = true; // 이미지 비율 유지
        }
        else
        {
            itemDetailImage.sprite = null;
            itemDetailImage.color = new Color(1, 1, 1, 0); // 이미지 비활성화
        }

        itemNameText.text = item.itemName;
        itemPriceText.text = $"{item.price:N0} G"; // 천 단위 쉼표 추가
        itemDescriptionText.text = item.itemText;
        itemRealDescriptionText.text = item.itemRealText;

        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        // 패널 활성화
        detailPanel.SetActive(true);
    }
}
