using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatchManager : MonoBehaviour
{
    public GameObject captureUIPanel; // 포획 UI 패널
    public TMP_Text animalNameText; // 동물 이름 텍스트
    public Image animalImage; // 동물 이미지
    public TMP_Text animalPriceText; // 동물 가격 텍스트

    // 포획 UI를 업데이트하는 메서드
    public void UpdateCaptureUI(Item capturedItem)
    {
        if (captureUIPanel != null)
        {
            // UI 패널 활성화
            captureUIPanel.SetActive(true);

            // 텍스트 및 이미지 업데이트
            animalNameText.text = capturedItem.itemName;
            animalImage.sprite = capturedItem.itemImage;
            animalImage.preserveAspect = true; // 이미지 비율 유지
            animalPriceText.text = $"{capturedItem.price:N0} G"; // 가격을 포맷하여 표시
        }
        else
        {
            Debug.LogWarning("Capture UI Panel is not assigned.");
        }
    }

    // 포획 UI를 숨기는 메서드
    public void HideCaptureUI()
    {
        if (captureUIPanel != null)
        {
            captureUIPanel.SetActive(false);
        }
    }
}
