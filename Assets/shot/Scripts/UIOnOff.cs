using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIOnOff : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject targetUI; // 활성화/비활성화할 대상 UI

    // 클릭 이벤트가 발생하면 실행
    public void OnPointerClick(PointerEventData eventData)
    {
        if (targetUI != null)
        {
            // 현재 상태를 반전하여 활성화/비활성화
            targetUI.SetActive(!targetUI.activeSelf);
        }
        Audiomanager.instance.PlaySfx(Audiomanager.Sfx.UI);
    }
}
