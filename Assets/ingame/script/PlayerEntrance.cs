using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerEntrance : MonoBehaviour
{
    public float moveDistance = 5f;  // 주인공이 이동할 거리
    public float moveSpeed = 2f;     // 주인공의 이동 속도

    private Vector3 targetPosition;  // 목표 위치
    private bool shouldMove = true;  // 이동할지 여부

    void Start()
    {
        // 주인공이 이동할 목표 위치를 현재 위치에서 앞쪽으로 moveDistance 만큼 설정
        targetPosition = transform.position + new Vector3(moveDistance, 0f, 0f);

        // 이동 코루틴 시작
        StartCoroutine(MoveForward());
    }

    IEnumerator MoveForward()
    {
        // 주인공이 목표 위치에 도달할 때까지 이동
        while (shouldMove)
        {
            // 주인공의 위치를 점진적으로 이동시킴
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 목표 위치에 도달하면 이동 멈춤
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                shouldMove = false;
            }

            yield return null;  // 매 프레임 대기
        }
    }
}
