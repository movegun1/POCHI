using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial Status")]
    [SerializeField] private bool clearTheTutorial = false; // 인스펙터에서 상태 표시

    private const string TutorialKey = "ClearTheTutorial"; // PlayerPrefs 키

    private void Awake()
    {
        // ClearTheTutorial 상태 불러오기
        if (PlayerPrefs.HasKey(TutorialKey))
        {
            clearTheTutorial = PlayerPrefs.GetInt(TutorialKey) == 1;
        }
        else
        {
            clearTheTutorial = false;
            PlayerPrefs.SetInt(TutorialKey, 0); // 초기값 false
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        // 현재 씬이 인게임 씬(씬 1)일 때 튜토리얼 상태 확인
        if (SceneManager.GetActiveScene().buildIndex == 1 && !clearTheTutorial)
        {
            Debug.Log("튜토리얼로 이동 중...");
            SceneManager.LoadScene(2); // 튜토리얼 씬(씬 2)으로 전환
        }
        else
        {
            Debug.Log("튜토리얼 완료됨. 인게임 유지.");
        }
    }

    // 튜토리얼 완료 상태 저장 및 인게임 씬으로 이동
    public void MarkTutorialComplete()
    {
        clearTheTutorial = true;
        PlayerPrefs.SetInt(TutorialKey, 1); // 튜토리얼 완료 상태 저장
        PlayerPrefs.Save();

        Debug.Log("튜토리얼 완료! 인게임으로 돌아갑니다.");
        SceneManager.LoadScene(1); // 인게임 씬(씬 1)으로 이동
    }

    // 튜토리얼 상태 초기화 (디버그용)
    public void ResetTutorialState()
    {
        clearTheTutorial = false;
        PlayerPrefs.SetInt(TutorialKey, 0); // 초기값 false로 초기화
        PlayerPrefs.Save();

        Debug.Log("튜토리얼 상태 초기화됨.");
    }

    // 버튼 클릭 시 튜토리얼 씬(씬 2)로 이동
    public void GoToTutorialScene()
    {
        Debug.Log("튜토리얼 씬으로 이동합니다.");
        SceneManager.LoadScene(2); // 튜토리얼 씬(씬 2)로 이동
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // 인스펙터에서 ClearTheTutorial 상태를 PlayerPrefs와 동기화
        clearTheTutorial = PlayerPrefs.GetInt(TutorialKey, 0) == 1;
    }
#endif
}
