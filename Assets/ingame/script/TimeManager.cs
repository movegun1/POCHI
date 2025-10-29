using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timeDisplay; // UI�� �ð��� ǥ���� �ؽ�Ʈ

    [SerializeField, Tooltip("6�ð� �Ǹ� true�� �����˴ϴ�.")]
    private bool isNextDay = false; // �Ϸ簡 �������� ���� (�ν����Ϳ��� Ȯ�� ����)

    private float realTimeElapsed = 0f; // ����� ���� �ð� (�� ����)
    private float dayDuration = 1260f; // �Ϸ��� ���� (21�� = 1260��)
    private int startHour = 23; // �ΰ��� ���� �ð� (11��, 24�ð���)
    private int endHour = 6; // �ΰ��� ���� �ð� (6��, 24�ð���)

    void Update()
    {
        // �Ϸ簡 ������ �ʾҴٸ� �ð��� ����ϰ� ������Ʈ
        if (!isNextDay)
        {
            realTimeElapsed += Time.deltaTime; // ���� �ð� ����
            UpdateInGameTime(); // �ΰ��� �ð��� ����ϰ� UI ������Ʈ

            // 6�ð� �Ǿ����� Ȯ��
            if (realTimeElapsed >= dayDuration)
            {
                isNextDay = true; // �Ϸ簡 �������� ǥ��
            }
        }
    }

    // �ΰ��� �ð��� ����ϰ� UI�� ������Ʈ�ϴ� �Լ�
    private void UpdateInGameTime()
    {
        // ����� �ð��� ������� �Ϸ� ����� ���
        float timeProgress = realTimeElapsed / dayDuration;

        // ���� �ð��� ��� (24�ð��� ����)
        float currentHour = (startHour + timeProgress * (endHour - startHour + 24)) % 24;

        // ��(hour)�� ��(minute) ���
        int hour = Mathf.FloorToInt(currentHour);
        int minute = Mathf.FloorToInt((currentHour - hour) * 60);

        // UI�� ���� �ð� ǥ��
        timeDisplay.text = $"{FormatTime(hour)}:{FormatTime(minute)}";
    }

    // �ð��� �� �ڸ��� �������ϴ� �Լ� (ex: 9 -> 09)
    private string FormatTime(int value)
    {
        return value.ToString("00");
    }
}
