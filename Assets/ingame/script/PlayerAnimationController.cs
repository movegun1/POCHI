using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;  // Animator 컴포넌트 참조
    private Vector3 lastPosition;  // 이전 프레임의 캐릭터 위치

    void Start()
    {
        // 시작 시 캐릭터의 현재 위치를 저장
        lastPosition = transform.position;
    }

    void Update()
    {
        // 현재 위치와 이전 위치의 차이를 통해 이동 거리 계산
        float distanceMoved = (transform.position - lastPosition).sqrMagnitude;

        // 캐릭터가 움직였는지 확인 (임계값 설정)
        if (distanceMoved > 0.0000000001f)  // 작은 값 설정
        {
            // 캐릭터가 움직이면 애니메이션 정상 재생
            animator.speed = 1f;
        }
        else
        {
            // 캐릭터가 멈춰 있으면 애니메이션 재생 멈춤
            animator.speed = 0f;
        }

        // 현재 위치를 저장하여 다음 프레임에서 비교
        lastPosition = transform.position;
    }
}
