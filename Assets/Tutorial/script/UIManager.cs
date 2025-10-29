using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] uiPanels; // UI ȭ�� �迭
    [SerializeField] private int currentIndex = 0; // ���� UI �ε���

    [SerializeField] private GameObject nextButton; // ���� ��ư
    [SerializeField] private GameObject backButton; // �ڷ� ��ư

    // Start���� �ʱ� UI ����
    private void Start()
    {
        UpdateUI();
    }

    // ���� ��ư Ŭ�� �� ȣ��
    public void GoToNextUI()
    {
        if (currentIndex < uiPanels.Length - 1)
        {
            currentIndex++;
            UpdateUI();
        }
        else
        {
            Debug.Log("������ UI ȭ���Դϴ�.");
        }
    }

    // �ڷ� ��ư Ŭ�� �� ȣ��
    public void GoToPreviousUI()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateUI();
        }
        else
        {
            Debug.Log("ù ��° UI ȭ���Դϴ�.");
        }
    }

    // UI ������Ʈ
    private void UpdateUI()
    {
        for (int i = 0; i < uiPanels.Length; i++)
        {
            uiPanels[i].SetActive(i == currentIndex); // ���� UI�� Ȱ��ȭ
        }

        // ��ư Ȱ��ȭ/��Ȱ��ȭ
        if (nextButton != null)
            nextButton.SetActive(currentIndex < uiPanels.Length - 1);
        if (backButton != null)
            backButton.SetActive(currentIndex > 0);

        Debug.Log($"���� UI �ε���: {currentIndex}");
    }
}
