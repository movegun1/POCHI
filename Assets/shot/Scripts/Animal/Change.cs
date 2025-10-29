using UnityEngine;

public class Change : MonoBehaviour
{
    public Material NewMaterial; // 새로운 재질을 여기에 할당하세요.

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체가 Player 태그를 가진 객체인지 확인합니다.
        if (other.CompareTag("Ball"))
        {
            Renderer renderer = GetComponent<Renderer>();

            if (renderer != null && NewMaterial != null)
            {
                renderer.material = NewMaterial; // 새로운 재질로 변경합니다
                Debug.Log("재질변경");
            }
        }
    }
}
