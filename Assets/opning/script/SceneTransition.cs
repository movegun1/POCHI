using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image whiteScreen;  // 화이트 인과 화이트 아웃에 재사용할 하얀 화면 Image (UI)
    public float fadeDuration = 1.0f;  // 화이트 아웃 효과 시간
    public string nextSceneName;  // 전환될 다음 씬의 이름

    private bool isTransitioning = false;  // 씬 전환 중인지 여부 확인

    void Update()
    {
        // 상호작용을 감지하여 화이트 아웃 시작 (마우스 클릭, 키보드 입력 등)
        if (!isTransitioning && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            StartCoroutine(WhiteOutAndLoadNextScene());
        }
    }

    private IEnumerator WhiteOutAndLoadNextScene()
    {
        isTransitioning = true;  // 상호작용 중복 방지
        float elapsedTime = 0f;
        Color screenColor = whiteScreen.color;

        // 이미 화이트 인에서 Alpha가 0으로 되어있으므로 시작할 때 Alpha는 0으로 둠
        screenColor.a = 0f;
        whiteScreen.gameObject.SetActive(true);  // 화이트 인 오브젝트 다시 활성화

        // 화이트 아웃 효과 (Alpha 값을 점차 1로 증가시킴)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            screenColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);  // Alpha 값 증가
            whiteScreen.color = screenColor;
            yield return null;
        }

        // 화이트 아웃 완료 후 씬 전환
        SceneManager.LoadScene(nextSceneName);
    }
}
