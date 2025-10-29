using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    public bool isCameraLocked = false;  // 카메라가 잠겨있는지 여부를 나타내는 변수

    Camera cam;

    public Ball ball;
    public Trajectory trajectory;
    public GameObject gun; // 총 객체 추가
    [SerializeField] float pushForce = 4f;

    // 드래그 가능한 범위를 정의하는 변수
    [SerializeField] Rect draggableArea = new Rect(-5f, -5f, 10f, 10f);

    // Player 오브젝트 참조 추가
    public GameObject player;

    // 원형 드래그 영역 변수
    public Transform dragCircle; // 원형 드래그 영역의 Transform을 참조합니다.
    private float dragCircleRadius; // 원형 드래그 영역의 반지름

    bool isDragging = false;
    bool isMoving = false; // 플레이어가 이동 중인지 여부 확인

    Vector2 dragStartPoint; // 드래그 시작 위치를 저장
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;

    // 최대 드래그 거리를 나타내는 변수 (이 값을 조정하면 드래그 가능한 범위가 변경됨)
    public float maxDragDistance = 3f;

    //---------------------------------------
    void Start()
    {
        cam = Camera.main;
        ball.DeactivateRb(); // 공을 시작할 때만 비활성화

        // 원형 드래그 영역의 반지름 초기화
        dragCircleRadius = dragCircle.localScale.x / 2; // x 축 스케일을 반지름으로 사용

        Audiomanager.instance.PlayBgm(true);
    }

    void Update()
    {
        // 플레이어 위치에 맞춰 드래그 가능한 영역 업데이트
        draggableArea.x = player.transform.position.x + 1.1f - draggableArea.width / 2;

        // 플레이어가 이동 중이 아니면 드래그 가능
        if (!isMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

                // 드래그 가능한 영역 안에서만 드래그를 시작
                if (draggableArea.Contains(mousePosition))
                {
                    isDragging = true;
                    OnDragStart();
                }
            }
            if (Input.GetMouseButtonUp(0) && isDragging)
            {
                isDragging = false;
                OnDragEnd();
            }
        }

        if (isDragging)
        {
            OnDrag();
        }

        // 드래그 중에도 총 객체의 방향을 업데이트
        UpdateGunRotationDuringDrag();
    }

    //-Drag--------------------------------------
    public void OnDragStart()
    {
        isCameraLocked = true;  // 드래그가 시작되면 카메라 잠금
        ball.DeactivateRb(); // 드래그 시작할 때 공을 비활성화
        dragStartPoint = cam.ScreenToWorldPoint(Input.mousePosition); // 드래그 시작 위치 저장

        trajectory.Show();
    }

    public void OnDragEnd()
    {
        isCameraLocked = false;  // 드래그가 끝나면 카메라 잠금 해제
        ball.ActivateRb();
        ball.Push(force);

        trajectory.Hide();
    }

    // OnDrag 메서드 수정
    void OnDrag()
    {
        // 마우스 위치를 가져오기
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        // 드래그 가능 영역이 원형인지 확인
        Vector2 dragOffset = endPoint - (Vector2)dragCircle.position;
        if (dragOffset.magnitude > dragCircleRadius)
        {
            // 원형 범위를 초과할 경우, endPoint를 원형 경계로 제한
            endPoint = (Vector2)dragCircle.position + dragOffset.normalized * dragCircleRadius;
        }

        distance = Vector2.Distance(dragStartPoint, endPoint); // 거리 계산

        // 방향 계산
        direction = (dragStartPoint - endPoint).normalized;

        // 힘 계산
        force = direction * distance * pushForce;

        // trajectory 업데이트
        trajectory.UpdateDots(ball.pos, force);
    }

    void UpdateGunRotationDuringDrag()
    {
        // 공의 위치를 가져와 총이 바라보게 합니다.
        Vector2 gunDirection = (Vector2)ball.transform.position - (Vector2)gun.transform.position;
        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    //-----------------------------------------
    public void ReloadBall()
    {
        ball.DeactivateRb();
        ball.transform.position = Vector3.zero;
        trajectory.Show();
    }

    // 플레이어가 이동할 때 호출 (움직임에 따라 쏘기 비활성화)
    public void SetIsMoving(bool isPlayerMoving)
    {
        isMoving = isPlayerMoving;
    }

    // Gizmos 그리기
    void OnDrawGizmos()
    {
        // Draggable area 표시 (초록색 사각형)
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(draggableArea.Center(), draggableArea.Size());

        // 최대 드래그 거리 표시 (빨간색 원)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ball.transform.position, maxDragDistance);  // 원점에서 maxDragDistance 반경으로 원 그리기
    }
}

public static class MyRectExtensions
{
    public static Vector2 Center(this Rect rect)
    {
        return new Vector2(rect.x + rect.width / 2, rect.y + rect.height / 2);
    }

    public static Vector2 Size(this Rect rect)
    {
        return new Vector2(rect.width, rect.height);
    }
}