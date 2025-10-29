using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] int dotsNumber = 30; // ���� ���� ��
    [SerializeField] GameObject dotsParent; // ���� ���� ��� �θ� ������Ʈ
    [SerializeField] GameObject dotPrefab; // ���� �� ������
    [SerializeField] float dotSpacing = 0.1f; // ���� �� ���� ����
    [SerializeField][Range(0.01f, 0.3f)] float dotMinScale = 0.1f; // ���� ���� �ּ� ũ��
    [SerializeField][Range(0.3f, 1f)] float dotMaxScale = 1f; // ���� ���� �ִ� ũ��

    Transform[] dotsList; // ���� ������ Transform �迭

    Vector2 pos; // ���� ���� ��ġ
    float timeStamp; // ���� ���� �ð� ������

    //--------------------------------
    void Start()
    {
        Hide(); // ������ �� ���� �����
        PrepareDots(); // ���� �� �غ�
    }

    // ���� �� �غ�
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

    // ���� �� ������Ʈ
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

    // ���� �� �����ֱ�
    public void Show()
    {
        dotsParent.SetActive(true);
    }

    // ���� �� �����
    public void Hide()
    {
        dotsParent.SetActive(false);
    }
}
