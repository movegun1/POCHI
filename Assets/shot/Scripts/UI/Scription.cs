using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scription : MonoBehaviour
{
    public static Scription instance; // �̱��� �ν��Ͻ�

    private CanvasGroup canvasGroup; // CanvasGroup ����
        
    void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ٸ� �ν��Ͻ��� �̹� �����ϸ� �� ������Ʈ �ı�
        }
    }

    void Start()
    {
        // �� ������Ʈ�� CanvasGroup ������Ʈ�� ������
        canvasGroup = GetComponent<CanvasGroup>();

        // CanvasGroup�� ������ �߰�
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        HideUI();
    }

    public void Turnon()
    {
        Debug.Log("Turnon method called");
        ShowUI();
    }

    public void Turnoff()
    {
        Debug.Log("Turnoff method called");
        HideUI();
    }

    // UI�� ���̰� �ϴ� �޼���
    private void ShowUI()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f; // UI�� ���̰� ��
            canvasGroup.interactable = true; // ��ȣ�ۿ� �����ϰ� ��
            canvasGroup.blocksRaycasts = true; // Raycast�� ����
        }
    }

    // UI�� ����� �޼���
    private void HideUI()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f; // UI�� ����
            canvasGroup.interactable = false; // ��ȣ�ۿ� �Ұ����ϰ� ��
            canvasGroup.blocksRaycasts = false; // Raycast�� ���� ����
        }
    }
}
