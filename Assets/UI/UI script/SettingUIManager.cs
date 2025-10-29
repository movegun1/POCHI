using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUIManager : MonoBehaviour
{
    public GameObject homeUI; // 홈 UI
    public GameObject settingsUI; // 설정 UI

    // 설정 UI를 켜기
    public void OpenSettings()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        settingsUI.SetActive(true); // 설정 UI 활성화
        homeUI.SetActive(false);   // 홈 UI 비활성화
    }

    // 설정 UI를 닫기
    public void CloseSettings()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        settingsUI.SetActive(false); // 설정 UI 비활성화
        homeUI.SetActive(true);      // 홈 UI 활성화
    }
}
