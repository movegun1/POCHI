using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPositionDisplay : MonoBehaviour
{
    public Transform playerTransform;  // ���ΰ� ������Ʈ�� Transform�� ����
    public Text positionText;          // UI �ؽ�Ʈ ������Ʈ�� ����

    void Update()
    {
        // �÷��̾� x ��ǥ�� ������ ��ȯ�Ͽ� ǥ��
        int playerXPosition = Mathf.RoundToInt(playerTransform.position.x);
        positionText.text = playerXPosition + "M";
    }
}