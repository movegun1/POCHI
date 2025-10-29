using UnityEngine;
using TMPro;
using System.Globalization; // ���� ���˿�

public class StatUIManager : MonoBehaviour
{
    public GameObject statPanel; // ���� UI �г�
    public GameObject homeUI; // Ȩ UI �г�

    // �ؽ�Ʈ UI
    public TextMeshProUGUI currentMoneyText; // ������ ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI netLevelText, netPriceText, netRealStatText; // �׹�
    public TextMeshProUGUI ropeLevelText, ropePriceText, ropeRealStatText; // ����
    public TextMeshProUGUI launcherLevelText, launcherPriceText, launcherRealStatText; // �߻��

    private int netLevel, ropeLevel, launcherLevel; // ���� ����
    private float netPrice, ropePrice, launcherPrice; // ���� ����

    private float netStat, ropeStat, launcherStat; // ���� �ɷ�ġ

    private void Start()
    {
        LoadStats(); // ����� ���� �ҷ�����
        UpdateUI();  // UI �ʱ�ȭ
        statPanel.SetActive(false); // ���� UI�� �⺻������ ����
        homeUI.SetActive(true); // Ȩ UI�� �⺻������ ����

    }

    private void Update()
    {
        UpdateMoneyDisplay(); // ������ ǥ�� �ǽð� ������Ʈ
    }
    public void OpenStatUI()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        statPanel.SetActive(true); // ���� UI Ȱ��ȭ
        homeUI.SetActive(false); // Ȩ UI ��Ȱ��ȭ
    }

    // ���� UI �ݱ�
    public void CloseStatUI()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        statPanel.SetActive(false); // ���� UI ��Ȱ��ȭ
        homeUI.SetActive(true); // Ȩ UI Ȱ��ȭ
    }

    // ���� UI ���
    public void ToggleStatUI()
    {
        statPanel.SetActive(!statPanel.activeSelf);
    }

    // �׹� ���� ��
    public void LevelUpNet()
    {
        if (MoneyManager.Instance.Cash >= netPrice)
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Coin);
            MoneyManager.Instance.SubtractCash((int)netPrice);
            netLevel++;
            netPrice += netPrice / 13f;
            netStat = netLevel; // �ɷ�ġ: 1���� 1�� ����
            SaveStats();
            UpdateUI();
        }
        else
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Error);
        }
    }

    // ���� ���� ��
    public void LevelUpRope()
    {
        if (MoneyManager.Instance.Cash >= ropePrice)
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Coin);
            MoneyManager.Instance.SubtractCash((int)ropePrice);
            ropeLevel++;
            ropePrice += ropePrice / 8.2f;
            ropeStat = 3f + (ropeLevel - 1) * 0.5f; // �ɷ�ġ: 3���� ����, 0.5�� ����
            SaveStats();
            UpdateUI();
        }
        else
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Error);
        }
    }

    // �߻�� ���� ��
    public void LevelUpLauncher()
    {
        if (MoneyManager.Instance.Cash >= launcherPrice)
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Coin);
            MoneyManager.Instance.SubtractCash((int)launcherPrice);
            launcherLevel++;
            launcherPrice += launcherPrice / 18f;
            launcherStat = 1f + (launcherLevel - 2) * 1f; // �ɷ�ġ: 1���� ����, 1�� ����
            SaveStats();
            UpdateUI();
        }
        else
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Error);
        }
    }

    // �� �� ���� �ʱ�ȭ
    // �� �� ���� �ʱ�ȭ
    public void ResetStats()
    {
        // MoneyManager �ʱ�ȭ
        MoneyManager.Instance.ResetCash();

        // ���� �ʱ�ȭ
        netLevel = 1;
        netPrice = 300f;
        netStat = 1f;

        ropeLevel = 1;
        ropePrice = 500f;
        ropeStat = 3f;

        launcherLevel = 2;
        launcherPrice = 1000f;
        launcherStat = 1f;

        // ���� ���� �ʱ�ȭ
        ResetShopState();

        SaveStats(); // �ʱ�ȭ�� ���� ����
        UpdateUI();  // UI�� �ʱ� ���·� ������Ʈ
    }

    // ���� ���� �ʱ�ȭ
    private void ResetShopState()
    {
        const int NumItems = 5; // ������ ��ư ����
        const string LentonKey = "LentonButton";
        const string NetgunKey = "NetgunButton";
        const string LentonEquippedKey = "LentonEquippedIndex";
        const string NetgunEquippedKey = "NetgunEquippedIndex";

        // LentonTab �ʱ�ȭ
        for (int i = 0; i < NumItems; i++)
        {
            PlayerPrefs.SetInt($"{LentonKey}{i}State", (i == 0 ? 1 : 0)); // ù ��° ��ư�� Unlocked
        }
        PlayerPrefs.SetInt(LentonEquippedKey, -1); // Lenton ���� ���� �ʱ�ȭ

        // NetgunTab �ʱ�ȭ
        for (int i = 0; i < NumItems; i++)
        {
            PlayerPrefs.SetInt($"{NetgunKey}{i}State", (i == 0 ? 1 : 0)); // ù ��° ��ư�� Unlocked
        }
        PlayerPrefs.SetInt(NetgunEquippedKey, -1); // Netgun ���� ���� �ʱ�ȭ

        PlayerPrefs.Save(); // ���� ���� ����
    }

    // ����� ���� �ҷ�����
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

    // ���� ����
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

    // UI ������Ʈ
    private void UpdateUI()
    {
        UpdateMoneyDisplay(); // ������ ������Ʈ

        // ���� ����: 3�ڸ����� �޸� �߰�
        string FormatPrice(float price)
        {
            return Mathf.Ceil(price).ToString("N0", CultureInfo.InvariantCulture);
        }

        // �׹�
        netLevelText.text = $"Lv {netLevel}";
        netPriceText.text = $"{FormatPrice(netPrice)}G";
        netRealStatText.text = $"{netStat}";

        // ����
        ropeLevelText.text = $"Lv {ropeLevel}";
        ropePriceText.text = $"{FormatPrice(ropePrice)}G";
        ropeRealStatText.text = $"{ropeStat:F1}";

        // �߻��
        launcherLevelText.text = $"Lv {launcherLevel}";
        launcherPriceText.text = $"{FormatPrice(launcherPrice)}G";
        launcherRealStatText.text = $"+{launcherStat:F0}%";
    }

    // ������ �ؽ�Ʈ ������Ʈ
    private void UpdateMoneyDisplay()
    {
        // ������ ����
        string FormatMoney(int money)
        {
            return money.ToString("N0", CultureInfo.InvariantCulture);
        }
        currentMoneyText.text = $"{FormatMoney(MoneyManager.Instance.Cash)}G";
    }
}
