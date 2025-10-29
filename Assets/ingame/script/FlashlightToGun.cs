using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightToGun : MonoBehaviour
{
    public CircleCollider2D touchCollider; // CircleCollider2D (터치 영역)
    public bool isTouchingArea = false;    // 터치 영역에 접촉 여부를 나타내는 플래그

    void Update()
    {
        // 터치 입력 처리
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // 터치가 시작되었을 때
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                // 터치 영역 안에서 터치가 발생했는지 확인
                if (hit.collider != null && hit.collider == touchCollider)
                {
                    isTouchingArea = true;  // 터치 영역 안에서 터치 중임을 표시
                }
            }

            // 터치가 끝났을 때
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouchingArea = false; // 터치가 끝났으므로 플래그 해제
            }
        }

        // 마우스 클릭 처리 (에디터 환경에서 테스트용)
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider == touchCollider)
            {
                isTouchingArea = true; // 터치 영역 안에서 클릭 중임을 표시
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isTouchingArea = false; // 클릭이 끝났으므로 플래그 해제
        }
    }
}
