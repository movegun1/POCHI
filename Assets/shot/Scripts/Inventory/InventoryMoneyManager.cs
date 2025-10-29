using UnityEngine;
using TMPro;

public class InventoryMoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText; // �κ��丮 UI�� ǥ�õ� �� �ؽ�Ʈ
    [SerializeField] private GameObject targetObject;   // ��Ȱ��ȭ�� ������Ʈ

    private void Start()
    {
        UpdateMoneyText(); // ���� �� ���� �� ������Ʈ
    }

    private void Update()
    {
        UpdateMoneyText(); // �� �����Ӹ��� �� ������Ʈ
    }

    // �� �ؽ�Ʈ�� ������Ʈ
    public void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = $"{MoneyManager.Instance.Cash:N0} G"; // �� ���� ������ ǥ�� (�޸� ����)
        }
        else
        {
            Debug.LogWarning("Money Text is not assigned in the Inspector!");
        }
    }

    // ��ư Ŭ�� �� ȣ���Ͽ� ������Ʈ ��Ȱ��ȭ
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
