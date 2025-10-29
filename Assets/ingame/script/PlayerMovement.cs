using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public FlashlightToGun flashlightScript;
    public GameObject flashlight;
    public GameObject gun;
    public SwapManager swapManager; // SwapManager ����

    private float stepSoundCooldown = 0.45f; // �ּ� ���� ����
    private float lastStepSoundTime = -0.45f; // ���������� ����� �ð� �ʱ�ȭ

    void Start()
    {
        ActivateFlashlight();
    }

    void Update()
    {
        if (flashlightScript != null && !flashlightScript.isTouchingArea && !IsPointerOverUI())
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = Input.mousePosition;

                if (mousePosition.x > Screen.width / 2)
                {
                    MoveRight();
                }
                else
                {
                    MoveLeft();
                }

                ActivateFlashlight();
            }
        }
    }

    void MoveRight()
    {
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        PlayStepSound();
    }

    void MoveLeft()
    {
        if (transform.position.x > 0)
        {
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
        PlayStepSound();
    }

    private void PlayStepSound()
    {
        // ���� �ð��� ������ ��� �ð��� ��
        if (Time.time - lastStepSoundTime >= stepSoundCooldown)
        {
            Audiomanager.instance.PlaySfx(Audiomanager.Sfx.Step);
            lastStepSoundTime = Time.time; // ������ ��� �ð� ������Ʈ
        }
    }

    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null) return false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
        }
        else if (Input.GetMouseButton(0))
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        return false;
    }

    private void ActivateFlashlight()
    {
        if (flashlight != null) flashlight.SetActive(true);
        if (gun != null) gun.SetActive(false);

        if (swapManager != null)
        {
            swapManager.SetWeaponState(false); // SwapManager���� flashlight Ȱ��ȭ ���� �˸�
        }
    }
}
