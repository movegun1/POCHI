using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance; // 싱글톤 인스턴스
    public int Cash { get; private set; } // 현재 돈, 외부에서 읽기만 가능

    [SerializeField] private TextMeshProUGUI cashText; // TextMeshPro 텍스트

    private const string CashKey = "PlayerCash"; // PlayerPrefs 키 값

    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadCash(); // 저장된 데이터 로드
        UpdateCashUI(); // UI 업데이트
    }

    // 돈 추가
    public void AddCash(int amount)
    {
        Cash += amount;
        SaveCash(); // 저장
        UpdateCashUI(); // UI 업데이트
        Debug.Log($"Added {amount} G. Current cash: {Cash}");
    }

    // 돈 차감
    public void SubtractCash(int amount)
    {
        if (Cash >= amount)
        {
            Cash -= amount;
            SaveCash(); // 저장
            UpdateCashUI(); // UI 업데이트
        }
        else
        {
            Debug.LogWarning("Not enough cash!");
        }
    }

    // 캐쉬 데이터 저장
    private void SaveCash()
    {
        PlayerPrefs.SetInt(CashKey, Cash);
        PlayerPrefs.Save();
    }

    // 캐쉬 데이터 로드
    private void LoadCash()
    {
        Cash = PlayerPrefs.GetInt(CashKey, 0); // 저장된 값이 없으면 0
    }

    // UI 업데이트
    private void UpdateCashUI()
    {
        if (cashText != null)
        {
            cashText.text = $"{Cash} G";
        }
    }
    // 기존 MoneyManager에 추가
    public void ResetCash()
    {
        Cash = 0;
        SaveCash(); // 0으로 초기화된 값을 저장
        Debug.Log("Cash has been reset to 0!");
    }


}
