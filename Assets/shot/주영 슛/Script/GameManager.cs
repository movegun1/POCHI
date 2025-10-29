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

    public bool isCameraLocked = false;  // ī�޶� ����ִ��� ���θ� ��Ÿ���� ����

    Camera cam;

    public Ball ball;
    public Trajectory trajectory;
    public GameObject gun; // �� ��ü �߰�
    [SerializeField] float pushForce = 4f;

    // �巡�� ������ ������ �����ϴ� ����
    [SerializeField] Rect draggableArea = new Rect(-5f, -5f, 10f, 10f);

    // Player ������Ʈ ���� �߰�
    public GameObject player;

    // ���� �巡�� ���� ����
    public Transform dragCircle; // ���� �巡�� ������ Transform�� �����մϴ�.
    private float dragCircleRadius; // ���� �巡�� ������ ������

    bool isDragging = false;
    bool isMoving = false; // �÷��̾ �̵� ������ ���� Ȯ��

    Vector2 dragStartPoint; // �巡�� ���� ��ġ�� ����
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;

    // �ִ� �巡�� �Ÿ��� ��Ÿ���� ���� (�� ���� �����ϸ� �巡�� ������ ������ �����)
    public float maxDragDistance = 3f;

    //---------------------------------------
    void Start()
    {
        cam = Camera.main;
        ball.DeactivateRb(); // ���� ������ ���� ��Ȱ��ȭ

        // ���� �巡�� ������ ������ �ʱ�ȭ
        dragCircleRadius = dragCircle.localScale.x / 2; // x �� �������� ���������� ���

        Audiomanager.instance.PlayBgm(true);
    }

    void Update()
    {
        // �÷��̾� ��ġ�� ���� �巡�� ������ ���� ������Ʈ
        draggableArea.x = player.transform.position.x + 1.1f - draggableArea.width / 2;

        // �÷��̾ �̵� ���� �ƴϸ� �巡�� ����
        if (!isMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

                // �巡�� ������ ���� �ȿ����� �巡�׸� ����
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

        // �巡�� �߿��� �� ��ü�� ������ ������Ʈ
        UpdateGunRotationDuringDrag();
    }

    //-Drag--------------------------------------
    public void OnDragStart()
    {
        isCameraLocked = true;  // �巡�װ� ���۵Ǹ� ī�޶� ���
        ball.DeactivateRb(); // �巡�� ������ �� ���� ��Ȱ��ȭ
        dragStartPoint = cam.ScreenToWorldPoint(Input.mousePosition); // �巡�� ���� ��ġ ����

        trajectory.Show();
    }

    public void OnDragEnd()
    {
        isCameraLocked = false;  // �巡�װ� ������ ī�޶� ��� ����
        ball.ActivateRb();
        ball.Push(force);

        trajectory.Hide();
    }

    // OnDrag �޼��� ����
    void OnDrag()
    {
        // ���콺 ��ġ�� ��������
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        // �巡�� ���� ������ �������� Ȯ��
        Vector2 dragOffset = endPoint - (Vector2)dragCircle.position;
        if (dragOffset.magnitude > dragCircleRadius)
        {
            // ���� ������ �ʰ��� ���, endPoint�� ���� ���� ����
            endPoint = (Vector2)dragCircle.position + dragOffset.normalized * dragCircleRadius;
        }

        distance = Vector2.Distance(dragStartPoint, endPoint); // �Ÿ� ���

        // ���� ���
        direction = (dragStartPoint - endPoint).normalized;

        // �� ���
        force = direction * distance * pushForce;

        // trajectory ������Ʈ
        trajectory.UpdateDots(ball.pos, force);
    }

    void UpdateGunRotationDuringDrag()
    {
        // ���� ��ġ�� ������ ���� �ٶ󺸰� �մϴ�.
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

    // �÷��̾ �̵��� �� ȣ�� (�����ӿ� ���� ��� ��Ȱ��ȭ)
    public void SetIsMoving(bool isPlayerMoving)
    {
        isMoving = isPlayerMoving;
    }

    // Gizmos �׸���
    void OnDrawGizmos()
    {
        // Draggable area ǥ�� (�ʷϻ� �簢��)
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(draggableArea.Center(), draggableArea.Size());

        // �ִ� �巡�� �Ÿ� ǥ�� (������ ��)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ball.transform.position, maxDragDistance);  // �������� maxDragDistance �ݰ����� �� �׸���
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