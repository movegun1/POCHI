using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    // UI 패널
    public GameObject shopPanel; // 상점 UI 패널
    public GameObject homeUI;    // 홈 UI 패널

    // 탭 패널
    public GameObject lentonTab; // Lenton 탭
    public GameObject netgunTab; // Netgun 탭

    // 탭 버튼 텍스트
    public TextMeshProUGUI lentonTabText;  // Lenton 탭 텍스트
    public TextMeshProUGUI netgunTabText;  // Netgun 탭 텍스트

    // 소지금 표시
    public TextMeshProUGUI currentMoneyText; // 소지금 표시 텍스트

    private void Start()
    {
        // 초기 상태 설정
        shopPanel.SetActive(false); // 상점 UI는 기본적으로 꺼짐
        homeUI.SetActive(true);     // 홈 UI는 기본적으로 켜짐
        UpdateMoneyDisplay();       // 소지금 UI 초기화

        OpenLentonTab();            // 기본적으로 Lenton 탭 열림
    }

    private void Update()
    {
        // 상점이 활성화되어 있을 때만 소지금을 실시간 업데이트
        if (shopPanel.activeSelf)
        {
            UpdateMoneyDisplay();
        }
    }

    // 상점 열기
    public void OpenShop()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        shopPanel.SetActive(true);  // 상점 UI 활성화
        homeUI.SetActive(false);   // 홈 UI 비활성화
        UpdateMoneyDisplay();      // 소지금 표시 업데이트
    }

    // 상점 닫기
    public void CloseShop()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        shopPanel.SetActive(false); // 상점 UI 비활성화
        homeUI.SetActive(true);     // 홈 UI 활성화
    }

    // Lenton 탭 열기
    public void OpenLentonTab()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        lentonTab.SetActive(true);  // Lenton 탭 활성화
        netgunTab.SetActive(false); // Netgun 탭 비활성화

        // 텍스트 색상 변경
        lentonTabText.color = Color.white; // 흰색
        netgunTabText.color = new Color32(0, 0, 0, 255); // 323232
    }

    // Netgun 탭 열기
    public void OpenNetgunTab()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        lentonTab.SetActive(false); // Lenton 탭 비활성화
        netgunTab.SetActive(true);  // Netgun 탭 활성화

        // 텍스트 색상 변경
        netgunTabText.color = Color.white; // 흰색
        lentonTabText.color = new Color32(0, 0, 0, 255); // 323232
    }

    // 소지금 텍스트 업데이트
    private void UpdateMoneyDisplay()
    {
        if (currentMoneyText != null)
        {
            currentMoneyText.text = $"{MoneyManager.Instance.Cash:N0} G"; // 3자리 콤마 추가
        }
    }
}
