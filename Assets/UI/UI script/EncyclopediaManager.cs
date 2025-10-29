using UnityEngine;
using TMPro;

public class EncyclopediaManager : MonoBehaviour
{
    [System.Serializable]
    public class EncyclopediaEntry
    {
        public string entryName;  // �׸� �̸� (���� ����)
        public TextMeshProUGUI textElement;  // ������ �ٲ� �ؽ�Ʈ
    }

    public EncyclopediaEntry[] entries; // ���� ��Ʈ���� ����

    private readonly Color activeColor = new Color32(255, 255, 255, 255); // Ȱ��ȭ�� �ؽ�Ʈ ���� (FFFFFF)
    private readonly Color inactiveColor = new Color32(50, 50, 50, 255);  // ��Ȱ��ȭ�� �ؽ�Ʈ ���� (323232)

    // ��ư�� ������ �� ȣ��� �޼���
    public void OnButtonClicked(int index)
    {
        if (index < 0 || index >= entries.Length)
        {
            Debug.LogWarning("�߸��� �ε����Դϴ�!");
            return;
        }

        // ��� �ؽ�Ʈ�� ��Ȱ��ȭ �������� ����
        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].textElement != null)
            {
                entries[i].textElement.color = inactiveColor;
            }
        }

        // ���õ� �ؽ�Ʈ�� Ȱ��ȭ �������� ����
        if (entries[index].textElement != null)
        {
            entries[index].textElement.color = activeColor;
        }

        Debug.Log($"Button {index} Ŭ��: �ؽ�Ʈ ���� ������Ʈ �Ϸ�");
    }

    // �ʱ�ȭ: �ƹ� �۾��� ���� ����
    private void Start()
    {
        Debug.Log("EncyclopediaManager �ʱ�ȭ �Ϸ�");
    }
}
