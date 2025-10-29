using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PressToStartAnimation : MonoBehaviour
{
    public RectTransform pressToStartImage;  // 'PRESS TO START' 이미지의 RectTransform
    public float moveSpeed = 1.0f;  // 움직이는 속도
    public float moveDistance = 10.0f;  // 위아래로 움직이는 거리

    private Vector2 originalPosition;  // 이미지의 원래 위치
    private bool movingUp = true;  // 이미지를 위로 이동할지 여부

    void Start()
    {
        originalPosition = pressToStartImage.anchoredPosition;  // 이미지의 원래 위치 저장
        StartCoroutine(MoveUpDown());
    }

    private IEnumerator MoveUpDown()
    {
        while (true)
        {
            // 위로 움직임
            if (movingUp)
            {
                pressToStartImage.anchoredPosition = Vector2.Lerp(
                    pressToStartImage.anchoredPosition,
                    originalPosition + new Vector2(0, moveDistance),
                    Time.deltaTime * moveSpeed
                );

                // 목표 위치에 도달하면 방향 변경
                if (Vector2.Distance(pressToStartImage.anchoredPosition, originalPosition + new Vector2(0, moveDistance)) < 0.1f)
                {
                    movingUp = false;
                }
            }
            // 아래로 움직임
            else
            {
                pressToStartImage.anchoredPosition = Vector2.Lerp(
                    pressToStartImage.anchoredPosition,
                    originalPosition - new Vector2(0, moveDistance),
                    Time.deltaTime * moveSpeed
                );

                // 목표 위치에 도달하면 방향 변경
                if (Vector2.Distance(pressToStartImage.anchoredPosition, originalPosition - new Vector2(0, moveDistance)) < 0.1f)
                {
                    movingUp = true;
                }
            }

            yield return null;  // 프레임마다 반복
        }
    }
}
