using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    // 기본 카메라 추적 변수
    public Transform player;        // 주인공의 Transform
    public Vector3 offset;          // 주인공과 카메라 간의 거리
    public float smoothSpeed = 0.05f; // 카메라 이동 속도 (더 부드럽게 설정)

    // 특별한 이벤트 변수
    public bool specialEvent = false; // 특별한 이벤트 상태
    public GameObject aObject;        // A 오브젝트
    public GameObject bObject;        // B 오브젝트
    public float distanceThreshold = 10.0f; // A와 B 간 거리 조건
    public Vector3 aOffset;           // A 오브젝트를 따라갈 때의 카메라 오프셋

    // 카메라 이동 제한 변수
    public float minY = -10f;         // Y축 최소 값
    public float maxY = 10f;          // Y축 최대 값

    private bool isFollowingA = false; // A 오브젝트를 따라가는 상태
    private Vector3 velocity = Vector3.zero; // SmoothDamp에서 사용할 속도 참조 변수

    private void LateUpdate()
    {
        // A 오브젝트를 따라가는 조건적 제어
        if (aObject.activeSelf && Vector3.Distance(aObject.transform.position, bObject.transform.position) > distanceThreshold)
        {
            ActivateSpecialEvent(); // 스페셜 이벤트 활성화
            FollowAObject();
        }
        else
        {
            // 기본 카메라 추적 처리
            DeactivateSpecialEvent(); // 스페셜 이벤트 비활성화
            HandleBasicFollow();
        }

        // 카메라 Y축 이동 제한
        ClampCameraYPosition();
    }

    private void HandleBasicFollow()
    {
        if (!specialEvent) // 특별한 이벤트가 아닐 때만 실행
        {
            Vector3 desiredPosition = player.position + offset; // 목표 위치
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed); // 더 부드럽게 이동
            transform.position = smoothedPosition;
        }
    }

    private void FollowAObject()
    {
        // A 오브젝트 중심으로 카메라 위치 고정 + aOffset 적용
        Vector3 desiredPosition = new Vector3(
            aObject.transform.position.x + aOffset.x,
            aObject.transform.position.y + aOffset.y,
            aObject.transform.position.z + aOffset.z
        );
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }

    private void ClampCameraYPosition()
    {
        // Y축 위치 제한
        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = clampedPosition;
    }

    private void ActivateSpecialEvent()
    {
        if (!specialEvent)
        {
            specialEvent = true;
            Debug.Log("Special Event Activated");
        }
    }

    private void DeactivateSpecialEvent()
    {
        if (specialEvent)
        {
            specialEvent = false;
            Debug.Log("Special Event Deactivated");
        }
    }
}
