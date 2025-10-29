using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarEffect : MonoBehaviour
{
    public GameObject starPrefab;  // 별 Prefab
    public float starFadeInTime = 0.3f;  // 별이 서서히 나타나는 시간
    public float starFadeOutTime = 1.0f;  // 별이 천천히 사라지는 시간
    public float intervalBetweenStars = 0.5f;  // 별이 생성되는 간격
    public Canvas canvas;  // 별이 생성될 캔버스

    private void Start()
    {
        StartCoroutine(StarSpawner());
    }

    private IEnumerator StarSpawner()
    {
        while (true)
        {
            SpawnStar();
            yield return new WaitForSeconds(intervalBetweenStars);
        }
    }

    private void SpawnStar()
    {
        // 화면 크기 구하기
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        // 화면 내에서 X축은 랜덤, Y축은 가중치를 적용한 랜덤 좌표 생성
        Vector2 randomScreenPosition = new Vector2(
            Random.Range(0, screenSize.x),  // X축은 화면 전체에서 랜덤
            WeightedRandomY(screenSize.y)   // Y축은 가중치를 적용한 랜덤
        );

        // 스크린 좌표를 캔버스 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(), randomScreenPosition, canvas.worldCamera, out Vector2 localPoint);

        // 별 생성
        GameObject newStar = Instantiate(starPrefab, canvas.transform);
        newStar.GetComponent<RectTransform>().anchoredPosition = localPoint;

        // 별의 페이드 인/아웃 애니메이션 시작
        StartCoroutine(FadeInAndOut(newStar));
    }

    private float WeightedRandomY(float screenHeight)
    {
        // Y축에 가중치를 적용하여 위쪽에서 별이 더 많이 생성되도록 수정
        float randomY = Random.value;

        // 가중치 적용: 화면 상단에 가까울수록 값이 커짐 (Mathf.Pow의 역으로 처리)
        randomY = 1 - Mathf.Pow(1 - randomY, 2);  // randomY 값에 반대 방향 가중치 적용

        // 화면 높이와 곱하여 실제 Y 좌표 계산
        return randomY * screenHeight;
    }


    private IEnumerator FadeInAndOut(GameObject star)
    {
        Image starImage = star.GetComponent<Image>();
        Color starColor = starImage.color;
        float elapsedTime = 0f;

        // 서서히 나타나기 (Fade In)
        while (elapsedTime < starFadeInTime)
        {
            elapsedTime += Time.deltaTime;
            starColor.a = Mathf.Lerp(0f, 1f, elapsedTime / starFadeInTime);
            starImage.color = starColor;
            yield return null;
        }

        // 서서히 사라지기 전에 약간의 대기
        yield return new WaitForSeconds(starFadeOutTime);

        // 서서히 사라지기 (Fade Out)
        elapsedTime = 0f;
        while (elapsedTime < starFadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            starColor.a = Mathf.Lerp(1f, 0f, elapsedTime / starFadeOutTime);
            starImage.color = starColor;
            yield return null;
        }

        // 별 제거
        Destroy(star);
    }
}
