using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inpntmove : MonoBehaviour
{
    public Transform player;  // ���ΰ��� Transform�� �������� ���� ����
    private Vector3 lastPlayerPosition;  // ���ΰ��� ���� ��ġ
    Material mat;  // ��濡 ���Ǵ� Material (����) ����
    float distance;  // ����� �̵��� �Ÿ�

    [Range(0f, 0.5f)]
    public float speed = 0.1f;  // �з����� ȿ���� �ӵ� (0.0 ~ 0.5 �������� ���� ����)

    void Start()
    {
        // �� ������Ʈ�� Renderer���� ����ϴ� Material�� ������
        mat = GetComponent<Renderer>().material;

        // ���ΰ��� �ʱ� ��ġ ����
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        // ���ΰ��� ������ �Ÿ��� ���
        Vector3 playerDelta = player.position - lastPlayerPosition;

        // ���ΰ��� �̵����� ��쿡�� ����� �̵���Ŵ
        if (playerDelta.x != 0)
        {
            // ���ΰ� �̵� �Ÿ��� ���� ����� �̵�
            distance += playerDelta.x * speed;
            mat.SetTextureOffset("_MainTex", Vector2.right * distance);
        }

        // ���� ���ΰ� ��ġ�� �����Ͽ� ���� �����ӿ��� ��
        lastPlayerPosition = player.position;
    }
}
