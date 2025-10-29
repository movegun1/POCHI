using UnityEngine;
using UnityEngine.UI;

public class LanternManager : MonoBehaviour
{
    [System.Serializable]
    public class LanternItem
    {
        public Button button; // Lantern ��ư
        public bool isEquipped; // ��ư�� Ȱ��ȭ�Ǿ����� ����
        public Sprite associatedSprite; // ��ư�� ����� ��������Ʈ
    }

    public LanternItem[] lanternItems; // Lantern �׸� �迭
    public GameObject targetObject; // �̹����� ����� ��� ������Ʈ

    private SpriteRenderer spriteRenderer; // ��� ������Ʈ�� SpriteRenderer
    private const string EquippedLanternKey = "EquippedLanternIndex"; // ���� Ű

    private void Start()
    {
        // ��� ������Ʈ���� SpriteRenderer ��������
        if (targetObject != null)
        {
            spriteRenderer = targetObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("Target object does not have a SpriteRenderer component.");
                return;
            }
        }
        else
        {
            Debug.LogError("Target object is not assigned.");
            return;
        }

        // �� ��ư�� Ŭ�� ������ �߰�
        for (int i = 0; i < lanternItems.Length; i++)
        {
            int index = i; // ���� ������ ĸó
            lanternItems[i].button.onClick.AddListener(() => OnLanternButtonClicked(index));
        }

        // �ʱ� ���� ���� �ε� �� �ݿ�
        LoadEquippedLantern();
    }

    public void OnLanternButtonClicked(int index)
    {
        // ��� ��ư ���� �ʱ�ȭ
        for (int i = 0; i < lanternItems.Length; i++)
        {
            lanternItems[i].isEquipped = (i == index); // Ŭ���� ��ư�� Ȱ��ȭ
        }

        // ���� ���� ���� ����
        SaveEquippedLantern(index);

        // Ȱ��ȭ�� ��ư�� ���� ��������Ʈ ����
        UpdateTargetSprite();
    }

    private void UpdateTargetSprite()
    {
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer is not assigned.");
            return;
        }

        bool updated = false;

        foreach (var item in lanternItems)
        {
            if (item.isEquipped && item.associatedSprite != null)
            {
                if (spriteRenderer.sprite != item.associatedSprite) // �ߺ� �۾� ����
                {
                    spriteRenderer.sprite = item.associatedSprite;
                    Debug.Log($"Sprite updated to: {item.associatedSprite.name}");
                }
                updated = true;
                break;
            }
        }

        if (!updated)
        {
            Debug.LogWarning("No equipped item or associated sprite found.");
        }
    }

    private void SaveEquippedLantern(int index)
    {
        PlayerPrefs.SetInt(EquippedLanternKey, index);
        PlayerPrefs.Save();
        Debug.Log($"Equipped lantern saved: {index}");
    }

    private void LoadEquippedLantern()
    {
        int equippedIndex = PlayerPrefs.GetInt(EquippedLanternKey, 0); // �⺻��: ù ��° ���
        Debug.Log($"Loaded equipped lantern: {equippedIndex}");

        for (int i = 0; i < lanternItems.Length; i++)
        {
            lanternItems[i].isEquipped = (i == equippedIndex);
        }

        // �ʱ� ���� �ݿ�
        UpdateTargetSprite();
    }
}
