using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image whiteScreen;  // ȭ��Ʈ �ΰ� ȭ��Ʈ �ƿ��� ������ �Ͼ� ȭ�� Image (UI)
    public float fadeDuration = 1.0f;  // ȭ��Ʈ �ƿ� ȿ�� �ð�
    public string nextSceneName;  // ��ȯ�� ���� ���� �̸�

    private bool isTransitioning = false;  // �� ��ȯ ������ ���� Ȯ��

    void Update()
    {
        // ��ȣ�ۿ��� �����Ͽ� ȭ��Ʈ �ƿ� ���� (���콺 Ŭ��, Ű���� �Է� ��)
        if (!isTransitioning && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            StartCoroutine(WhiteOutAndLoadNextScene());
        }
    }

    private IEnumerator WhiteOutAndLoadNextScene()
    {
        isTransitioning = true;  // ��ȣ�ۿ� �ߺ� ����
        float elapsedTime = 0f;
        Color screenColor = whiteScreen.color;

        // �̹� ȭ��Ʈ �ο��� Alpha�� 0���� �Ǿ������Ƿ� ������ �� Alpha�� 0���� ��
        screenColor.a = 0f;
        whiteScreen.gameObject.SetActive(true);  // ȭ��Ʈ �� ������Ʈ �ٽ� Ȱ��ȭ

        // ȭ��Ʈ �ƿ� ȿ�� (Alpha ���� ���� 1�� ������Ŵ)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            screenColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);  // Alpha �� ����
            whiteScreen.color = screenColor;
            yield return null;
        }

        // ȭ��Ʈ �ƿ� �Ϸ� �� �� ��ȯ
        SceneManager.LoadScene(nextSceneName);
    }
}
