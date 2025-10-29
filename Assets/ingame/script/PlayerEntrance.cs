using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerEntrance : MonoBehaviour
{
    public float moveDistance = 5f;  // ���ΰ��� �̵��� �Ÿ�
    public float moveSpeed = 2f;     // ���ΰ��� �̵� �ӵ�

    private Vector3 targetPosition;  // ��ǥ ��ġ
    private bool shouldMove = true;  // �̵����� ����

    void Start()
    {
        // ���ΰ��� �̵��� ��ǥ ��ġ�� ���� ��ġ���� �������� moveDistance ��ŭ ����
        targetPosition = transform.position + new Vector3(moveDistance, 0f, 0f);

        // �̵� �ڷ�ƾ ����
        StartCoroutine(MoveForward());
    }

    IEnumerator MoveForward()
    {
        // ���ΰ��� ��ǥ ��ġ�� ������ ������ �̵�
        while (shouldMove)
        {
            // ���ΰ��� ��ġ�� ���������� �̵���Ŵ
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // ��ǥ ��ġ�� �����ϸ� �̵� ����
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                shouldMove = false;
            }

            yield return null;  // �� ������ ���
        }
    }
}
