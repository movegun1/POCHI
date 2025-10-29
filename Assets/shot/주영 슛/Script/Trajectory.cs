using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] int dotsNumber = 30; // 궤적 점의 수
    [SerializeField] GameObject dotsParent; // 궤적 점을 담는 부모 오브젝트
    [SerializeField] GameObject dotPrefab; // 궤적 점 프리팹
    [SerializeField] float dotSpacing = 0.1f; // 궤적 점 간의 간격
    [SerializeField][Range(0.01f, 0.3f)] float dotMinScale = 0.1f; // 궤적 점의 최소 크기
    [SerializeField][Range(0.3f, 1f)] float dotMaxScale = 1f; // 궤적 점의 최대 크기

    Transform[] dotsList; // 궤적 점들의 Transform 배열

    Vector2 pos; // 궤적 점의 위치
    float timeStamp; // 궤적 점의 시간 스탬프

    //--------------------------------
    void Start()
    {
        Hide(); // 시작할 때 궤적 숨기기
        PrepareDots(); // 궤적 점 준비
    }

    // 궤적 점 준비
    void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

        float scale = dotMaxScale;
        float scaleFactor = (dotMaxScale - dotMinScale) / dotsNumber;

        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, dotsParent.transform).transform;
            dotsList[i].localScale = Vector3.one * scale;
            if (scale > dotMinScale)
                scale -= scaleFactor;
        }
    }

    // 궤적 점 업데이트
    public void UpdateDots(Vector3 ballPos, Vector2 forceApplied)
    {
        timeStamp = dotSpacing;

        for (int i = 0; i < dotsNumber; i++)
        {
            pos = (Vector2)ballPos + forceApplied * timeStamp + 0.5f * Physics2D.gravity * timeStamp * timeStamp;
            dotsList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }

    // 궤적 점 보여주기
    public void Show()
    {
        dotsParent.SetActive(true);
    }

    // 궤적 점 숨기기
    public void Hide()
    {
        dotsParent.SetActive(false);
    }
}
