using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    // UI �г�
    public GameObject shopPanel; // ���� UI �г�
    public GameObject homeUI;    // Ȩ UI �г�

    // �� �г�
    public GameObject lentonTab; // Lenton ��
    public GameObject netgunTab; // Netgun ��

    // �� ��ư �ؽ�Ʈ
    public TextMeshProUGUI lentonTabText;  // Lenton �� �ؽ�Ʈ
    public TextMeshProUGUI netgunTabText;  // Netgun �� �ؽ�Ʈ

    // ������ ǥ��
    public TextMeshProUGUI currentMoneyText; // ������ ǥ�� �ؽ�Ʈ

    private void Start()
    {
        // �ʱ� ���� ����
        shopPanel.SetActive(false); // ���� UI�� �⺻������ ����
        homeUI.SetActive(true);     // Ȩ UI�� �⺻������ ����
        UpdateMoneyDisplay();       // ������ UI �ʱ�ȭ

        OpenLentonTab();            // �⺻������ Lenton �� ����
    }

    private void Update()
    {
        // ������ Ȱ��ȭ�Ǿ� ���� ���� �������� �ǽð� ������Ʈ
        if (shopPanel.activeSelf)
        {
            UpdateMoneyDisplay();
        }
    }

    // ���� ����
    public void OpenShop()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        shopPanel.SetActive(true);  // ���� UI Ȱ��ȭ
        homeUI.SetActive(false);   // Ȩ UI ��Ȱ��ȭ
        UpdateMoneyDisplay();      // ������ ǥ�� ������Ʈ
    }

    // ���� �ݱ�
    public void CloseShop()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        shopPanel.SetActive(false); // ���� UI ��Ȱ��ȭ
        homeUI.SetActive(true);     // Ȩ UI Ȱ��ȭ
    }

    // Lenton �� ����
    public void OpenLentonTab()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        lentonTab.SetActive(true);  // Lenton �� Ȱ��ȭ
        netgunTab.SetActive(false); // Netgun �� ��Ȱ��ȭ

        // �ؽ�Ʈ ���� ����
        lentonTabText.color = Color.white; // ���
        netgunTabText.color = new Color32(0, 0, 0, 255); // 323232
    }

    // Netgun �� ����
    public void OpenNetgunTab()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        lentonTab.SetActive(false); // Lenton �� ��Ȱ��ȭ
        netgunTab.SetActive(true);  // Netgun �� Ȱ��ȭ

        // �ؽ�Ʈ ���� ����
        netgunTabText.color = Color.white; // ���
        lentonTabText.color = new Color32(0, 0, 0, 255); // 323232
    }

    // ������ �ؽ�Ʈ ������Ʈ
    private void UpdateMoneyDisplay()
    {
        if (currentMoneyText != null)
        {
            currentMoneyText.text = $"{MoneyManager.Instance.Cash:N0} G"; // 3�ڸ� �޸� �߰�
        }
    }
}
