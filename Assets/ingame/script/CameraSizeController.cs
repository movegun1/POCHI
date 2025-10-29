using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeController : MonoBehaviour
{
    public GameObject aObject;        // A ������Ʈ ����
    public GameObject bObject;        // B ������Ʈ ����
    public Camera mainCamera;         // ���� ī�޶� ����
    public float enlargedSize = 10f;  // A ������Ʈ�� ������ ���� ī�޶� ũ��
    private float originalSize;       // �ʱ� ī�޶� ũ�� ����
    public float transitionSpeed = 2f; // ũ�� ��ȯ �ӵ�

    private float targetSize;         // ��ǥ ī�޶� ũ��

    void Start()
    {
        // ���� ī�޶��� �ʱ� ũ�⸦ �����ϰ�, �ʱ� ��ǥ ũ�⸦ ����
        originalSize = mainCamera.orthographicSize;
        targetSize = originalSize;
    }

    void Update()
    {
        // A ������Ʈ�� Ȱ��ȭ�Ǹ� ī�޶� ũ�� Ȯ��
        if (aObject.activeSelf)
        {
            targetSize = enlargedSize;
        }
        // B ������Ʈ�� Ȱ��ȭ�Ǹ� ī�޶� ũ�⸦ ������� �ǵ���
        else if (bObject.activeSelf)
        {
            targetSize = originalSize;
        }

        // ���� ī�޶� ũ�⸦ ��ǥ ũ��� ���������� ����
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, transitionSpeed * Time.deltaTime);
    }
}
