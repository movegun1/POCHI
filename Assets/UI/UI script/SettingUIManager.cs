using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUIManager : MonoBehaviour
{
    public GameObject homeUI; // Ȩ UI
    public GameObject settingsUI; // ���� UI

    // ���� UI�� �ѱ�
    public void OpenSettings()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        settingsUI.SetActive(true); // ���� UI Ȱ��ȭ
        homeUI.SetActive(false);   // Ȩ UI ��Ȱ��ȭ
    }

    // ���� UI�� �ݱ�
    public void CloseSettings()
    {
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
        settingsUI.SetActive(false); // ���� UI ��Ȱ��ȭ
        homeUI.SetActive(true);      // Ȩ UI Ȱ��ȭ
    }
}
