using UnityEngine;

// ������ Ÿ�� ������ (���� �����Ϳ�)
public enum ItemType
{
    Mammalia, // ������
    Amphibia, // �缭��
    Birds     // ����
}

// ������ ������ Ŭ����
[System.Serializable]
public class Item
{
    public int itemId; // ������ ID
    public int itemLv; // ���� ����
    public ItemType itemType; // ������ Ÿ��
    public string itemName; // ���� �̸�
    public Sprite itemImage; // ���� �̹���

    public int price; // ������ ����

    public string spownArea; // ���� ����
    public int spownRange; // ���� �Ÿ�

    public string itemText; // �ΰ��� ����
    public string itemRealText; // ���� ����

    public bool getChecked = false; // ���� ȹ�� ���� �÷���
}
