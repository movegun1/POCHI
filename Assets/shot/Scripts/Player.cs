using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool MoveForward = true; // 앞으로 이동 가능 여부
    private bool MoveBack = true; // 뒤로 이동 가능 여부
    private bool isDragging = false; // 드래그 중인지 여부
    Animator anim;

    // 이동 가능한 범위를 기즈모로 시각화하기 위한 변수 (UI처럼 고정)
    public Rect moveArea = new Rect(-5f, -5f, 10f, 10f); // x, y, width, height
    private Vector3 initialPosition; // 플레이어의 초기 위치 저장

    private void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = new Vector3(-5.5f, -2.2f, 0);
        initialPosition = transform.position; // 시작할 때 플레이어 위치 저장
    }

    public void Move()
    {
        if (!MoveForward && !MoveBack || isDragging) return; // 이동 가능 여부를 체크하고 드래그 중일 경우 이동 불가

        float speed = Time.deltaTime * 5f;

        if (IsMouseInRange() && IsMouseClicked()) // 마우스가 범위 안에 있을 때만 이동
        {
            anim.SetBool("Isrunning", true);
            transform.Translate(Vector2.right * speed);

            // 플레이어가 앞으로 갈 때 moveArea도 함께 이동 (12만큼 앞에 위치)
            moveArea.x = transform.position.x - moveArea.width / 2 + 14f; // moveArea가 플레이어보다 20만큼 앞에 위치하도록 조정
        }
        else
        {
            anim.SetBool("Isrunning", false);
        }
    }

    // 드래그 상태를 설정하는 메서드
    public void SetDraggingState(bool dragging)
    {
        isDragging = dragging; // 드래그 중인지 여부 설정
    }

    // 마우스 클릭 확인 함수 추가
    private bool IsMouseClicked()
    {
        return Input.GetMouseButton(0); // 마우스 왼쪽 버튼이 눌렸는지 확인
    }

    // 마우스가 지정된 범위 안에 있는지 확인하는 함수
    private bool IsMouseInRange()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return moveArea.Contains(mousePosition);
    }

    void ChangeR()
    {
        transform.position = new Vector3(-5f, -2.2f, 0);
    }

    void ChangeL()
    {
        transform.position = new Vector3(45f, -2.2f, 0);
    }

    void Update()
    {
        if (transform.position.x < -10)
        {
            ChangeL();
        }
        else if (transform.position.x > 80)
        {
            ChangeR();
        }
        else if (MoveForward && MoveBack)
        {
            Move();
        }
    }

    public void GoStop()
    {
        anim.SetBool("Isrunning", false);
        MoveForward = false; // 앞으로 이동 불가 설정
        SetDraggingState(true); // 드래그 중임을 설정
    }

    public void BackStop()
    {
        anim.SetBool("Isrunning", false);
        MoveBack = false; // 뒤로 이동 불가 설정
    }

    public void Stop()
    {
        anim.SetBool("Isrunning", false);
        MoveForward = false; // 앞으로 이동 불가 설정
        MoveBack = false; // 뒤로 이동 불가 설정
        SetDraggingState(true); // 드래그 중임을 설정
    }

    public void Go()
    {
        MoveForward = true; // 앞으로 이동 가능 해제
        MoveBack = true; // 뒤로 이동 가능 해제
        SetDraggingState(false); // 드래그 중 아님을 설정
    }

    // 기즈모를 사용하여 이동 가능한 범위를 시각화
    void OnDrawGizmos()
    {
        // 초록색 사각형으로 이동 가능한 범위 그리기
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(moveArea.center(), moveArea.size());
    }

    // 플레이어의 이동 범위를 UI처럼 고정
    void LateUpdate()
    {
        // 플레이어의 x축 이동만 허용, y축 위치는 고정 (UI처럼 작동하게)
        Vector3 fixedPosition = transform.position;
        fixedPosition.y = initialPosition.y; // 초기 y 위치를 고정
        transform.position = fixedPosition;
    }
}

public static class RectExtensions
{
    public static Vector2 center(this Rect rect)
    {
        return new Vector2(rect.x + rect.width / 2, rect.y + rect.height / 2);
    }

    public static Vector2 size(this Rect rect)
    {
        return new Vector2(rect.width, rect.height);
    }
}
