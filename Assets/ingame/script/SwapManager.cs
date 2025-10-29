using UnityEngine;
using UnityEngine.EventSystems;

public class SwapManager : MonoBehaviour
{
    public GameObject flashlight;  // ������ ������Ʈ
    public GameObject gun;         // �� ������Ʈ
    public GameObject uiImage;     // ��ȯ�� ���� UI �̹���
    private bool isFlashlightActive = true;  // ������ Ȱ��ȭ ����

    [Header("Debug Information")]
    [SerializeField, Tooltip("Is the gun currently active?")]
    private bool isGunActive;

    void Start()
    {
        // �ʱ� ���� ����
        if (flashlight != null) flashlight.SetActive(true);
        if (gun != null) gun.SetActive(false);

        // GUN ������Ʈ ���¿� ���� UI �̹��� Ȱ��ȭ/��Ȱ��ȭ
        UpdateUIImage();

        // UI �̹��� Ŭ�� �̺�Ʈ ���
        if (uiImage != null)
        {
            EventTrigger trigger = uiImage.AddComponent<EventTrigger>();

            // PointerClick �̺�Ʈ �߰�
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };
            entry.callback.AddListener((data) => { SwapWeapon(); }); // Ŭ�� �� SwapWeapon ȣ��
            trigger.triggers.Add(entry);
        }
    }

    public void SwapWeapon()
    {
        if (flashlight != null && gun != null)
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Turn);

            // ���� ��ȯ
            isFlashlightActive = !isFlashlightActive;

            // ������� �� ���� ������Ʈ
            flashlight.SetActive(isFlashlightActive);
            gun.SetActive(!isFlashlightActive);

            // UI �̹��� �� ����� ���� ������Ʈ
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
            // GUN ���¿� ���� UI �̹��� Ȱ��ȭ/��Ȱ��ȭ
            uiImage.SetActive(!gun.activeSelf);

            // ����� ���� ������Ʈ
            isGunActive = gun.activeSelf;
        }
    }
}
