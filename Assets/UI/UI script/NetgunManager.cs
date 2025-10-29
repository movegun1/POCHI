using UnityEngine;
using UnityEngine.UI;

public class NetgunManager : MonoBehaviour
{
    [System.Serializable]
    public class NetgunItem
    {
        public Button button; // Netgun ��ư
        public bool isEquipped; // ��ư�� Ȱ��ȭ�Ǿ����� ����
        public Sprite associatedSprite; // ��ư�� ����� ��������Ʈ
    }

    public NetgunItem[] netgunItems; // Netgun �׸� �迭
    public GameObject targetObject; // �̹����� ����� ��� ������Ʈ

    private SpriteRenderer spriteRenderer; // ��� ������Ʈ�� SpriteRenderer
    private const string EquippedNetgunKey = "EquippedNetgunIndex"; // ���� Ű

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
        for (int i = 0; i < netgunItems.Length; i++)
        {
            int index = i; // ���� ������ ĸó
            netgunItems[i].button.onClick.AddListener(() => OnNetgunButtonClicked(index));
        }

        // �ʱ� ���� ���� �ε� �� �ݿ�
        LoadEquippedNetgun();
    }

    public void OnNetgunButtonClicked(int index)
    {
        // ��� ��ư ���� �ʱ�ȭ
        for (int i = 0; i < netgunItems.Length; i++)
        {
            netgunItems[i].isEquipped = (i == index); // Ŭ���� ��ư�� Ȱ��ȭ
        }

        // ���� ���� ���� ����
        SaveEquippedNetgun(index);

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

        for (int i = 0; i < netgunItems.Length; i++)
        {
            if (netgunItems[i].isEquipped && netgunItems[i].associatedSprite != null)
            {
                spriteRenderer.sprite = netgunItems[i].associatedSprite;
                updated = true;
                Debug.Log($"Target object's sprite updated to: {netgunItems[i].associatedSprite.name}");
                break; // ���� ȣ�� ����
            }
        }

        if (!updated)
        {
            Debug.LogWarning("No equipped button or associated sprite found.");
        }
    }


    private void SaveEquippedNetgun(int index)
    {
        PlayerPrefs.SetInt(EquippedNetgunKey, index);
        PlayerPrefs.Save();
        Debug.Log($"Equipped netgun saved: {index}");
    }

    private void LoadEquippedNetgun()
    {
        int equippedIndex = PlayerPrefs.GetInt(EquippedNetgunKey, 0); // �⺻��: ù ��° ���
        Debug.Log($"Loaded equipped netgun: {equippedIndex}");

        for (int i = 0; i < netgunItems.Length; i++)
        {
            netgunItems[i].isEquipped = (i == equippedIndex);
        }

        // �ʱ� ���� �ݿ�
        UpdateTargetSprite();
    }
}
