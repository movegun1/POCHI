using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PressToStartAnimation : MonoBehaviour
{
    public RectTransform pressToStartImage;  // 'PRESS TO START' �̹����� RectTransform
    public float moveSpeed = 1.0f;  // �����̴� �ӵ�
    public float moveDistance = 10.0f;  // ���Ʒ��� �����̴� �Ÿ�

    private Vector2 originalPosition;  // �̹����� ���� ��ġ
    private bool movingUp = true;  // �̹����� ���� �̵����� ����

    void Start()
    {
        originalPosition = pressToStartImage.anchoredPosition;  // �̹����� ���� ��ġ ����
        StartCoroutine(MoveUpDown());
    }

    private IEnumerator MoveUpDown()
    {
        while (true)
        {
            // ���� ������
            if (movingUp)
            {
                pressToStartImage.anchoredPosition = Vector2.Lerp(
                    pressToStartImage.anchoredPosition,
                    originalPosition + new Vector2(0, moveDistance),
                    Time.deltaTime * moveSpeed
                );

                // ��ǥ ��ġ�� �����ϸ� ���� ����
                if (Vector2.Distance(pressToStartImage.anchoredPosition, originalPosition + new Vector2(0, moveDistance)) < 0.1f)
                {
                    movingUp = false;
                }
            }
            // �Ʒ��� ������
            else
            {
                pressToStartImage.anchoredPosition = Vector2.Lerp(
                    pressToStartImage.anchoredPosition,
                    originalPosition - new Vector2(0, moveDistance),
                    Time.deltaTime * moveSpeed
                );

                // ��ǥ ��ġ�� �����ϸ� ���� ����
                if (Vector2.Distance(pressToStartImage.anchoredPosition, originalPosition - new Vector2(0, moveDistance)) < 0.1f)
                {
                    movingUp = true;
                }
            }

            yield return null;  // �����Ӹ��� �ݺ�
        }
    }
}
