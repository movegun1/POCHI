using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inpntmove : MonoBehaviour
{
    public Transform player;  // 주인공의 Transform을 가져오기 위한 변수
    private Vector3 lastPlayerPosition;  // 주인공의 이전 위치
    Material mat;  // 배경에 사용되는 Material (재질) 변수
    float distance;  // 배경이 이동한 거리

    [Range(0f, 0.5f)]
    public float speed = 0.1f;  // 패럴랙스 효과의 속도 (0.0 ~ 0.5 범위에서 조정 가능)

    void Start()
    {
        // 이 오브젝트의 Renderer에서 사용하는 Material을 가져옴
        mat = GetComponent<Renderer>().material;

        // 주인공의 초기 위치 저장
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        // 주인공이 움직인 거리를 계산
        Vector3 playerDelta = player.position - lastPlayerPosition;

        // 주인공이 이동했을 경우에만 배경을 이동시킴
        if (playerDelta.x != 0)
        {
            // 주인공 이동 거리에 따라 배경을 이동
            distance += playerDelta.x * speed;
            mat.SetTextureOffset("_MainTex", Vector2.right * distance);
        }

        // 현재 주인공 위치를 저장하여 다음 프레임에서 비교
        lastPlayerPosition = player.position;
    }
}
