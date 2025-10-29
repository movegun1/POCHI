using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    public GameObject touchEffectPrefab;  // ��ġ�� �� ������ ����Ʈ ������
    public float effectDuration = 2f;     // ����Ʈ�� ������ �ð� (��)

    void Update()
    {
        // ��ġ�� �߻��ߴ��� Ȯ�� (����� ȯ��)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ��ġ�� ���۵Ǿ��� �� (TouchPhase.Began)
            if (touch.phase == TouchPhase.Began)
            {
                // ��ġ�� ��ġ�� ��ũ�� ��ǥ���� ���� ��ǥ�� ��ȯ
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

                // ����Ʈ �������� ��ġ�� ��ġ�� ����
                GameObject effectInstance = Instantiate(touchEffectPrefab, touchPosition, Quaternion.identity);

                // ���� �ð� �Ŀ� ����Ʈ�� ����
                Destroy(effectInstance, effectDuration);
            }
        }

        // ���콺 Ŭ������ �׽�Ʈ (������ ȯ��)
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 Ŭ�� ��ġ�� ��ũ�� ��ǥ���� ���� ��ǥ�� ��ȯ
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

            // ����Ʈ �������� Ŭ���� ��ġ�� ����
            GameObject effectInstance = Instantiate(touchEffectPrefab, mousePosition, Quaternion.identity);

            // ���� �ð� �Ŀ� ����Ʈ�� ����
            Destroy(effectInstance, effectDuration);
        }
    }
}
