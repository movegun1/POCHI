using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_animal : MonoBehaviour
{
    public float moveSpeed = 1f; // �̵� �ӵ� ����
    private Transform playerTransform;
    Animator anim;
    private bool isRunning = false; // �ڷ�ƾ �ߺ� ���� �÷���
    public Collider2D Area;

    public bool isMoving = false;

    private Coroutine wanderCoroutine; // Wander �ڷ�ƾ ���� ����

    // �̵��ϴ� �ڷ�ƾ �Լ�
    IEnumerator Run(float distanceMin, float distanceMax)
    {
        if (isRunning) yield break; // �̹� �̵� ���̸� ���� �� ��

        // Wander�� ���� ���̶�� ����
        if (wanderCoroutine != null)
        {
            StopCoroutine(wanderCoroutine);
            wanderCoroutine = null; // �ڷ�ƾ ���� �ʱ�ȭ
            isMoving = false;
        }

        isRunning = true; // �ڷ�ƾ ���� ��

        float totalTime = 4f / moveSpeed; // �� �̵� �ð��� moveSpeed�� ���� ����
        float elapsedTime = 0f; // ��� �ð�
        Vector3 startPosition = transform.position; // ���� ��ġ
        float moveDistance = Random.Range(distanceMin, distanceMax); // �̵� �Ÿ� ����

        if (playerTransform != null)
        {
            if (transform.position.x > playerTransform.position.x && moveDistance < 0)
            {
                moveDistance *= -1; // �÷��̾�� �տ� �ְ� ������� ����� ��ȯ
            }
            else if (transform.position.x < playerTransform.position.x && moveDistance > 0)
            {
                moveDistance *= -1; // �÷��̾�� �ڿ� �ְ� ������ ������ ��ȯ
            }
        }

        Vector3 direction = (moveDistance > 0) ? Vector3.right : Vector3.left; // �̵� ���� ����
        Vector3 targetPosition = startPosition + direction * Mathf.Abs(moveDistance); // ��ǥ ��ġ ���
        anim = GetComponent<Animator>();

        anim.SetBool("iswalk", false);
        anim.SetBool("isrun", true); // �ִϸ��̼� "isrun" ����

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

        anim.SetBool("isrun", false); // �̵� ����

        transform.position = targetPosition;

        if (Random.value > 0.5f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        isRunning = false; // �̵� �Ϸ� �� �ٽ� ���� ����
    }

    // ��� �̵� �ڷ�ƾ
    IEnumerator Wander()
    {
        if (isMoving || isRunning) yield break; // �̹� �̵� ���̸� ���� �� ��

        isMoving = true;

        float wanderTime = Random.Range(1.5f, 3f); // ����Ÿ��� �ð�
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        float wanderDistance = Random.Range(-3f, 3f); // ���� �Ÿ�
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
                yield break; // Run�� ����Ǹ� ��� �ߴ�
            }

            float t = elapsedTime / wanderTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        anim.SetBool("iswalk", false);
        yield return new WaitForSeconds(1.5f); // ����Ÿ� �� 2�� ���

        isMoving = false; // ��� �̵� ����
        wanderCoroutine = null; // �ڷ�ƾ ���� �ʱ�ȭ
    }

    void Start()
    {
        anim = GetComponent<Animator>(); // Animator ������Ʈ �ʱ�ȭ
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerTransform = playerObject?.transform;

        GameObject areaObject = GameObject.Find("Area");
        if (areaObject != null)
        {
            Area = areaObject.GetComponent<Collider2D>();
        }
        else
        {
            Debug.LogWarning("�̸��� 'Area'�� ������Ʈ�� ã�� �� �����ϴ�.");
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
            if (wanderCoroutine == null) // ��� �ڷ�ƾ ����
            {
                wanderCoroutine = StartCoroutine(Wander());
            }
        }
    }

    void Respawn()
    {
        Spawner.instance.Ground_Spawn(); // �� ������Ʈ ����
        Destroy(gameObject); // ������Ʈ �ı�
    }
}
