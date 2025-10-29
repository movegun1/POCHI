using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BookManager : MonoBehaviour
{
    [SerializeField] private GameObject aObject; // ��� UI ������Ʈ
    [SerializeField] private float fadeDuration = 0.5f; // ���̵� ��/�ƿ� �ð�
    [SerializeField] private float waitDuration = 0.7f; // ���� �ð�

    private CanvasGroup canvasGroup;

    private void Start()
    {
        // aObject�� CanvasGroup�� �������ų� ���� �߰�
        if (aObject != null)
        {
            canvasGroup = aObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = aObject.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0; // �ʱ� ���İ� ����
            aObject.SetActive(false); // �ʱ� ���� ��Ȱ��ȭ
        }
        else
        {
            Debug.LogError("aObject�� �������� �ʾҽ��ϴ�!");
        }
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnButtonClicked()
    {
        if (aObject != null && canvasGroup != null)
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Error);
            StartCoroutine(FadeInOutRoutine());
        }
        else
        {
            Debug.LogError("aObject �Ǵ� CanvasGroup�� ����� �������� �ʾҽ��ϴ�!");
        }
    }

    // ���̵� �� -> ��� -> ���̵� �ƿ� ��ƾ
    private IEnumerator FadeInOutRoutine()
    {
        // �ʱ�ȭ
        aObject.SetActive(true);

        // ���̵� ��
        yield return StartCoroutine(Fade(0, 1, fadeDuration));

        // ���� �ð�
        yield return new WaitForSeconds(waitDuration);

        // ���̵� �ƿ�
        yield return StartCoroutine(Fade(1, 0, fadeDuration));

        // ��Ȱ��ȭ
        aObject.SetActive(false);
    }

    // ���İ��� ������ ��ȭ��Ű�� �ڷ�ƾ
    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }

        // Ensure final alpha value is set
        canvasGroup.alpha = endAlpha;
    }
}
