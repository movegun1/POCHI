using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    public GameObject touchEffectPrefab;  // 터치할 때 생성할 이펙트 프리팹
    public float effectDuration = 2f;     // 이펙트가 유지될 시간 (초)

    void Update()
    {
        // 터치가 발생했는지 확인 (모바일 환경)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // 터치가 시작되었을 때 (TouchPhase.Began)
            if (touch.phase == TouchPhase.Began)
            {
                // 터치한 위치를 스크린 좌표에서 월드 좌표로 변환
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

                // 이펙트 프리팹을 터치한 위치에 생성
                GameObject effectInstance = Instantiate(touchEffectPrefab, touchPosition, Quaternion.identity);

                // 일정 시간 후에 이펙트를 삭제
                Destroy(effectInstance, effectDuration);
            }
        }

        // 마우스 클릭으로 테스트 (에디터 환경)
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭 위치를 스크린 좌표에서 월드 좌표로 변환
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

            // 이펙트 프리팹을 클릭한 위치에 생성
            GameObject effectInstance = Instantiate(touchEffectPrefab, mousePosition, Quaternion.identity);

            // 일정 시간 후에 이펙트를 삭제
            Destroy(effectInstance, effectDuration);
        }
    }
}
