using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool MoveForward = true; // ������ �̵� ���� ����
    private bool MoveBack = true; // �ڷ� �̵� ���� ����
    private bool isDragging = false; // �巡�� ������ ����
    Animator anim;

    // �̵� ������ ������ ������ �ð�ȭ�ϱ� ���� ���� (UIó�� ����)
    public Rect moveArea = new Rect(-5f, -5f, 10f, 10f); // x, y, width, height
    private Vector3 initialPosition; // �÷��̾��� �ʱ� ��ġ ����

    private void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = new Vector3(-5.5f, -2.2f, 0);
        initialPosition = transform.position; // ������ �� �÷��̾� ��ġ ����
    }

    public void Move()
    {
        if (!MoveForward && !MoveBack || isDragging) return; // �̵� ���� ���θ� üũ�ϰ� �巡�� ���� ��� �̵� �Ұ�

        float speed = Time.deltaTime * 5f;

        if (IsMouseInRange() && IsMouseClicked()) // ���콺�� ���� �ȿ� ���� ���� �̵�
        {
            anim.SetBool("Isrunning", true);
            transform.Translate(Vector2.right * speed);

            // �÷��̾ ������ �� �� moveArea�� �Բ� �̵� (12��ŭ �տ� ��ġ)
            moveArea.x = transform.position.x - moveArea.width / 2 + 14f; // moveArea�� �÷��̾�� 20��ŭ �տ� ��ġ�ϵ��� ����
        }
        else
        {
            anim.SetBool("Isrunning", false);
        }
    }

    // �巡�� ���¸� �����ϴ� �޼���
    public void SetDraggingState(bool dragging)
    {
        isDragging = dragging; // �巡�� ������ ���� ����
    }

    // ���콺 Ŭ�� Ȯ�� �Լ� �߰�
    private bool IsMouseClicked()
    {
        return Input.GetMouseButton(0); // ���콺 ���� ��ư�� ���ȴ��� Ȯ��
    }

    // ���콺�� ������ ���� �ȿ� �ִ��� Ȯ���ϴ� �Լ�
    private bool IsMouseInRange()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return moveArea.Contains(mousePosition);
    }

    void ChangeR()
    {
        transform.position = new Vector3(-5f, -2.2f, 0);
    }

    void ChangeL()
    {
        transform.position = new Vector3(45f, -2.2f, 0);
    }

    void Update()
    {
        if (transform.position.x < -10)
        {
            ChangeL();
        }
        else if (transform.position.x > 80)
        {
            ChangeR();
        }
        else if (MoveForward && MoveBack)
        {
            Move();
        }
    }

    public void GoStop()
    {
        anim.SetBool("Isrunning", false);
        MoveForward = false; // ������ �̵� �Ұ� ����
        SetDraggingState(true); // �巡�� ������ ����
    }

    public void BackStop()
    {
        anim.SetBool("Isrunning", false);
        MoveBack = false; // �ڷ� �̵� �Ұ� ����
    }

    public void Stop()
    {
        anim.SetBool("Isrunning", false);
        MoveForward = false; // ������ �̵� �Ұ� ����
        MoveBack = false; // �ڷ� �̵� �Ұ� ����
        SetDraggingState(true); // �巡�� ������ ����
    }

    public void Go()
    {
        MoveForward = true; // ������ �̵� ���� ����
        MoveBack = true; // �ڷ� �̵� ���� ����
        SetDraggingState(false); // �巡�� �� �ƴ��� ����
    }

    // ����� ����Ͽ� �̵� ������ ������ �ð�ȭ
    void OnDrawGizmos()
    {
        // �ʷϻ� �簢������ �̵� ������ ���� �׸���
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(moveArea.center(), moveArea.size());
    }

    // �÷��̾��� �̵� ������ UIó�� ����
    void LateUpdate()
    {
        // �÷��̾��� x�� �̵��� ���, y�� ��ġ�� ���� (UIó�� �۵��ϰ�)
        Vector3 fixedPosition = transform.position;
        fixedPosition.y = initialPosition.y; // �ʱ� y ��ġ�� ����
        transform.position = fixedPosition;
    }
}

public static class RectExtensions
{
    public static Vector2 center(this Rect rect)
    {
        return new Vector2(rect.x + rect.width / 2, rect.y + rect.height / 2);
    }

    public static Vector2 size(this Rect rect)
    {
        return new Vector2(rect.width, rect.height);
    }
}
