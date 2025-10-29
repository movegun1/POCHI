using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sky_animal1 : MonoBehaviour
{
    public Collider2D Area;  // Area ������ Ȯ���ϱ� ���� Collider2D

    // Start is called before the first frame update
    void Start()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = 1.2f;  // �ʱ� y ��ġ ����
        transform.position = newPosition;

        // "Area"��� �̸��� ������Ʈ�� ã�� Collider2D�� �Ҵ�
        GameObject areaObject = GameObject.Find("Area");
        if (areaObject != null)
        {
            Area = areaObject.GetComponent<Collider2D>();
            if (Area == null)
            {
                Debug.LogWarning("Area ������Ʈ�� Collider2D ������Ʈ�� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogWarning("�̸��� 'Area'�� ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Time.deltaTime * 4f;
        transform.Translate(-Vector2.right * speed);  // ������Ʈ�� �������� �̵�

        // Area ���� ������ ������� Ȯ��
        if (Area == null)
        {
            return;
        }

        if (!Area.bounds.Contains(transform.position))
        {
            Vector3 newPosition = transform.position;

            // x�� ��ġ�� �ݴ������� �̵�
            if (transform.position.x > Area.bounds.max.x)
            {
                // ���� �ð� ��� �� Fly_Spawn ȣ�� �� ������Ʈ �ı�
                Respawn();
            }
            else if (transform.position.x < Area.bounds.min.x)
            {
                // ���� �ð� ��� �� Fly_Spawn ȣ�� �� ������Ʈ �ı�
                Respawn();
            }
        }
    }
    void Respawn()
    {
        Spawner.instance.Fly_Spawn();  // �� ������Ʈ ����
        Destroy(gameObject);  // ������Ʈ �ı�
    }
}
