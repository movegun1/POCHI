/*using UnityEngine;
using System.Collections; // IEnumerator�� ������ ���ӽ����̽�
using System.Collections.Generic; // �÷����� ����ϴ� ��� �ʿ�


public class Sig : MonoBehaviour
{
    public bool reloading = false;
    public GameObject targetObject; // ���̰� �� ��ü ����
    private bool isCoroutineRunning = false; // �ڷ�ƾ ���� ���� Ȯ�� �÷���

    public void Reloadingcome(bool value)
    {
        targetObject.SetActive(true);
        reloading = value;
        if (reloading)
        {
            if (targetObject != null && targetObject.activeInHierarchy)
            {
                if (!isCoroutineRunning) // �ڷ�ƾ�� ���� ������ ���� ���� ����
                {
                    targetObject.SetActive(true);
                    StartCoroutine(RotateForSeconds(targetObject, 4f));
                }
            }
            else
            {
                Debug.LogError("TargetObject�� Ȱ��ȭ�Ǿ� ���� �ʾƼ� �ڷ�ƾ�� ������ �� �����ϴ�.");
            }
        }
        Debug.Log($"Reloadingcome called, reloading set to {value}");
    }

    private IEnumerator RotateForSeconds(GameObject obj, float seconds)
    {
        isCoroutineRunning = true;
        float elapsedTime = 0f;
        while (elapsedTime < seconds && obj.activeInHierarchy)
        {
            // �� �����Ӹ��� ��ü�� ȸ��
            obj.transform.Rotate(new Vector3(0, 0, -500) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // �ð��� ���� �Ŀ� ��ü�� ��Ȱ��ȭ
        if (obj.activeInHierarchy)
        {
            obj.SetActive(false);
        }
        isCoroutineRunning = false;
    }
}
*/