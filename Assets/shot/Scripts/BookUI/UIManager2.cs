using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�

public class UIManager2 : MonoBehaviour
{
    public static UIManager2 instance; // �̱��� ����
    [SerializeField] private GameObject targetUI; // Ȱ��ȭ/��Ȱ��ȭ�� ��� UI

    [Header("UI Elements")]
    [SerializeField] private GameObject detailPanel; // ������ �� ������ ǥ���� �г�
    [SerializeField] private Image itemDetailImage; // ������ �� �̹���
    [SerializeField] private TMP_Text itemNameText; // ������ �̸� �ؽ�Ʈ
    [SerializeField] private TMP_Text itemPriceText; // ������ ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text itemDescriptionText; // ���� �� ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text itemRealDescriptionText; // ���� ���� �ؽ�Ʈ

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ� ����
        }
    }

    // ������ ������ UI�� ǥ���ϰ� �г��� Ȱ��ȭ
    public void ShowItemDetails(Item item)
    {
        if (targetUI != null)
        {
            // ���� ���¸� �����Ͽ� Ȱ��ȭ/��Ȱ��ȭ
            targetUI.SetActive(!targetUI.activeSelf);
        }

        if (item == null) return;

        // UI ��ҿ� ������ ������ ����
        if (item.itemImage != null)
        {
            itemDetailImage.sprite = item.itemImage;
            itemDetailImage.color = Color.white; // �̹��� ǥ��
            itemDetailImage.preserveAspect = true; // �̹��� ���� ����
        }
        else
        {
            itemDetailImage.sprite = null;
            itemDetailImage.color = new Color(1, 1, 1, 0); // �̹��� ��Ȱ��ȭ
        }

        itemNameText.text = item.itemName;
        itemPriceText.text = $"{item.price:N0} G"; // õ ���� ��ǥ �߰�
        itemDescriptionText.text = item.itemText;
        itemRealDescriptionText.text = item.itemRealText;

        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        // �г� Ȱ��ȭ
        detailPanel.SetActive(true);
    }
}
