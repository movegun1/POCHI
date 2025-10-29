using UnityEngine;

public class ObjectToggleController : MonoBehaviour
{
    public GameObject AObject; // A 오브젝트 참조
    public GameObject BObject; // B 오브젝트 참조
    public GameObject CObject; // C 오브젝트 참조

    void Update()
    {
        // C 오브젝트가 활성화 중이면 B 비활성화
        if (CObject != null && CObject.activeSelf)
        {
            if (BObject != null && BObject.activeSelf)
            {
                BObject.SetActive(false);
            }
            return; // C가 활성화된 경우 다른 동작을 중단
        }

        // A 오브젝트가 비활성화되었을 때 B 오브젝트 활성화
        if (AObject != null && BObject != null)
        {
            if (!AObject.activeSelf && !BObject.activeSelf)
            {
                BObject.SetActive(true);
            }
            // A 오브젝트가 활성화되었을 때 B 오브젝트 비활성화
            else if (AObject.activeSelf && BObject.activeSelf)
            {
                BObject.SetActive(false);
            }
        }
    }
}
