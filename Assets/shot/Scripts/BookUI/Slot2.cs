using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot2 : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemImage; // ���� ���� ItemImage (�ڽ� Image ������Ʈ)
    private Item currentItem; // ���Կ� ����� ������ ������

    private RectTransform slotRectTransform; // ���� RectTransform
    private RectTransform imageRectTransform; // �̹��� RectTransform

    private void Awake()
    {
        if (itemImage == null)
        {
            itemImage = GetComponentInChildren<Image>(); // �ڽ� Image �ڵ� Ž��
        }

        slotRectTransform = GetComponent<RectTransform>();
        imageRectTransform = itemImage.GetComponent<RectTransform>();
    }

    // ���Կ� �������� �����ϴ� �޼���
    public void SetItem(Item item)
    {
        currentItem = item;

        if (item != null && item.itemImage != null)
        {
            itemImage.sprite = item.itemImage; // �̹��� ����
            itemImage.color = Color.white; // �̹��� ���� �ʱ�ȭ
            AdjustImageToFitSlot(item.itemImage); // �̹��� ���� ���� �� ���� ũ�� ����
        }
        else
        {
            itemImage.sprite = null; // �̹��� ����
            itemImage.color = new Color(1, 1, 1, 0); // �����ϰ� ó��
        }
    }

    // �̹��� ������ �����ϸ鼭 ���� ũ�⿡ �� ������ ����
    private void AdjustImageToFitSlot(Sprite sprite)
    {
        if (sprite == null || slotRectTransform == null || imageRectTransform == null)
        {
            return;
        }

        // ���� ũ�� ��������
        float slotWidth = slotRectTransform.rect.width;
        float slotHeight = slotRectTransform.rect.height;

        // �̹����� ���� ���� ���
        float spriteWidth = sprite.rect.width;
        float spriteHeight = sprite.rect.height;
        float spriteAspectRatio = spriteWidth / spriteHeight;

        // ������ ���� ���
        float slotAspectRatio = slotWidth / slotHeight;

        // ���� ũ�⿡ �°� �̹��� ũ�� ����
        if (spriteAspectRatio > slotAspectRatio)
        {
            // �̹����� ���η� �� �� ���: ������ �ʺ� ����
            imageRectTransform.sizeDelta = new Vector2(slotWidth, slotWidth / spriteAspectRatio);
        }
        else
        {
            // �̹����� ���η� �� ��ų� ������ ���� ���: ������ ���̿� ����
            imageRectTransform.sizeDelta = new Vector2(slotHeight * spriteAspectRatio, slotHeight);
        }
    }

    // ���� Ŭ�� �̺�Ʈ ó��
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Slot clicked!");

        if (currentItem != null)
        {
            UIManager2.instance.ShowItemDetails(currentItem); // ������ ���� ���� ǥ��
        }
        else
        {
            Debug.LogWarning("No item assigned to this slot!");
        }
    }
}
