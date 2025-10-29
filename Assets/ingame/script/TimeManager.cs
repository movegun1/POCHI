using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timeDisplay; // UI에 시간을 표시할 텍스트

    [SerializeField, Tooltip("6시가 되면 true로 설정됩니다.")]
    private bool isNextDay = false; // 하루가 끝났는지 여부 (인스펙터에서 확인 가능)

    private float realTimeElapsed = 0f; // 경과된 현실 시간 (초 단위)
    private float dayDuration = 1260f; // 하루의 길이 (21분 = 1260초)
    private int startHour = 23; // 인게임 시작 시간 (11시, 24시간제)
    private int endHour = 6; // 인게임 종료 시간 (6시, 24시간제)

    void Update()
    {
        // 하루가 끝나지 않았다면 시간을 계산하고 업데이트
        if (!isNextDay)
        {
            realTimeElapsed += Time.deltaTime; // 현실 시간 증가
            UpdateInGameTime(); // 인게임 시간을 계산하고 UI 업데이트

            // 6시가 되었는지 확인
            if (realTimeElapsed >= dayDuration)
            {
                isNextDay = true; // 하루가 끝났음을 표시
            }
        }
    }

    // 인게임 시간을 계산하고 UI에 업데이트하는 함수
    private void UpdateInGameTime()
    {
        // 경과된 시간을 기반으로 하루 진행률 계산
        float timeProgress = realTimeElapsed / dayDuration;

        // 현재 시간을 계산 (24시간제 기준)
        float currentHour = (startHour + timeProgress * (endHour - startHour + 24)) % 24;

        // 시(hour)와 분(minute) 계산
        int hour = Mathf.FloorToInt(currentHour);
        int minute = Mathf.FloorToInt((currentHour - hour) * 60);

        // UI에 현재 시간 표시
        timeDisplay.text = $"{FormatTime(hour)}:{FormatTime(minute)}";
    }

    // 시간을 두 자리로 포맷팅하는 함수 (ex: 9 -> 09)
    private string FormatTime(int value)
    {
        return value.ToString("00");
    }
}
