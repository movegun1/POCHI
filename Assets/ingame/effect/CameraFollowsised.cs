using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsised : MonoBehaviour
{
    public Transform player;  // ���� PLAYER ������Ʈ�� Transform
    public Vector3 offset = new Vector3(0f, 2f, -10f);  // ī�޶��� ����� ��ġ (������ ��)

    void LateUpdate()
    {
        // PLAYER�� ��ġ + ������ ��ġ�� ī�޶� ��ġ�� ����
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
