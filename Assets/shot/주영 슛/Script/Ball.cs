using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CircleCollider2D col;
    private SpriteRenderer spriteRenderer;

    public Sprite[] sprites; // 스프라이트 배열
    private int spriteIndex = 0;

    public Transform targetObject; // 목표로 이동할 오브젝트 (Transform으로 설정)

    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer 컴포넌트 가져오기

        // 공이 처음 생성될 때 초기 상태로 설정
        ResetBall();
    }

    void Update()
    {
        // 매 프레임마다 스프라이트가 공의 이동 방향을 보도록 회전
        RotateSpriteToMatchDirection();
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
        rb.gravityScale = 1f;

        // 공이 날아가는 순간 스프라이트 변경
        StartCoroutine(ChangeSprites());

        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Fire);
    }

    private void RotateSpriteToMatchDirection()
    {
        Vector2 velocity = rb.velocity;

        // 공이 충분히 움직이고 있을 때만 회전 처리
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg; // 속도 벡터를 각도로 변환
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // 각도에 맞게 회전
        }
    }

    public void ActivateRb()
    {
        rb.isKinematic = false;
    }

    public void DeactivateRb()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = 0f;
        rb.isKinematic = true;
    }

    private IEnumerator ChangeSprites()
    {
        while (spriteIndex < sprites.Length)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
            Debug.Log("Changing to sprite: " + sprites[spriteIndex].name); // 디버깅 로그 추가
            spriteIndex++;
            yield return new WaitForSeconds(0.1f); // 스프라이트 변경 간격 조정
        }

        // 애니메이션 완료 후 스프라이트 인덱스 리셋
        spriteIndex = 0;
    }

    // 공을 처음 상태로 초기화하는 함수
    public void ResetBall()
    {
        // 스프라이트 초기화
        spriteIndex = 0;
        spriteRenderer.sprite = sprites[spriteIndex];

        // Transform을 초기 상태로 돌리기 (회전 리셋)
        transform.rotation = Quaternion.identity;

        // 목표 오브젝트의 위치로 이동
        if (targetObject != null)
        {
            transform.position = targetObject.position;
        }
        else
        {
            Debug.LogWarning("Target object is not set, ball will not move to a specific position.");
        }
    }

    public void Reload()
    {
        // 재장전 시 공의 상태를 초기화
        ResetBall();
    }

    // 충돌 감지 함수 (CircleCollider2D가 있을 때)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌 시 스프라이트를 처음 상태로 변경
        ResetBall();

        // 필요한 경우 공의 동작을 멈추기
        DeactivateRb();
    }

    // 트리거 충돌이 있을 때도 동일하게 처리 가능
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 트리거 충돌 시 스프라이트를 처음 상태로 변경
        ResetBall();

        // 필요한 경우 공의 동작을 멈추기
        DeactivateRb();
    }
}
