using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPositionDisplay : MonoBehaviour
{
    public Transform playerTransform;  // 주인공 오브젝트의 Transform을 연결
    public Text positionText;          // UI 텍스트 컴포넌트를 연결

    void Update()
    {
        // 플레이어 x 좌표를 정수로 변환하여 표시
        int playerXPosition = Mathf.RoundToInt(playerTransform.position.x);
        positionText.text = playerXPosition + "M";
    }
}