using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;       // 이동 속도
    public float rotationSpeed = 100f;  // 회전 속도
    public float maxVerticalAngle = 45f; // 상하 회전 최대 각도 제한

    private CharacterController controller;
    private float pitch = 0f;           // 상하 회전 각도
    private float yaw = 0f;             // 좌우 회전 각도

    public GameObject bulletPrefab;      // 발사할 탄환 프리팹
    public Transform firePoint;          // 탄환이 발사될 위치
    public float bulletSpeed = 20f;      // 탄환의 속도
    public float fireRate = 0.5f;        // 발사 속도 (초당 발사 간격)

    private float nextFireTime = 0.5f;     // 다음 발사가 가능한 시간

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서를 화면 중앙에 고정
    }

    void Update()
    {
        Move();

        // 마우스 왼쪽 버튼 클릭 시 공격
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // 다음 발사 시간 설정
        }
    }

    void Move()
    {
        // 플레이어 이동
        float horizontal = Input.GetAxis("Horizontal"); // A, D 또는 좌우 화살표 키
        float vertical = Input.GetAxis("Vertical");     // W, S 또는 상하 화살표 키
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // 마우스 입력에 따른 회전
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // yaw: 좌우 회전
        yaw += mouseX;
        // pitch: 상하 회전
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -maxVerticalAngle, maxVerticalAngle); // 상하 각도 제한

        // 플레이어 회전 적용
        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void Shoot()
    {
        // 탄환 생성 및 초기화
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.velocity = firePoint.forward * bulletSpeed; // 탄환에 속도 부여
        }
    }
}