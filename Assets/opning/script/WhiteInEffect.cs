using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteInEffect : MonoBehaviour
{
    public Image whiteScreen;  // ȭ���� ���� �Ͼ� �̹���
    public float fadeDuration = 1.0f;  // ���̵� �� �ð�

    private void Start()
    {
        StartCoroutine(WhiteIn());
    }

    private IEnumerator WhiteIn()
    {
        float elapsedTime = 0f;
        Color whiteColor = whiteScreen.color;
        whiteColor.a = 1f;  // ó���� �������� ���·� ����
        whiteScreen.color = whiteColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            whiteColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);  // ������ ���� ����
            whiteScreen.color = whiteColor;
            yield return null;
        }

        // ���̵尡 ������ �Ͼ� �̹����� ��Ȱ��ȭ�ϰų� ����
        whiteScreen.gameObject.SetActive(false);
    }
}
