using UnityEngine;
using TMPro;

public class InventoryMoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText; // 인벤토리 UI에 표시될 돈 텍스트
    [SerializeField] private GameObject targetObject;   // 비활성화할 오브젝트

    private void Start()
    {
        UpdateMoneyText(); // 시작 시 현재 돈 업데이트
    }

    private void Update()
    {
        UpdateMoneyText(); // 매 프레임마다 돈 업데이트
    }

    // 돈 텍스트를 업데이트
    public void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = $"{MoneyManager.Instance.Cash:N0} G"; // 돈 값을 가져와 표시 (콤마 포함)
        }
        else
        {
            Debug.LogWarning("Money Text is not assigned in the Inspector!");
        }
    }

    // 버튼 클릭 시 호출하여 오브젝트 비활성화
    public void DeactivateTargetObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
            Debug.Log($"{targetObject.name} has been deactivated.");
        }
        else
        {
            Debug.LogWarning("Target object is not assigned in the Inspector!");
        }
    }
}
