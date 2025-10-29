using UnityEngine;

public class Change : MonoBehaviour
{
    public Material NewMaterial; // ���ο� ������ ���⿡ �Ҵ��ϼ���.

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� Player �±׸� ���� ��ü���� Ȯ���մϴ�.
        if (other.CompareTag("Ball"))
        {
            Renderer renderer = GetComponent<Renderer>();

            if (renderer != null && NewMaterial != null)
            {
                renderer.material = NewMaterial; // ���ο� ������ �����մϴ�
                Debug.Log("��������");
            }
        }
    }
}
