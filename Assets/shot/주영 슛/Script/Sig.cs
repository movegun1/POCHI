/*using UnityEngine;
using System.Collections; // IEnumerator를 포함한 네임스페이스
using System.Collections.Generic; // 컬렉션을 사용하는 경우 필요


public class Sig : MonoBehaviour
{
    public bool reloading = false;
    public GameObject targetObject; // 보이게 할 객체 참조
    private bool isCoroutineRunning = false; // 코루틴 실행 여부 확인 플래그

    public void Reloadingcome(bool value)
    {
        targetObject.SetActive(true);
        reloading = value;
        if (reloading)
        {
            if (targetObject != null && targetObject.activeInHierarchy)
            {
                if (!isCoroutineRunning) // 코루틴이 실행 중이지 않을 때만 실행
                {
                    targetObject.SetActive(true);
                    StartCoroutine(RotateForSeconds(targetObject, 4f));
                }
            }
            else
            {
                Debug.LogError("TargetObject가 활성화되어 있지 않아서 코루틴을 실행할 수 없습니다.");
            }
        }
        Debug.Log($"Reloadingcome called, reloading set to {value}");
    }

    private IEnumerator RotateForSeconds(GameObject obj, float seconds)
    {
        isCoroutineRunning = true;
        float elapsedTime = 0f;
        while (elapsedTime < seconds && obj.activeInHierarchy)
        {
            // 매 프레임마다 객체를 회전
            obj.transform.Rotate(new Vector3(0, 0, -500) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 시간이 지난 후에 객체를 비활성화
        if (obj.activeInHierarchy)
        {
            obj.SetActive(false);
        }
        isCoroutineRunning = false;
    }
}
*/