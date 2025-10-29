using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] uiPanels; // UI 화면 배열
    [SerializeField] private int currentIndex = 0; // 현재 UI 인덱스

    [SerializeField] private GameObject nextButton; // 다음 버튼
    [SerializeField] private GameObject backButton; // 뒤로 버튼

    // Start에서 초기 UI 설정
    private void Start()
    {
        UpdateUI();
    }

    // 다음 버튼 클릭 시 호출
    public void GoToNextUI()
    {
        if (currentIndex < uiPanels.Length - 1)
        {
            currentIndex++;
            UpdateUI();
        }
        else
        {
            Debug.Log("마지막 UI 화면입니다.");
        }
    }

    // 뒤로 버튼 클릭 시 호출
    public void GoToPreviousUI()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateUI();
        }
        else
        {
            Debug.Log("첫 번째 UI 화면입니다.");
        }
    }

    // UI 업데이트
    private void UpdateUI()
    {
        for (int i = 0; i < uiPanels.Length; i++)
        {
            uiPanels[i].SetActive(i == currentIndex); // 현재 UI만 활성화
        }

        // 버튼 활성화/비활성화
        if (nextButton != null)
            nextButton.SetActive(currentIndex < uiPanels.Length - 1);
        if (backButton != null)
            backButton.SetActive(currentIndex > 0);

        Debug.Log($"현재 UI 인덱스: {currentIndex}");
    }
}
