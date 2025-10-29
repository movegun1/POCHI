using UnityEngine;
using TMPro;

public class SaleUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText; // 날짜 텍스트
    [SerializeField] private TextMeshProUGUI totalEarningsText; // 총 정산금 텍스트
    [SerializeField] private GameObject saleUI; // 판매창 UI (Canvas 또는 Panel)
    [SerializeField] private GameObject aObject; // 활성화할 오브젝트

    private int currentDay = 1; // 현재 날짜 (초기값: 1일차)
    private int totalEarnings = 0; // 현재 총 정산금

    private const string DayKey = "CurrentDay"; // PlayerPrefs 키 값

    private void Start()
    {
        // 저장된 날짜 불러오기
        currentDay = PlayerPrefs.GetInt(DayKey, 1);

        if (saleUI != null)
            saleUI.SetActive(false); // 초기 상태에서 판매창 비활성화

        UpdateSaleUI(); // UI 초기화
    }

    private void UpdateSaleUI()
    {
        if (dayText != null)
            dayText.text = $"{currentDay}일차";
        if (totalEarningsText != null)
            totalEarningsText.text = $"+ {totalEarnings:N0} G";
    }

    // 정산금을 Inventory에서 직접 가져와 업데이트
    public void UpdateTotalEarnings()
    {
        if (Inventory.instance != null)
        {
            totalEarnings = Inventory.instance.TotalPrice; // Inventory의 TotalPrice를 가져옴
            Debug.Log($"Total earnings updated from Inventory: {totalEarnings}");
            UpdateSaleUI(); // UI 갱신
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
            UpdateTotalEarnings(); // UI를 열기 전에 정산금 갱신
            saleUI.SetActive(true);
            Debug.Log("판매창이 활성화되었습니다.");
        }
        else
        {
            Debug.LogWarning("판매창 UI가 설정되지 않았습니다!");
        }
    }

    // 수령 버튼 클릭 시 호출
    public void OnCollectEarningsButton()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Coin);
        // 소지금에 정산금 추가
        MoneyManager.Instance.AddCash(totalEarnings);
        Debug.Log($"Collected {totalEarnings} G. Current Cash: {MoneyManager.Instance.Cash}");

        // 날짜 증가
        currentDay++;
        PlayerPrefs.SetInt(DayKey, currentDay); // 날짜 저장
        totalEarnings = 0; // 총 정산금 초기화
        UpdateSaleUI(); // UI 갱신

        // 판매창 UI 비활성화
        if (saleUI != null)
        {
            saleUI.SetActive(false);
            Debug.Log("판매창이 비활성화되었습니다.");
        }
        else
        {
            Debug.LogWarning("판매창 UI가 설정되지 않았습니다!");
        }

        // a오브젝트 활성화
        if (aObject != null)
        {
            aObject.SetActive(true);
            Debug.Log($"{aObject.name} has been activated.");
        }
        else
        {
            Debug.LogWarning("aObject가 설정되지 않았습니다!");
        }
    }

    // 날짜를 1일차로 초기화하는 함수
    public void ResetDayToFirst()
    {
        currentDay = 1;
        PlayerPrefs.SetInt(DayKey, currentDay); // 날짜 저장
        UpdateSaleUI(); // UI 갱신
        Debug.Log("Day reset to 1.");
    }
}
