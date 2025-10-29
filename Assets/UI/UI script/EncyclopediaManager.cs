using UnityEngine;
using TMPro;

public class EncyclopediaManager : MonoBehaviour
{
    [System.Serializable]
    public class EncyclopediaEntry
    {
        public string entryName;  // 항목 이름 (선택 사항)
        public TextMeshProUGUI textElement;  // 색상이 바뀔 텍스트
    }

    public EncyclopediaEntry[] entries; // 여러 엔트리를 관리

    private readonly Color activeColor = new Color32(255, 255, 255, 255); // 활성화된 텍스트 색상 (FFFFFF)
    private readonly Color inactiveColor = new Color32(50, 50, 50, 255);  // 비활성화된 텍스트 색상 (323232)

    // 버튼이 눌렸을 때 호출될 메서드
    public void OnButtonClicked(int index)
    {
        if (index < 0 || index >= entries.Length)
        {
            Debug.LogWarning("잘못된 인덱스입니다!");
            return;
        }

        // 모든 텍스트를 비활성화 색상으로 설정
        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].textElement != null)
            {
                entries[i].textElement.color = inactiveColor;
            }
        }

        // 선택된 텍스트만 활성화 색상으로 설정
        if (entries[index].textElement != null)
        {
            entries[index].textElement.color = activeColor;
        }

        Debug.Log($"Button {index} 클릭: 텍스트 색상 업데이트 완료");
    }

    // 초기화: 아무 작업도 하지 않음
    private void Start()
    {
        Debug.Log("EncyclopediaManager 초기화 완료");
    }
}
