using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scription : MonoBehaviour
{
    public static Scription instance; // 싱글턴 인스턴스

    private CanvasGroup canvasGroup; // CanvasGroup 참조
        
    void Awake()
    {
        // 싱글턴 인스턴스 설정
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 다른 인스턴스가 이미 존재하면 이 오브젝트 파괴
        }
    }

    void Start()
    {
        // 이 오브젝트의 CanvasGroup 컴포넌트를 가져옴
        canvasGroup = GetComponent<CanvasGroup>();

        // CanvasGroup이 없으면 추가
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

    // UI를 보이게 하는 메서드
    private void ShowUI()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f; // UI를 보이게 함
            canvasGroup.interactable = true; // 상호작용 가능하게 함
            canvasGroup.blocksRaycasts = true; // Raycast를 막음
        }
    }

    // UI를 숨기는 메서드
    private void HideUI()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f; // UI를 숨김
            canvasGroup.interactable = false; // 상호작용 불가능하게 함
            canvasGroup.blocksRaycasts = false; // Raycast를 막지 않음
        }
    }
}
