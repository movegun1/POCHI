using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarEffect : MonoBehaviour
{
    public GameObject starPrefab;  // �� Prefab
    public float starFadeInTime = 0.3f;  // ���� ������ ��Ÿ���� �ð�
    public float starFadeOutTime = 1.0f;  // ���� õõ�� ������� �ð�
    public float intervalBetweenStars = 0.5f;  // ���� �����Ǵ� ����
    public Canvas canvas;  // ���� ������ ĵ����

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
        // ȭ�� ũ�� ���ϱ�
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        // ȭ�� ������ X���� ����, Y���� ����ġ�� ������ ���� ��ǥ ����
        Vector2 randomScreenPosition = new Vector2(
            Random.Range(0, screenSize.x),  // X���� ȭ�� ��ü���� ����
            WeightedRandomY(screenSize.y)   // Y���� ����ġ�� ������ ����
        );

        // ��ũ�� ��ǥ�� ĵ���� ��ǥ�� ��ȯ
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(), randomScreenPosition, canvas.worldCamera, out Vector2 localPoint);

        // �� ����
        GameObject newStar = Instantiate(starPrefab, canvas.transform);
        newStar.GetComponent<RectTransform>().anchoredPosition = localPoint;

        // ���� ���̵� ��/�ƿ� �ִϸ��̼� ����
        StartCoroutine(FadeInAndOut(newStar));
    }

    private float WeightedRandomY(float screenHeight)
    {
        // Y�࿡ ����ġ�� �����Ͽ� ���ʿ��� ���� �� ���� �����ǵ��� ����
        float randomY = Random.value;

        // ����ġ ����: ȭ�� ��ܿ� �������� ���� Ŀ�� (Mathf.Pow�� ������ ó��)
        randomY = 1 - Mathf.Pow(1 - randomY, 2);  // randomY ���� �ݴ� ���� ����ġ ����

        // ȭ�� ���̿� ���Ͽ� ���� Y ��ǥ ���
        return randomY * screenHeight;
    }


    private IEnumerator FadeInAndOut(GameObject star)
    {
        Image starImage = star.GetComponent<Image>();
        Color starColor = starImage.color;
        float elapsedTime = 0f;

        // ������ ��Ÿ���� (Fade In)
        while (elapsedTime < starFadeInTime)
        {
            elapsedTime += Time.deltaTime;
            starColor.a = Mathf.Lerp(0f, 1f, elapsedTime / starFadeInTime);
            starImage.color = starColor;
            yield return null;
        }

        // ������ ������� ���� �ణ�� ���
        yield return new WaitForSeconds(starFadeOutTime);

        // ������ ������� (Fade Out)
        elapsedTime = 0f;
        while (elapsedTime < starFadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            starColor.a = Mathf.Lerp(1f, 0f, elapsedTime / starFadeOutTime);
            starImage.color = starColor;
            yield return null;
        }

        // �� ����
        Destroy(star);
    }
}
