using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollwerCamera : MonoBehaviour
{
    public Transform player;            // 따라갈 플레이어 오브젝트
    public Vector3 offset = new Vector3(0, 5, -10); // 플레이어와 카메라 사이 거리
    public float followSpeed = 10f;     // 카메라가 플레이어를 따라가는 속도
    public float rotationSpeed = 100f;  // 마우스를 사용한 카메라 회전 속도

    private float yaw = 0f;             // 좌우 회전 각도
    private float pitch = 0f;           // 상하 회전 각도
    public float minPitch = -30f;       // 카메라의 상하 회전 각도 최소값
    public float maxPitch = 60f;        // 카메라의 상하 회전 각도 최대값

    void LateUpdate()
    {
        if (player == null) return; // 플레이어가 없는 경우 스크립트 중단

        // 마우스 입력을 통해 카메라 회전
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch); // 상하 각도 제한

        // 카메라 위치와 회전 계산
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetPosition = player.position + rotation * offset;

        // 카메라 위치와 회전을 부드럽게 보간
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.LookAt(player.position);
    }
}
