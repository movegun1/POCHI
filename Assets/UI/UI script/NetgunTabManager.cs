using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetgunTabManager : MonoBehaviour
{
    public Button[] netgunButtons; // 5���� ��ư
    public TextMeshProUGUI[] buttonTexts; // �� ��ư�� �ؽ�Ʈ
    public int[] unlockCosts = { 0, 9900, 49000, 129000, 599000 }; // �رݿ� �ʿ��� �ݾ�

    // ���¿� ���� ������ ������ �гε�
    public Image[] rStatPanels; // RStatPanel
    public Image[] wPricePanels; // WPricePanel
    public Image[] backgroundPanels; // Background Panel

    private int currentEquippedIndex = -1; // ���� ���� ���� ��ư(-1�� ����)
    private ButtonState[] buttonStates; // �� ��ư�� ����

    private enum ButtonState
    {
        Locked,     // �ر� �� ����
        Unlocked,   // �رݵ� ����
        Equipped    // ���� �� ����
    }

    private void Start()
    {
        // ���� �迭 �ʱ�ȭ
        buttonStates = new ButtonState[netgunButtons.Length];

        // ������ �ҷ�����
        LoadState();

        // ��ư �ʱ�ȭ
        for (int i = 0; i < netgunButtons.Length; i++)
        {
            int index = i; // ���� ������ ĸó

            // ��ư ���� ������Ʈ
            UpdateButtonState(index);

            // ��ư Ŭ�� �̺�Ʈ �߰�
            netgunButtons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    private void OnButtonClick(int index)
    {
        switch (buttonStates[index])
        {
            case ButtonState.Locked:
                TryUnlock(index);
                break;
            case ButtonState.Unlocked:
                Equip(index);
                break;
            case ButtonState.Equipped:
                Debug.Log($"Button {index + 1} is already equipped!");
                break;
        }
    }

    private void TryUnlock(int index)
    {
        int cost = unlockCosts[index];

        if (MoneyManager.Instance.Cash >= cost)
        {
            MoneyManager.Instance.SubtractCash(cost);
            buttonStates[index] = ButtonState.Unlocked;
            SaveState(); // ���� ����
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Coin);
            UpdateButtonState(index);
        }
        else
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Error);
            Debug.Log("Not enough money to unlock this item!");
        }
    }

    private void Equip(int index)
    {
        // ���� ���� ����
        if (currentEquippedIndex != -1)
        {
            buttonStates[currentEquippedIndex] = ButtonState.Unlocked;
            UpdateButtonState(currentEquippedIndex);
        }

        // ���� ����
        currentEquippedIndex = index;
        buttonStates[index] = ButtonState.Equipped;
        SaveState(); // ���� ����
        UpdateButtonState(index);
    }

    private void UpdateButtonState(int index)
    {
        TextMeshProUGUI buttonText = buttonTexts[index];

        // ���¿� ���� ���� ����
        switch (buttonStates[index])
        {
            case ButtonState.Locked:
                buttonText.text = $"{unlockCosts[index]:N0}G"; // �ݾ� ǥ��
                buttonText.color = new Color32(240, 206, 128, 255); // F0CE80

                SetPanelColors(index,
                    new Color32(7, 7, 7, 97), // 070707 ���İ� 97
                    new Color32(59, 59, 59, 170), // 3B3B3B ���İ� 170
                    new Color32(0, 0, 0, 100) // 000000 ���İ� 100
                );
                netgunButtons[index].interactable = true; // ��ư Ȱ��ȭ
                break;

            case ButtonState.Unlocked:
                buttonText.text = "����";
                buttonText.color = Color.white; // ���

                SetPanelColors(index,
                    new Color32(7, 7, 7, 97), // 070707 ���İ� 97
                    new Color32(59, 59, 59, 170), // 3B3B3B ���İ� 170
                    new Color32(0, 0, 0, 100) // 000000 ���İ� 100
                );
                netgunButtons[index].interactable = true; // ��ư Ȱ��ȭ
                break;

            case ButtonState.Equipped:
                buttonText.text = "���� ��";
                buttonText.color = Color.black; // ������

                SetPanelColors(index,
                    new Color32(255, 255, 255, 40), // FFFFFF ���İ� 40
                    new Color32(191, 191, 191, 170), // BFBFBF ���İ� 170
                    new Color32(255, 255, 255, 10) // FFFFFF ���İ� 10
                );
                netgunButtons[index].interactable = false; // ��Ȱ��ȭ
                break;
        }
    }

    // �г� ���� ���� �޼���
    private void SetPanelColors(int index, Color rStatColor, Color wPriceColor, Color backgroundColor)
    {
        rStatPanels[index].color = rStatColor;
        wPricePanels[index].color = wPriceColor;
        backgroundPanels[index].color = backgroundColor;
    }

    // ���� ����
    private void SaveState()
    {
        for (int i = 0; i < buttonStates.Length; i++)
        {
            // �� ��ư�� ���� ����
            PlayerPrefs.SetInt($"NetgunButton{i}State", (int)buttonStates[i]);
        }

        // ���� ������ ��ư ����
        PlayerPrefs.SetInt("NetgunEquippedIndex", currentEquippedIndex);

        PlayerPrefs.Save(); // ���� ����
    }

    // ���� �ҷ�����
    private void LoadState()
    {
        for (int i = 0; i < buttonStates.Length; i++)
        {
            // ��ư ���� �ҷ����� (�⺻��: Locked)
            buttonStates[i] = (ButtonState)PlayerPrefs.GetInt($"NetgunButton{i}State", (i == 0 ? (int)ButtonState.Unlocked : (int)ButtonState.Locked));
        }

        // ���� ������ ��ư �ҷ����� (�⺻��: -1)
        currentEquippedIndex = PlayerPrefs.GetInt("NetgunEquippedIndex", -1);
    }
}
