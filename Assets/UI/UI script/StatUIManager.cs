using UnityEngine;
using TMPro;
using System.Globalization; // 숫자 포맷용

public class StatUIManager : MonoBehaviour
{
    public GameObject statPanel; // 스텟 UI 패널
    public GameObject homeUI; // 홈 UI 패널

    // 텍스트 UI
    public TextMeshProUGUI currentMoneyText; // 소지금 표시 텍스트
    public TextMeshProUGUI netLevelText, netPriceText, netRealStatText; // 그물
    public TextMeshProUGUI ropeLevelText, ropePriceText, ropeRealStatText; // 로프
    public TextMeshProUGUI launcherLevelText, launcherPriceText, launcherRealStatText; // 발사대

    private int netLevel, ropeLevel, launcherLevel; // 스텟 레벨
    private float netPrice, ropePrice, launcherPrice; // 스텟 가격

    private float netStat, ropeStat, launcherStat; // 스텟 능력치

    private void Start()
    {
        LoadStats(); // 저장된 스텟 불러오기
        UpdateUI();  // UI 초기화
        statPanel.SetActive(false); // 스텟 UI는 기본적으로 꺼짐
        homeUI.SetActive(true); // 홈 UI는 기본적으로 켜짐

    }

    private void Update()
    {
        UpdateMoneyDisplay(); // 소지금 표시 실시간 업데이트
    }
    public void OpenStatUI()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        statPanel.SetActive(true); // 스텟 UI 활성화
        homeUI.SetActive(false); // 홈 UI 비활성화
    }

    // 스텟 UI 닫기
    public void CloseStatUI()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        statPanel.SetActive(false); // 스텟 UI 비활성화
        homeUI.SetActive(true); // 홈 UI 활성화
    }

    // 스텟 UI 토글
    public void ToggleStatUI()
    {
        statPanel.SetActive(!statPanel.activeSelf);
    }

    // 그물 레벨 업
    public void LevelUpNet()
    {
        if (MoneyManager.Instance.Cash >= netPrice)
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Coin);
            MoneyManager.Instance.SubtractCash((int)netPrice);
            netLevel++;
            netPrice += netPrice / 13f;
            netStat = netLevel; // 능력치: 1렙당 1씩 증가
            SaveStats();
            UpdateUI();
        }
        else
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Error);
        }
    }

    // 로프 레벨 업
    public void LevelUpRope()
    {
        if (MoneyManager.Instance.Cash >= ropePrice)
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Coin);
            MoneyManager.Instance.SubtractCash((int)ropePrice);
            ropeLevel++;
            ropePrice += ropePrice / 8.2f;
            ropeStat = 3f + (ropeLevel - 1) * 0.5f; // 능력치: 3부터 시작, 0.5씩 증가
            SaveStats();
            UpdateUI();
        }
        else
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Error);
        }
    }

    // 발사대 레벨 업
    public void LevelUpLauncher()
    {
        if (MoneyManager.Instance.Cash >= launcherPrice)
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Coin);
            MoneyManager.Instance.SubtractCash((int)launcherPrice);
            launcherLevel++;
            launcherPrice += launcherPrice / 18f;
            launcherStat = 1f + (launcherLevel - 2) * 1f; // 능력치: 1부터 시작, 1씩 증가
            SaveStats();
            UpdateUI();
        }
        else
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Error);
        }
    }

    // 돈 및 스텟 초기화
    // 돈 및 스텟 초기화
    public void ResetStats()
    {
        // MoneyManager 초기화
        MoneyManager.Instance.ResetCash();

        // 스탯 초기화
        netLevel = 1;
        netPrice = 300f;
        netStat = 1f;

        ropeLevel = 1;
        ropePrice = 500f;
        ropeStat = 3f;

        launcherLevel = 2;
        launcherPrice = 1000f;
        launcherStat = 1f;

        // 상점 상태 초기화
        ResetShopState();

        SaveStats(); // 초기화된 값을 저장
        UpdateUI();  // UI를 초기 상태로 업데이트
    }

    // 상점 상태 초기화
    private void ResetShopState()
    {
        const int NumItems = 5; // 상점의 버튼 개수
        const string LentonKey = "LentonButton";
        const string NetgunKey = "NetgunButton";
        const string LentonEquippedKey = "LentonEquippedIndex";
        const string NetgunEquippedKey = "NetgunEquippedIndex";

        // LentonTab 초기화
        for (int i = 0; i < NumItems; i++)
        {
            PlayerPrefs.SetInt($"{LentonKey}{i}State", (i == 0 ? 1 : 0)); // 첫 번째 버튼만 Unlocked
        }
        PlayerPrefs.SetInt(LentonEquippedKey, -1); // Lenton 장착 상태 초기화

        // NetgunTab 초기화
        for (int i = 0; i < NumItems; i++)
        {
            PlayerPrefs.SetInt($"{NetgunKey}{i}State", (i == 0 ? 1 : 0)); // 첫 번째 버튼만 Unlocked
        }
        PlayerPrefs.SetInt(NetgunEquippedKey, -1); // Netgun 장착 상태 초기화

        PlayerPrefs.Save(); // 변경 사항 저장
    }

    // 저장된 스텟 불러오기
    private void LoadStats()
    {
        netLevel = PlayerPrefs.GetInt("NetLevel", 1);
        netPrice = PlayerPrefs.GetFloat("NetPrice", 300f);
        netStat = netLevel;

        ropeLevel = PlayerPrefs.GetInt("RopeLevel", 1);
        ropePrice = PlayerPrefs.GetFloat("RopePrice", 500f);
        ropeStat = 3f + (ropeLevel - 1) * 0.5f;

        launcherLevel = PlayerPrefs.GetInt("LauncherLevel", 2);
        launcherPrice = PlayerPrefs.GetFloat("LauncherPrice", 1000f);
        launcherStat = 1f + (launcherLevel - 2) * 1f;
    }

    // 스텟 저장
    private void SaveStats()
    {
        PlayerPrefs.SetInt("NetLevel", netLevel);
        PlayerPrefs.SetFloat("NetPrice", netPrice);

        PlayerPrefs.SetInt("RopeLevel", ropeLevel);
        PlayerPrefs.SetFloat("RopePrice", ropePrice);

        PlayerPrefs.SetInt("LauncherLevel", launcherLevel);
        PlayerPrefs.SetFloat("LauncherPrice", launcherPrice);

        PlayerPrefs.Save();
    }

    // UI 업데이트
    private void UpdateUI()
    {
        UpdateMoneyDisplay(); // 소지금 업데이트

        // 숫자 포맷: 3자리마다 콤마 추가
        string FormatPrice(float price)
        {
            return Mathf.Ceil(price).ToString("N0", CultureInfo.InvariantCulture);
        }

        // 그물
        netLevelText.text = $"Lv {netLevel}";
        netPriceText.text = $"{FormatPrice(netPrice)}G";
        netRealStatText.text = $"{netStat}";

        // 로프
        ropeLevelText.text = $"Lv {ropeLevel}";
        ropePriceText.text = $"{FormatPrice(ropePrice)}G";
        ropeRealStatText.text = $"{ropeStat:F1}";

        // 발사대
        launcherLevelText.text = $"Lv {launcherLevel}";
        launcherPriceText.text = $"{FormatPrice(launcherPrice)}G";
        launcherRealStatText.text = $"+{launcherStat:F0}%";
    }

    // 소지금 텍스트 업데이트
    private void UpdateMoneyDisplay()
    {
        // 소지금 포맷
        string FormatMoney(int money)
        {
            return money.ToString("N0", CultureInfo.InvariantCulture);
        }
        currentMoneyText.text = $"{FormatMoney(MoneyManager.Instance.Cash)}G";
    }
}
