using UnityEngine;
using UnityEngine.UI;

public class NetgunManager : MonoBehaviour
{
    [System.Serializable]
    public class NetgunItem
    {
        public Button button; // Netgun 버튼
        public bool isEquipped; // 버튼이 활성화되었는지 여부
        public Sprite associatedSprite; // 버튼에 연결된 스프라이트
    }

    public NetgunItem[] netgunItems; // Netgun 항목 배열
    public GameObject targetObject; // 이미지가 변경될 대상 오브젝트

    private SpriteRenderer spriteRenderer; // 대상 오브젝트의 SpriteRenderer
    private const string EquippedNetgunKey = "EquippedNetgunIndex"; // 저장 키

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
        for (int i = 0; i < netgunItems.Length; i++)
        {
            int index = i; // 로컬 변수로 캡처
            netgunItems[i].button.onClick.AddListener(() => OnNetgunButtonClicked(index));
        }

        // 초기 장착 상태 로드 및 반영
        LoadEquippedNetgun();
    }

    public void OnNetgunButtonClicked(int index)
    {
        // 모든 버튼 상태 초기화
        for (int i = 0; i < netgunItems.Length; i++)
        {
            netgunItems[i].isEquipped = (i == index); // 클릭한 버튼만 활성화
        }

        // 현재 장착 상태 저장
        SaveEquippedNetgun(index);

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

        for (int i = 0; i < netgunItems.Length; i++)
        {
            if (netgunItems[i].isEquipped && netgunItems[i].associatedSprite != null)
            {
                spriteRenderer.sprite = netgunItems[i].associatedSprite;
                updated = true;
                Debug.Log($"Target object's sprite updated to: {netgunItems[i].associatedSprite.name}");
                break; // 무한 호출 방지
            }
        }

        if (!updated)
        {
            Debug.LogWarning("No equipped button or associated sprite found.");
        }
    }


    private void SaveEquippedNetgun(int index)
    {
        PlayerPrefs.SetInt(EquippedNetgunKey, index);
        PlayerPrefs.Save();
        Debug.Log($"Equipped netgun saved: {index}");
    }

    private void LoadEquippedNetgun()
    {
        int equippedIndex = PlayerPrefs.GetInt(EquippedNetgunKey, 0); // 기본값: 첫 번째 장비
        Debug.Log($"Loaded equipped netgun: {equippedIndex}");

        for (int i = 0; i < netgunItems.Length; i++)
        {
            netgunItems[i].isEquipped = (i == equippedIndex);
        }

        // 초기 상태 반영
        UpdateTargetSprite();
    }
}
