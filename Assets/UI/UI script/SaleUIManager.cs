using UnityEngine;
using TMPro;

public class SaleUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText; // ��¥ �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI totalEarningsText; // �� ����� �ؽ�Ʈ
    [SerializeField] private GameObject saleUI; // �Ǹ�â UI (Canvas �Ǵ� Panel)
    [SerializeField] private GameObject aObject; // Ȱ��ȭ�� ������Ʈ

    private int currentDay = 1; // ���� ��¥ (�ʱⰪ: 1����)
    private int totalEarnings = 0; // ���� �� �����

    private const string DayKey = "CurrentDay"; // PlayerPrefs Ű ��

    private void Start()
    {
        // ����� ��¥ �ҷ�����
        currentDay = PlayerPrefs.GetInt(DayKey, 1);

        if (saleUI != null)
            saleUI.SetActive(false); // �ʱ� ���¿��� �Ǹ�â ��Ȱ��ȭ

        UpdateSaleUI(); // UI �ʱ�ȭ
    }

    private void UpdateSaleUI()
    {
        if (dayText != null)
            dayText.text = $"{currentDay}����";
        if (totalEarningsText != null)
            totalEarningsText.text = $"+ {totalEarnings:N0} G";
    }

    // ������� Inventory���� ���� ������ ������Ʈ
    public void UpdateTotalEarnings()
    {
        if (Inventory.instance != null)
        {
            totalEarnings = Inventory.instance.TotalPrice; // Inventory�� TotalPrice�� ������
            Debug.Log($"Total earnings updated from Inventory: {totalEarnings}");
            UpdateSaleUI(); // UI ����
        }
        else
        {
            Debug.LogWarning("Inventory instance is null. Unable to fetch total earnings.");
        }
    }

    public void OpenSaleUI()
    {
        if (saleUI != null)
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Catch);
            UpdateTotalEarnings(); // UI�� ���� ���� ����� ����
            saleUI.SetActive(true);
            Debug.Log("�Ǹ�â�� Ȱ��ȭ�Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogWarning("�Ǹ�â UI�� �������� �ʾҽ��ϴ�!");
        }
    }

    // ���� ��ư Ŭ�� �� ȣ��
    public void OnCollectEarningsButton()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Coin);
        // �����ݿ� ����� �߰�
        MoneyManager.Instance.AddCash(totalEarnings);
        Debug.Log($"Collected {totalEarnings} G. Current Cash: {MoneyManager.Instance.Cash}");

        // ��¥ ����
        currentDay++;
        PlayerPrefs.SetInt(DayKey, currentDay); // ��¥ ����
        totalEarnings = 0; // �� ����� �ʱ�ȭ
        UpdateSaleUI(); // UI ����

        // �Ǹ�â UI ��Ȱ��ȭ
        if (saleUI != null)
        {
            saleUI.SetActive(false);
            Debug.Log("�Ǹ�â�� ��Ȱ��ȭ�Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogWarning("�Ǹ�â UI�� �������� �ʾҽ��ϴ�!");
        }

        // a������Ʈ Ȱ��ȭ
        if (aObject != null)
        {
            aObject.SetActive(true);
            Debug.Log($"{aObject.name} has been activated.");
        }
        else
        {
            Debug.LogWarning("aObject�� �������� �ʾҽ��ϴ�!");
        }
    }

    // ��¥�� 1������ �ʱ�ȭ�ϴ� �Լ�
    public void ResetDayToFirst()
    {
        currentDay = 1;
        PlayerPrefs.SetInt(DayKey, currentDay); // ��¥ ����
        UpdateSaleUI(); // UI ����
        Debug.Log("Day reset to 1.");
    }
}
