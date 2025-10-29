using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CircleCollider2D col;
    private SpriteRenderer spriteRenderer;

    public Sprite[] sprites; // ��������Ʈ �迭
    private int spriteIndex = 0;

    public Transform targetObject; // ��ǥ�� �̵��� ������Ʈ (Transform���� ����)

    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer ������Ʈ ��������

        // ���� ó�� ������ �� �ʱ� ���·� ����
        ResetBall();
    }

    void Update()
    {
        // �� �����Ӹ��� ��������Ʈ�� ���� �̵� ������ ������ ȸ��
        RotateSpriteToMatchDirection();
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
        rb.gravityScale = 1f;

        // ���� ���ư��� ���� ��������Ʈ ����
        StartCoroutine(ChangeSprites());

        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Fire);
    }

    private void RotateSpriteToMatchDirection()
    {
        Vector2 velocity = rb.velocity;

        // ���� ����� �����̰� ���� ���� ȸ�� ó��
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg; // �ӵ� ���͸� ������ ��ȯ
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // ������ �°� ȸ��
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
            Debug.Log("Changing to sprite: " + sprites[spriteIndex].name); // ����� �α� �߰�
            spriteIndex++;
            yield return new WaitForSeconds(0.1f); // ��������Ʈ ���� ���� ����
        }

        // �ִϸ��̼� �Ϸ� �� ��������Ʈ �ε��� ����
        spriteIndex = 0;
    }

    // ���� ó�� ���·� �ʱ�ȭ�ϴ� �Լ�
    public void ResetBall()
    {
        // ��������Ʈ �ʱ�ȭ
        spriteIndex = 0;
        spriteRenderer.sprite = sprites[spriteIndex];

        // Transform�� �ʱ� ���·� ������ (ȸ�� ����)
        transform.rotation = Quaternion.identity;

        // ��ǥ ������Ʈ�� ��ġ�� �̵�
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
        // ������ �� ���� ���¸� �ʱ�ȭ
        ResetBall();
    }

    // �浹 ���� �Լ� (CircleCollider2D�� ���� ��)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹 �� ��������Ʈ�� ó�� ���·� ����
        ResetBall();

        // �ʿ��� ��� ���� ������ ���߱�
        DeactivateRb();
    }

    // Ʈ���� �浹�� ���� ���� �����ϰ� ó�� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ʈ���� �浹 �� ��������Ʈ�� ó�� ���·� ����
        ResetBall();

        // �ʿ��� ��� ���� ������ ���߱�
        DeactivateRb();
    }
}
