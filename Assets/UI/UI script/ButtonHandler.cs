using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void AddMoney()
    {
        MoneyManager.Instance.AddCash(1000); // �� 100 �߰�
    }

    public void SubtractMoney()
    {
        MoneyManager.Instance.SubtractCash(1000); // �� 100 ����
    }
}
