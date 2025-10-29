using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeController : MonoBehaviour
{
    public GameObject aObject;        // A 오브젝트 참조
    public GameObject bObject;        // B 오브젝트 참조
    public Camera mainCamera;         // 메인 카메라 참조
    public float enlargedSize = 10f;  // A 오브젝트가 켜졌을 때의 카메라 크기
    private float originalSize;       // 초기 카메라 크기 저장
    public float transitionSpeed = 2f; // 크기 전환 속도

    private float targetSize;         // 목표 카메라 크기

    void Start()
    {
        // 메인 카메라의 초기 크기를 저장하고, 초기 목표 크기를 설정
        originalSize = mainCamera.orthographicSize;
        targetSize = originalSize;
    }

    void Update()
    {
        // A 오브젝트가 활성화되면 카메라 크기 확대
        if (aObject.activeSelf)
        {
            targetSize = enlargedSize;
        }
        // B 오브젝트가 활성화되면 카메라 크기를 원래대로 되돌림
        else if (bObject.activeSelf)
        {
            targetSize = originalSize;
        }

        // 현재 카메라 크기를 목표 크기로 점진적으로 변경
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, transitionSpeed * Time.deltaTime);
    }
}
