using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    // �⺻ ī�޶� ���� ����
    public Transform player;        // ���ΰ��� Transform
    public Vector3 offset;          // ���ΰ��� ī�޶� ���� �Ÿ�
    public float smoothSpeed = 0.05f; // ī�޶� �̵� �ӵ� (�� �ε巴�� ����)

    // Ư���� �̺�Ʈ ����
    public bool specialEvent = false; // Ư���� �̺�Ʈ ����
    public GameObject aObject;        // A ������Ʈ
    public GameObject bObject;        // B ������Ʈ
    public float distanceThreshold = 10.0f; // A�� B �� �Ÿ� ����
    public Vector3 aOffset;           // A ������Ʈ�� ���� ���� ī�޶� ������

    // ī�޶� �̵� ���� ����
    public float minY = -10f;         // Y�� �ּ� ��
    public float maxY = 10f;          // Y�� �ִ� ��

    private bool isFollowingA = false; // A ������Ʈ�� ���󰡴� ����
    private Vector3 velocity = Vector3.zero; // SmoothDamp���� ����� �ӵ� ���� ����

    private void LateUpdate()
    {
        // A ������Ʈ�� ���󰡴� ������ ����
        if (aObject.activeSelf && Vector3.Distance(aObject.transform.position, bObject.transform.position) > distanceThreshold)
        {
            ActivateSpecialEvent(); // ����� �̺�Ʈ Ȱ��ȭ
            FollowAObject();
        }
        else
        {
            // �⺻ ī�޶� ���� ó��
            DeactivateSpecialEvent(); // ����� �̺�Ʈ ��Ȱ��ȭ
            HandleBasicFollow();
        }

        // ī�޶� Y�� �̵� ����
        ClampCameraYPosition();
    }

    private void HandleBasicFollow()
    {
        if (!specialEvent) // Ư���� �̺�Ʈ�� �ƴ� ���� ����
        {
            Vector3 desiredPosition = player.position + offset; // ��ǥ ��ġ
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed); // �� �ε巴�� �̵�
            transform.position = smoothedPosition;
        }
    }

    private void FollowAObject()
    {
        // A ������Ʈ �߽����� ī�޶� ��ġ ���� + aOffset ����
        Vector3 desiredPosition = new Vector3(
            aObject.transform.position.x + aOffset.x,
            aObject.transform.position.y + aOffset.y,
            aObject.transform.position.z + aOffset.z
        );
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }

    private void ClampCameraYPosition()
    {
        // Y�� ��ġ ����
        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = clampedPosition;
    }

    private void ActivateSpecialEvent()
    {
        if (!specialEvent)
        {
            specialEvent = true;
            Debug.Log("Special Event Activated");
        }
    }

    private void DeactivateSpecialEvent()
    {
        if (specialEvent)
        {
            specialEvent = false;
            Debug.Log("Special Event Deactivated");
        }
    }
}
