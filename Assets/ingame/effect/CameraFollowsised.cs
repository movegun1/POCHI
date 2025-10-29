using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsised : MonoBehaviour
{
    public Transform player;  // 따라갈 PLAYER 오브젝트의 Transform
    public Vector3 offset = new Vector3(0f, 2f, -10f);  // 카메라의 상대적 위치 (오프셋 값)

    void LateUpdate()
    {
        // PLAYER의 위치 + 오프셋 위치로 카메라 위치를 설정
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
