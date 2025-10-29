using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_animal : MonoBehaviour
{
    public float moveSpeed = 1f; // 이동 속도 설정
    private Transform playerTransform;
    Animator anim;
    private bool isRunning = false; // 코루틴 중복 방지 플래그
    public Collider2D Area;

    public bool isMoving = false;

    private Coroutine wanderCoroutine; // Wander 코루틴 참조 변수

    // 이동하는 코루틴 함수
    IEnumerator Run(float distanceMin, float distanceMax)
    {
        if (isRunning) yield break; // 이미 이동 중이면 실행 안 함

        // Wander가 실행 중이라면 중지
        if (wanderCoroutine != null)
        {
            StopCoroutine(wanderCoroutine);
            wanderCoroutine = null; // 코루틴 참조 초기화
            isMoving = false;
        }

        isRunning = true; // 코루틴 실행 중

        float totalTime = 4f / moveSpeed; // 총 이동 시간을 moveSpeed에 따라 조정
        float elapsedTime = 0f; // 경과 시간
        Vector3 startPosition = transform.position; // 시작 위치
        float moveDistance = Random.Range(distanceMin, distanceMax); // 이동 거리 설정

        if (playerTransform != null)
        {
            if (transform.position.x > playerTransform.position.x && moveDistance < 0)
            {
                moveDistance *= -1; // 플레이어보다 앞에 있고 음수라면 양수로 변환
            }
            else if (transform.position.x < playerTransform.position.x && moveDistance > 0)
            {
                moveDistance *= -1; // 플레이어보다 뒤에 있고 양수라면 음수로 변환
            }
        }

        Vector3 direction = (moveDistance > 0) ? Vector3.right : Vector3.left; // 이동 방향 설정
        Vector3 targetPosition = startPosition + direction * Mathf.Abs(moveDistance); // 목표 위치 계산
        anim = GetComponent<Animator>();

        anim.SetBool("iswalk", false);
        anim.SetBool("isrun", true); // 애니메이션 "isrun" 설정

        Vector3 currentScale = transform.localScale;
        transform.localScale = new Vector3(
            (moveDistance > 0) ? -Mathf.Abs(currentScale.x) : Mathf.Abs(currentScale.x),
            currentScale.y,
            currentScale.z
        );

        while (elapsedTime < totalTime)
        {
            float t = elapsedTime / totalTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        anim.SetBool("isrun", false); // 이동 종료

        transform.position = targetPosition;

        if (Random.value > 0.5f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        isRunning = false; // 이동 완료 후 다시 실행 가능
    }

    // 어슬렁 이동 코루틴
    IEnumerator Wander()
    {
        if (isMoving || isRunning) yield break; // 이미 이동 중이면 실행 안 함

        isMoving = true;

        float wanderTime = Random.Range(1.5f, 3f); // 어슬렁거리는 시간
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        float wanderDistance = Random.Range(-3f, 3f); // 랜덤 거리
        Vector3 wanderDirection = (wanderDistance > 0) ? Vector3.right : Vector3.left;
        Vector3 targetPosition = startPosition + wanderDirection * Mathf.Abs(wanderDistance);

        Vector3 currentScale = transform.localScale;
        transform.localScale = new Vector3(
            (wanderDistance > 0) ? -Mathf.Abs(currentScale.x) : Mathf.Abs(currentScale.x),
            currentScale.y,
            currentScale.z
        );

        anim.SetBool("iswalk", true);
        while (elapsedTime < wanderTime)
        {
            if (isRunning)
            {
                yield break; // Run이 실행되면 즉시 중단
            }

            float t = elapsedTime / wanderTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        anim.SetBool("iswalk", false);
        yield return new WaitForSeconds(1.5f); // 어슬렁거린 후 2초 대기

        isMoving = false; // 어슬렁 이동 종료
        wanderCoroutine = null; // 코루틴 참조 초기화
    }

    void Start()
    {
        anim = GetComponent<Animator>(); // Animator 컴포넌트 초기화
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerTransform = playerObject?.transform;

        GameObject areaObject = GameObject.Find("Area");
        if (areaObject != null)
        {
            Area = areaObject.GetComponent<Collider2D>();
        }
        else
        {
            Debug.LogWarning("이름이 'Area'인 오브젝트를 찾을 수 없습니다.");
        }
    }


    void Update()
    {
        if (playerTransform == null) return;

        float playerX = playerTransform.position.x;

        if (!isRunning)
        {
            if (transform.position.x >= playerX && transform.position.x <= playerX + 5)
            {
                StartCoroutine(Run(30f, 35f));
            }
            else if (transform.position.x <= playerX - 3 && transform.position.x >= playerX - 7)
            {
                StartCoroutine(Run(-30f, -35f));
            }
        }

        if (Area == null) return;

        if (!Area.bounds.Contains(transform.position))
        {
            Vector3 newPosition = transform.position;

            if (transform.position.x > Area.bounds.max.x || transform.position.x < Area.bounds.min.x)
            {
                Respawn();
            }
        }

        if (!isMoving && !isRunning)
        {
            if (wanderCoroutine == null) // 어슬렁 코루틴 실행
            {
                wanderCoroutine = StartCoroutine(Wander());
            }
        }
    }

    void Respawn()
    {
        Spawner.instance.Ground_Spawn(); // 새 오브젝트 스폰
        Destroy(gameObject); // 오브젝트 파괴
    }
}
