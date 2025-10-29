using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;  // Animator ������Ʈ ����
    private Vector3 lastPosition;  // ���� �������� ĳ���� ��ġ

    void Start()
    {
        // ���� �� ĳ������ ���� ��ġ�� ����
        lastPosition = transform.position;
    }

    void Update()
    {
        // ���� ��ġ�� ���� ��ġ�� ���̸� ���� �̵� �Ÿ� ���
        float distanceMoved = (transform.position - lastPosition).sqrMagnitude;

        // ĳ���Ͱ� ���������� Ȯ�� (�Ӱ谪 ����)
        if (distanceMoved > 0.0000000001f)  // ���� �� ����
        {
            // ĳ���Ͱ� �����̸� �ִϸ��̼� ���� ���
            animator.speed = 1f;
        }
        else
        {
            // ĳ���Ͱ� ���� ������ �ִϸ��̼� ��� ����
            animator.speed = 0f;
        }

        // ���� ��ġ�� �����Ͽ� ���� �����ӿ��� ��
        lastPosition = transform.position;
    }
}
