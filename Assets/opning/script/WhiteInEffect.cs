using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteInEffect : MonoBehaviour
{
    public Image whiteScreen;  // 화면을 덮을 하얀 이미지
    public float fadeDuration = 1.0f;  // 페이드 인 시간

    private void Start()
    {
        StartCoroutine(WhiteIn());
    }

    private IEnumerator WhiteIn()
    {
        float elapsedTime = 0f;
        Color whiteColor = whiteScreen.color;
        whiteColor.a = 1f;  // 처음엔 불투명한 상태로 시작
        whiteScreen.color = whiteColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            whiteColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);  // 투명도를 점차 줄임
            whiteScreen.color = whiteColor;
            yield return null;
        }

        // 페이드가 끝나면 하얀 이미지를 비활성화하거나 제거
        whiteScreen.gameObject.SetActive(false);
    }
}
