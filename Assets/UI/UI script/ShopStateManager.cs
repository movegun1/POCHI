using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopStateManager : MonoBehaviour
{
    private const int NumItems = 5; // �� ���� ������ ��
    private const string LentonKey = "LentonButton";
    private const string NetgunKey = "NetgunButton";
    private const string LentonEquippedKey = "LentonEquippedIndex";
    private const string NetgunEquippedKey = "NetgunEquippedIndex";

    // ���� ���� �ʱ�ȭ
    public void ResetShopState()
    {
        // LentonTab �ʱ�ȭ
        for (int i = 0; i < NumItems; i++)
        {
            PlayerPrefs.SetInt($"{LentonKey}{i}State", (i == 0 ? 1 : 0)); // ù ��° ��ư�� Unlocked
        }
        PlayerPrefs.SetInt(LentonEquippedKey, -1); // ���� ���� �ʱ�ȭ

        // NetgunTab �ʱ�ȭ
        for (int i = 0; i < NumItems; i++)
        {
            PlayerPrefs.SetInt($"{NetgunKey}{i}State", (i == 0 ? 1 : 0)); // ù ��° ��ư�� Unlocked
        }
        PlayerPrefs.SetInt(NetgunEquippedKey, -1); // ���� ���� �ʱ�ȭ

        PlayerPrefs.Save();
    }

    // ���� ���� �ε�
    public (int[] lentonStates, int lentonEquippedIndex, int[] netgunStates, int netgunEquippedIndex) LoadShopState()
    {
        int[] lentonStates = new int[NumItems];
        int[] netgunStates = new int[NumItems];

        for (int i = 0; i < NumItems; i++)
        {
            lentonStates[i] = PlayerPrefs.GetInt($"{LentonKey}{i}State", (i == 0 ? 1 : 0));
            netgunStates[i] = PlayerPrefs.GetInt($"{NetgunKey}{i}State", (i == 0 ? 1 : 0));
        }

        int lentonEquippedIndex = PlayerPrefs.GetInt(LentonEquippedKey, -1);
        int netgunEquippedIndex = PlayerPrefs.GetInt(NetgunEquippedKey, -1);

        return (lentonStates, lentonEquippedIndex, netgunStates, netgunEquippedIndex);
    }

    // ���� ���� ����
    public void SaveEquippedState(int lentonEquippedIndex, int netgunEquippedIndex)
    {
        PlayerPrefs.SetInt(LentonEquippedKey, lentonEquippedIndex);
        PlayerPrefs.SetInt(NetgunEquippedKey, netgunEquippedIndex);
        PlayerPrefs.Save();
    }

    // �ر� ���� ����
    public void SaveUnlockState(string tabKey, int index, int state)
    {
        PlayerPrefs.SetInt($"{tabKey}{index}State", state);
        PlayerPrefs.Save();
    }
}
