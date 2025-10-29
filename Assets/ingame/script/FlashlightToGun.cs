using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightToGun : MonoBehaviour
{
    public CircleCollider2D touchCollider; // CircleCollider2D (��ġ ����)
    public bool isTouchingArea = false;    // ��ġ ������ ���� ���θ� ��Ÿ���� �÷���

    void Update()
    {
        // ��ġ �Է� ó��
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ��ġ�� ���۵Ǿ��� ��
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                // ��ġ ���� �ȿ��� ��ġ�� �߻��ߴ��� Ȯ��
                if (hit.collider != null && hit.collider == touchCollider)
                {
                    isTouchingArea = true;  // ��ġ ���� �ȿ��� ��ġ ������ ǥ��
                }
            }

            // ��ġ�� ������ ��
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouchingArea = false; // ��ġ�� �������Ƿ� �÷��� ����
            }
        }

        // ���콺 Ŭ�� ó�� (������ ȯ�濡�� �׽�Ʈ��)
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider == touchCollider)
            {
                isTouchingArea = true; // ��ġ ���� �ȿ��� Ŭ�� ������ ǥ��
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isTouchingArea = false; // Ŭ���� �������Ƿ� �÷��� ����
        }
    }
}
