using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenUICt : MonoBehaviour
{
    public static InvenUICt instance;
    public GameObject UIPanel;

    private void Awake()
    {
        instance = this;
    }
    public void UiOn()
    {
        UIPanel.SetActive(true);
    }

    public void UiOff()
    {
        UIPanel.SetActive(false);
    }
}
