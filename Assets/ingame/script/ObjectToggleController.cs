using UnityEngine;

public class ObjectToggleController : MonoBehaviour
{
    public GameObject AObject; // A ������Ʈ ����
    public GameObject BObject; // B ������Ʈ ����
    public GameObject CObject; // C ������Ʈ ����

    void Update()
    {
        // C ������Ʈ�� Ȱ��ȭ ���̸� B ��Ȱ��ȭ
        if (CObject != null && CObject.activeSelf)
        {
            if (BObject != null && BObject.activeSelf)
            {
                BObject.SetActive(false);
            }
            return; // C�� Ȱ��ȭ�� ��� �ٸ� ������ �ߴ�
        }

        // A ������Ʈ�� ��Ȱ��ȭ�Ǿ��� �� B ������Ʈ Ȱ��ȭ
        if (AObject != null && BObject != null)
        {
            if (!AObject.activeSelf && !BObject.activeSelf)
            {
                BObject.SetActive(true);
            }
            // A ������Ʈ�� Ȱ��ȭ�Ǿ��� �� B ������Ʈ ��Ȱ��ȭ
            else if (AObject.activeSelf && BObject.activeSelf)
            {
                BObject.SetActive(false);
            }
        }
    }
}
