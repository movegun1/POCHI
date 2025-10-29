using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BookManager : MonoBehaviour
{
    [SerializeField] private GameObject aObject; // 대상 UI 오브젝트
    [SerializeField] private float fadeDuration = 0.5f; // 페이드 인/아웃 시간
    [SerializeField] private float waitDuration = 0.7f; // 유지 시간

    private CanvasGroup canvasGroup;

    private void Start()
    {
        // aObject의 CanvasGroup을 가져오거나 새로 추가
        if (aObject != null)
        {
            canvasGroup = aObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = aObject.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0; // 초기 알파값 설정
            aObject.SetActive(false); // 초기 상태 비활성화
        }
        else
        {
            Debug.LogError("aObject가 설정되지 않았습니다!");
        }
    }

    // 버튼 클릭 시 호출되는 메서드
    public void OnButtonClicked()
    {
        if (aObject != null && canvasGroup != null)
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Error);
            StartCoroutine(FadeInOutRoutine());
        }
        else
        {
            Debug.LogError("aObject 또는 CanvasGroup이 제대로 설정되지 않았습니다!");
        }
    }

    // 페이드 인 -> 대기 -> 페이드 아웃 루틴
    private IEnumerator FadeInOutRoutine()
    {
        // 초기화
        aObject.SetActive(true);

        // 페이드 인
        yield return StartCoroutine(Fade(0, 1, fadeDuration));

        // 유지 시간
        yield return new WaitForSeconds(waitDuration);

        // 페이드 아웃
        yield return StartCoroutine(Fade(1, 0, fadeDuration));

        // 비활성화
        aObject.SetActive(false);
    }

    // 알파값을 서서히 변화시키는 코루틴
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
