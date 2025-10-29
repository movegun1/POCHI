using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIOnOff : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject targetUI; // Ȱ��ȭ/��Ȱ��ȭ�� ��� UI

    // Ŭ�� �̺�Ʈ�� �߻��ϸ� ����
    public void OnPointerClick(PointerEventData eventData)
    {
        if (targetUI != null)
        {
            // ���� ���¸� �����Ͽ� Ȱ��ȭ/��Ȱ��ȭ
            targetUI.SetActive(!targetUI.activeSelf);
        }
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
    }
}
