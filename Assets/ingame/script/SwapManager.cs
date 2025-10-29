using UnityEngine;
using UnityEngine.EventSystems;

public class SwapManager : MonoBehaviour
{
    public GameObject flashlight;  // 손전등 오브젝트
    public GameObject gun;         // 총 오브젝트
    public GameObject uiImage;     // 전환에 사용될 UI 이미지
    private bool isFlashlightActive = true;  // 손전등 활성화 상태

    [Header("Debug Information")]
    [SerializeField, Tooltip("Is the gun currently active?")]
    private bool isGunActive;

    void Start()
    {
        // 초기 상태 설정
        if (flashlight != null) flashlight.SetActive(true);
        if (gun != null) gun.SetActive(false);

        // GUN 오브젝트 상태에 따라 UI 이미지 활성화/비활성화
        UpdateUIImage();

        // UI 이미지 클릭 이벤트 등록
        if (uiImage != null)
        {
            EventTrigger trigger = uiImage.AddComponent<EventTrigger>();

            // PointerClick 이벤트 추가
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };
            entry.callback.AddListener((data) => { SwapWeapon(); }); // 클릭 시 SwapWeapon 호출
            trigger.triggers.Add(entry);
        }
    }

    public void SwapWeapon()
    {
        if (flashlight != null && gun != null)
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Turn);

            // 상태 전환
            isFlashlightActive = !isFlashlightActive;

            // 손전등과 총 상태 업데이트
            flashlight.SetActive(isFlashlightActive);
            gun.SetActive(!isFlashlightActive);

            // UI 이미지 및 디버그 정보 업데이트
            UpdateUIImage();
        }
    }

    public void SetWeaponState(bool isGun)
    {
        if (flashlight != null && gun != null)
        {
            flashlight.SetActive(!isGun);
            gun.SetActive(isGun);

            isFlashlightActive = !isGun;
            UpdateUIImage();
        }
    }

    private void UpdateUIImage()
    {
        if (uiImage != null && gun != null)
        {
            // GUN 상태에 따라 UI 이미지 활성화/비활성화
            uiImage.SetActive(!gun.activeSelf);

            // 디버그 정보 업데이트
            isGunActive = gun.activeSelf;
        }
    }
}
