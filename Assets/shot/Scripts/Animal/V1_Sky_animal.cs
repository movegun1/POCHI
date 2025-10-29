using System.Collections;
using UnityEngine;

public class V1_Sky_animal : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool _isCoroutineRunning;
    private float _n;
    public Collider2D Area;
    private SpriteRenderer _spriteRenderer; // Add a reference to SpriteRenderer
    Animator anim;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component

        if (_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer가 할당되지 않았습니다. GameObject에 SpriteRenderer가 있는지 확인하세요.");
        }

        // "Area"라는 이름의 오브젝트를 찾아 Collider2D를 할당
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

    // 캐릭터를 상승시키는 코루틴
    IEnumerator Up()
    {
        // 애니메이션 "isrun"을 true로 설정 (이동 시작)
        anim.SetBool("isrun", true);
        // 매 프레임마다 Rigidbody2D의 Linear Drag 값을 3.2 ~ 4 사이의 랜덤값으로 변경
        _rb.drag = Random.Range(3.2f, 4.0f);

        if (_isCoroutineRunning) yield break;
        _isCoroutineRunning = true;

        float totalTime = 2f;  // 상승에 걸리는 총 시간
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        // 목표 위치 설정
        float targetY = Mathf.Clamp(transform.position.y + Random.Range(2f, 5f), 5f, 6f);
        Vector3 targetPosition = new Vector3(transform.position.x, targetY, startPosition.z);

        // SmoothStep을 사용하여 부드럽게 목표 위치로 이동
        while (elapsedTime < totalTime)
        {
            float t = elapsedTime / totalTime;
            float smoothedT = Mathf.SmoothStep(0, 1, t);
            transform.position = new Vector3(
                transform.position.x,  // X축 이동을 고정
                Mathf.Lerp(startPosition.y, targetPosition.y, smoothedT),
                startPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        _isCoroutineRunning = false;

        // 랜덤하게 Glide 코루틴 시작
        if (Random.Range(1, 3) == 1)
        {
            _n = Random.Range(-1f, 0f); // 왼쪽으로 활강
            StartCoroutine(Glide(Vector2.left));
        }
        else
        {
            _n = Random.Range(0f, 1f); // 오른쪽으로 활강
            StartCoroutine(Glide(Vector2.right));
        }
    }

    // 캐릭터를 특정 방향으로 활강시키는 공통 코루틴
    IEnumerator Glide(Vector2 direction)
    {
        // 애니메이션 "isrun"을 true로 설정 (이동 시작)
        anim.SetBool("isrun", false);
        if (_isCoroutineRunning) yield break;
        _isCoroutineRunning = true;

        float totalTime = Random.Range(1.5f, 2f);  // 활강에 걸리는 총 시간
        float elapsedTime = 0f;

        // 초기 속도와 목표 속도 설정
        Vector2 initialVelocity = Vector2.zero;
        Vector2 targetVelocity = direction * Random.Range(5f, 8f);

        // 스프라이트를 방향에 따라 반전
        if (direction == Vector2.left)
        {
            // 기존 오브젝트 크기를 유지하면서 x 축은 반전되지 않도록 설정
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // x축을 양수로 유지
        }
        else if (direction == Vector2.right)
        {
            // 기존 오브젝트 크기를 유지하면서 x 축을 반전
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // x축을 음수로 설정하여 반전
        }

        // SmoothDamp를 사용하여 속도를 천천히 증가시킴
        while (elapsedTime < totalTime)
        {
            Vector2 currentVelocity = Vector2.SmoothDamp(_rb.velocity, targetVelocity, ref initialVelocity, totalTime);
            _rb.velocity = currentVelocity;
            transform.position += (Vector3)(direction * currentVelocity.magnitude * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _isCoroutineRunning = false;

        // 상승 코루틴 다시 시작
        StartCoroutine(Up());
    }

    void Update()
    {

        if (!_isCoroutineRunning)
        {
            if (transform.position.y <= 0f)
            {
                StartCoroutine(Up());
            }
        }

        if (Area == null)
        {
            return;
        }

        if (!Area.bounds.Contains(transform.position))
        {
            Vector3 newPosition = transform.position;

            if (transform.position.x > Area.bounds.max.x)
            {
                // 일정 시간 대기 후 Fly_Spawn 호출 및 오브젝트 파괴
                Respawn();
            }
            else if (transform.position.x < Area.bounds.min.x)
            {
                // 일정 시간 대기 후 Fly_Spawn 호출 및 오브젝트 파괴
                Respawn();
            }
        }
    }
    void Respawn()
    {
        Spawner.instance.Fly_Spawn();  // 새 오브젝트 스폰
        Destroy(gameObject);  // 오브젝트 파괴
    }
}