using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    public GameObject targetObject; // �Ÿ� ����� ��� ������Ʈ
    private Vector3 previousPosition; // ���� ��ġ ����
    public float totalDistance = 0f; // �� �̵� �Ÿ�
    private Text distanceText; // UI Text ������Ʈ ����
    private Player playerScript; // Player ��ũ��Ʈ ����

    // Start�� ù ������ ������Ʈ ���� ȣ��˴ϴ�.
    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target Object�� �������� �ʾҽ��ϴ�.");
            return;
        }

        distanceText = GetComponent<Text>(); // UI Text ������Ʈ ��������
        previousPosition = targetObject.transform.position; // ���� ��ġ �ʱ�ȭ
        playerScript = targetObject.GetComponent<Player>(); // Player ��ũ��Ʈ ��������
    }

    // Update�� �� ������ ȣ��˴ϴ�.
    void Update()
    {
        if (targetObject == null || playerScript == null)
        {
            Debug.LogError("Target Object�� Player Script�� �������� �ʾҽ��ϴ�.");
            return;
        }

        // ���� ��ġ�� ���� ��ġ ������ �̵� �Ÿ� ���
        float distanceMoved = Vector3.Distance(targetObject.transform.position, previousPosition);

        // �̵��� �Ÿ��� �� �̵� �Ÿ��� �߰�
        totalDistance += distanceMoved;

        // ���� ��ġ�� ���� ��ġ�� ������Ʈ
        previousPosition = targetObject.transform.position;

        // �� �̵� �Ÿ��� UI�� ǥ�� (������ �ݿø�)
        distanceText.text = Mathf.RoundToInt(totalDistance) + "m";

        // 200m ���� �� �÷��̾� �̵� ����
        if (totalDistance >= 200)
        {
            playerScript.GoStop();
            distanceText.text = "�Ÿ��ʰ�"; // UI�� �̵� ���� ǥ��
            return; // �̵��� ������ �� �ٷ� �����Ͽ� �� �̻� �������� ����
        }

        // ���콺 Ŭ�� �� �÷��̾� �̵� ����
        if (Input.GetMouseButton(0))
        {
            playerScript.Go(); // �̵� ����
        }
        else
        {
            playerScript.GoStop(); // �̵� ����
        }
    }
}
