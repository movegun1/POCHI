using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial Status")]
    [SerializeField] private bool clearTheTutorial = false; // �ν����Ϳ��� ���� ǥ��

    private const string TutorialKey = "ClearTheTutorial"; // PlayerPrefs Ű

    private void Awake()
    {
        // ClearTheTutorial ���� �ҷ�����
        if (PlayerPrefs.HasKey(TutorialKey))
        {
            clearTheTutorial = PlayerPrefs.GetInt(TutorialKey) == 1;
        }
        else
        {
            clearTheTutorial = false;
            PlayerPrefs.SetInt(TutorialKey, 0); // �ʱⰪ false
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        // ���� ���� �ΰ��� ��(�� 1)�� �� Ʃ�丮�� ���� Ȯ��
        if (SceneManager.GetActiveScene().buildIndex == 1 && !clearTheTutorial)
        {
            Debug.Log("Ʃ�丮��� �̵� ��...");
            SceneManager.LoadScene(2); // Ʃ�丮�� ��(�� 2)���� ��ȯ
        }
        else
        {
            Debug.Log("Ʃ�丮�� �Ϸ��. �ΰ��� ����.");
        }
    }

    // Ʃ�丮�� �Ϸ� ���� ���� �� �ΰ��� ������ �̵�
    public void MarkTutorialComplete()
    {
        clearTheTutorial = true;
        PlayerPrefs.SetInt(TutorialKey, 1); // Ʃ�丮�� �Ϸ� ���� ����
        PlayerPrefs.Save();

        Debug.Log("Ʃ�丮�� �Ϸ�! �ΰ������� ���ư��ϴ�.");
        SceneManager.LoadScene(1); // �ΰ��� ��(�� 1)���� �̵�
    }

    // Ʃ�丮�� ���� �ʱ�ȭ (����׿�)
    public void ResetTutorialState()
    {
        clearTheTutorial = false;
        PlayerPrefs.SetInt(TutorialKey, 0); // �ʱⰪ false�� �ʱ�ȭ
        PlayerPrefs.Save();

        Debug.Log("Ʃ�丮�� ���� �ʱ�ȭ��.");
    }

    // ��ư Ŭ�� �� Ʃ�丮�� ��(�� 2)�� �̵�
    public void GoToTutorialScene()
    {
        Debug.Log("Ʃ�丮�� ������ �̵��մϴ�.");
        SceneManager.LoadScene(2); // Ʃ�丮�� ��(�� 2)�� �̵�
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // �ν����Ϳ��� ClearTheTutorial ���¸� PlayerPrefs�� ����ȭ
        clearTheTutorial = PlayerPrefs.GetInt(TutorialKey, 0) == 1;
    }
#endif
}
