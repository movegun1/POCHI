using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    public GameObject targetObject; // 거리 계산할 대상 오브젝트
    private Vector3 previousPosition; // 이전 위치 저장
    public float totalDistance = 0f; // 총 이동 거리
    private Text distanceText; // UI Text 컴포넌트 참조
    private Player playerScript; // Player 스크립트 참조

    // Start는 첫 프레임 업데이트 전에 호출됩니다.
    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target Object가 설정되지 않았습니다.");
            return;
        }

        distanceText = GetComponent<Text>(); // UI Text 컴포넌트 가져오기
        previousPosition = targetObject.transform.position; // 이전 위치 초기화
        playerScript = targetObject.GetComponent<Player>(); // Player 스크립트 가져오기
    }

    // Update는 매 프레임 호출됩니다.
    void Update()
    {
        if (targetObject == null || playerScript == null)
        {
            Debug.LogError("Target Object나 Player Script가 설정되지 않았습니다.");
            return;
        }

        // 현재 위치와 이전 위치 사이의 이동 거리 계산
        float distanceMoved = Vector3.Distance(targetObject.transform.position, previousPosition);

        // 이동한 거리를 총 이동 거리에 추가
        totalDistance += distanceMoved;

        // 이전 위치를 현재 위치로 업데이트
        previousPosition = targetObject.transform.position;

        // 총 이동 거리를 UI에 표시 (정수로 반올림)
        distanceText.text = Mathf.RoundToInt(totalDistance) + "m";

        // 200m 도달 시 플레이어 이동 중지
        if (totalDistance >= 200)
        {
            playerScript.GoStop();
            distanceText.text = "거리초과"; // UI에 이동 멈춤 표시
            return; // 이동을 중지한 후 바로 리턴하여 더 이상 진행하지 않음
        }

        // 마우스 클릭 시 플레이어 이동 시작
        if (Input.GetMouseButton(0))
        {
            playerScript.Go(); // 이동 시작
        }
        else
        {
            playerScript.GoStop(); // 이동 중지
        }
    }
}
