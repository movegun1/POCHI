using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetgunTabManager : MonoBehaviour
{
    public Button[] netgunButtons; // 5개의 버튼
    public TextMeshProUGUI[] buttonTexts; // 각 버튼의 텍스트
    public int[] unlockCosts = { 0, 9900, 49000, 129000, 599000 }; // 해금에 필요한 금액

    // 상태에 따라 색상을 변경할 패널들
    public Image[] rStatPanels; // RStatPanel
    public Image[] wPricePanels; // WPricePanel
    public Image[] backgroundPanels; // Background Panel

    private int currentEquippedIndex = -1; // 현재 장착 중인 버튼(-1은 없음)
    private ButtonState[] buttonStates; // 각 버튼의 상태

    private enum ButtonState
    {
        Locked,     // 해금 전 상태
        Unlocked,   // 해금된 상태
        Equipped    // 장착 중 상태
    }

    private void Start()
    {
        // 상태 배열 초기화
        buttonStates = new ButtonState[netgunButtons.Length];

        // 데이터 불러오기
        LoadState();

        // 버튼 초기화
        for (int i = 0; i < netgunButtons.Length; i++)
        {
            int index = i; // 로컬 변수로 캡처

            // 버튼 상태 업데이트
            UpdateButtonState(index);

            // 버튼 클릭 이벤트 추가
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
            SaveState(); // 상태 저장
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
        // 이전 장착 해제
        if (currentEquippedIndex != -1)
        {
            buttonStates[currentEquippedIndex] = ButtonState.Unlocked;
            UpdateButtonState(currentEquippedIndex);
        }

        // 새로 장착
        currentEquippedIndex = index;
        buttonStates[index] = ButtonState.Equipped;
        SaveState(); // 상태 저장
        UpdateButtonState(index);
    }

    private void UpdateButtonState(int index)
    {
        TextMeshProUGUI buttonText = buttonTexts[index];

        // 상태에 따른 색상 설정
        switch (buttonStates[index])
        {
            case ButtonState.Locked:
                buttonText.text = $"{unlockCosts[index]:N0}G"; // 금액 표시
                buttonText.color = new Color32(240, 206, 128, 255); // F0CE80

                SetPanelColors(index,
                    new Color32(7, 7, 7, 97), // 070707 알파값 97
                    new Color32(59, 59, 59, 170), // 3B3B3B 알파값 170
                    new Color32(0, 0, 0, 100) // 000000 알파값 100
                );
                netgunButtons[index].interactable = true; // 버튼 활성화
                break;

            case ButtonState.Unlocked:
                buttonText.text = "장착";
                buttonText.color = Color.white; // 흰색

                SetPanelColors(index,
                    new Color32(7, 7, 7, 97), // 070707 알파값 97
                    new Color32(59, 59, 59, 170), // 3B3B3B 알파값 170
                    new Color32(0, 0, 0, 100) // 000000 알파값 100
                );
                netgunButtons[index].interactable = true; // 버튼 활성화
                break;

            case ButtonState.Equipped:
                buttonText.text = "장착 중";
                buttonText.color = Color.black; // 검은색

                SetPanelColors(index,
                    new Color32(255, 255, 255, 40), // FFFFFF 알파값 40
                    new Color32(191, 191, 191, 170), // BFBFBF 알파값 170
                    new Color32(255, 255, 255, 10) // FFFFFF 알파값 10
                );
                netgunButtons[index].interactable = false; // 비활성화
                break;
        }
    }

    // 패널 색상 설정 메서드
    private void SetPanelColors(int index, Color rStatColor, Color wPriceColor, Color backgroundColor)
    {
        rStatPanels[index].color = rStatColor;
        wPricePanels[index].color = wPriceColor;
        backgroundPanels[index].color = backgroundColor;
    }

    // 상태 저장
    private void SaveState()
    {
        for (int i = 0; i < buttonStates.Length; i++)
        {
            // 각 버튼의 상태 저장
            PlayerPrefs.SetInt($"NetgunButton{i}State", (int)buttonStates[i]);
        }

        // 현재 장착된 버튼 저장
        PlayerPrefs.SetInt("NetgunEquippedIndex", currentEquippedIndex);

        PlayerPrefs.Save(); // 저장 적용
    }

    // 상태 불러오기
    private void LoadState()
    {
        for (int i = 0; i < buttonStates.Length; i++)
        {
            // 버튼 상태 불러오기 (기본값: Locked)
            buttonStates[i] = (ButtonState)PlayerPrefs.GetInt($"NetgunButton{i}State", (i == 0 ? (int)ButtonState.Unlocked : (int)ButtonState.Locked));
        }

        // 현재 장착된 버튼 불러오기 (기본값: -1)
        currentEquippedIndex = PlayerPrefs.GetInt("NetgunEquippedIndex", -1);
    }
}
