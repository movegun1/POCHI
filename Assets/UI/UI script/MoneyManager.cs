using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance; // �̱��� �ν��Ͻ�
    public int Cash { get; private set; } // ���� ��, �ܺο��� �б⸸ ����

    [SerializeField] private TextMeshProUGUI cashText; // TextMeshPro �ؽ�Ʈ

    private const string CashKey = "PlayerCash"; // PlayerPrefs Ű ��

    private void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� ����
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadCash(); // ����� ������ �ε�
        UpdateCashUI(); // UI ������Ʈ
    }

    // �� �߰�
    public void AddCash(int amount)
    {
        Cash += amount;
        SaveCash(); // ����
        UpdateCashUI(); // UI ������Ʈ
        Debug.Log($"Added {amount} G. Current cash: {Cash}");
    }

    // �� ����
    public void SubtractCash(int amount)
    {
        if (Cash >= amount)
        {
            Cash -= amount;
            SaveCash(); // ����
            UpdateCashUI(); // UI ������Ʈ
        }
        else
        {
            Debug.LogWarning("Not enough cash!");
        }
    }

    // ĳ�� ������ ����
    private void SaveCash()
    {
        PlayerPrefs.SetInt(CashKey, Cash);
        PlayerPrefs.Save();
    }

    // ĳ�� ������ �ε�
    private void LoadCash()
    {
        Cash = PlayerPrefs.GetInt(CashKey, 0); // ����� ���� ������ 0
    }

    // UI ������Ʈ
    private void UpdateCashUI()
    {
        if (cashText != null)
        {
            cashText.text = $"{Cash} G";
        }
    }
    // ���� MoneyManager�� �߰�
    public void ResetCash()
    {
        Cash = 0;
        SaveCash(); // 0���� �ʱ�ȭ�� ���� ����
        Debug.Log("Cash has been reset to 0!");
    }


}
