using UnityEngine;
using UnityEngine.UI;

public class LanternManager : MonoBehaviour
{
    [System.Serializable]
    public class LanternItem
    {
        public Button button; // Lantern 버튼
        public bool isEquipped; // 버튼이 활성화되었는지 여부
        public Sprite associatedSprite; // 버튼에 연결된 스프라이트
    }

    public LanternItem[] lanternItems; // Lantern 항목 배열
    public GameObject targetObject; // 이미지가 변경될 대상 오브젝트

    private SpriteRenderer spriteRenderer; // 대상 오브젝트의 SpriteRenderer
    private const string EquippedLanternKey = "EquippedLanternIndex"; // 저장 키

    private void Start()
    {
        // 대상 오브젝트에서 SpriteRenderer 가져오기
        if (targetObject != null)
        {
            spriteRenderer = targetObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("Target object does not have a SpriteRenderer component.");
                return;
            }
        }
        else
        {
            Debug.LogError("Target object is not assigned.");
            return;
        }

        // 각 버튼에 클릭 리스너 추가
        for (int i = 0; i < lanternItems.Length; i++)
        {
            int index = i; // 로컬 변수로 캡처
            lanternItems[i].button.onClick.AddListener(() => OnLanternButtonClicked(index));
        }

        // 초기 장착 상태 로드 및 반영
        LoadEquippedLantern();
    }

    public void OnLanternButtonClicked(int index)
    {
        // 모든 버튼 상태 초기화
        for (int i = 0; i < lanternItems.Length; i++)
        {
            lanternItems[i].isEquipped = (i == index); // 클릭한 버튼만 활성화
        }

        // 현재 장착 상태 저장
        SaveEquippedLantern(index);

        // 활성화된 버튼에 따라 스프라이트 변경
        UpdateTargetSprite();
    }

    private void UpdateTargetSprite()
    {
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer is not assigned.");
            return;
        }

        bool updated = false;

        foreach (var item in lanternItems)
        {
            if (item.isEquipped && item.associatedSprite != null)
            {
                if (spriteRenderer.sprite != item.associatedSprite) // 중복 작업 방지
                {
                    spriteRenderer.sprite = item.associatedSprite;
                    Debug.Log($"Sprite updated to: {item.associatedSprite.name}");
                }
                updated = true;
                break;
            }
        }

        if (!updated)
        {
            Debug.LogWarning("No equipped item or associated sprite found.");
        }
    }

    private void SaveEquippedLantern(int index)
    {
        PlayerPrefs.SetInt(EquippedLanternKey, index);
        PlayerPrefs.Save();
        Debug.Log($"Equipped lantern saved: {index}");
    }

    private void LoadEquippedLantern()
    {
        int equippedIndex = PlayerPrefs.GetInt(EquippedLanternKey, 0); // 기본값: 첫 번째 장비
        Debug.Log($"Loaded equipped lantern: {equippedIndex}");

        for (int i = 0; i < lanternItems.Length; i++)
        {
            lanternItems[i].isEquipped = (i == equippedIndex);
        }

        // 초기 상태 반영
        UpdateTargetSprite();
    }
}
