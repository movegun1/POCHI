using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // 중복된 target 제거
    public float smoothSpeed = 0.3f;
    public Vector2 offset;
    public float limitMinX, limitMaxX;
    public float zoomInSize = 3f;
    public float zoomOutSpeed = 0.5f;
    public bool isTargetShooting = false;

    private float originalOrthographicSize;

    private void Start()
    {
        originalOrthographicSize = Camera.main.orthographicSize;
    }

    private void LateUpdate()
    {
        if (isTargetShooting)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomInSize, Time.deltaTime * zoomOutSpeed);
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 targetPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
            float clampedX = Mathf.Clamp(targetPosition.x, limitMinX, limitMaxX);
            Vector3 clampedPosition = new Vector3(clampedX, targetPosition.y, targetPosition.z);
            transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed * Time.deltaTime);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalOrthographicSize, Time.deltaTime * zoomOutSpeed);
        }
    }

    public void OnTargetShoot()
    {
        isTargetShooting = true;
    }

    public void OnTargetShootEnd()
    {
        isTargetShooting = false;
    }
}
