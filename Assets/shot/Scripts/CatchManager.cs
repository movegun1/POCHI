using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatchManager : MonoBehaviour
{
    public GameObject captureUIPanel; // ��ȹ UI �г�
    public TMP_Text animalNameText; // ���� �̸� �ؽ�Ʈ
    public Image animalImage; // ���� �̹���
    public TMP_Text animalPriceText; // ���� ���� �ؽ�Ʈ

    // ��ȹ UI�� ������Ʈ�ϴ� �޼���
    public void UpdateCaptureUI(Item capturedItem)
    {
        if (captureUIPanel != null)
        {
            // UI �г� Ȱ��ȭ
            captureUIPanel.SetActive(true);

            // �ؽ�Ʈ �� �̹��� ������Ʈ
            animalNameText.text = capturedItem.itemName;
            animalImage.sprite = capturedItem.itemImage;
            animalImage.preserveAspect = true; // �̹��� ���� ����
            animalPriceText.text = $"{capturedItem.price:N0} G"; // ������ �����Ͽ� ǥ��
        }
        else
        {
            Debug.LogWarning("Capture UI Panel is not assigned.");
        }
    }

    // ��ȹ UI�� ����� �޼���
    public void HideCaptureUI()
    {
        if (captureUIPanel != null)
        {
            captureUIPanel.SetActive(false);
        }
    }
}
