using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;       // �̵� �ӵ�
    public float rotationSpeed = 100f;  // ȸ�� �ӵ�
    public float maxVerticalAngle = 45f; // ���� ȸ�� �ִ� ���� ����

    private CharacterController controller;
    private float pitch = 0f;           // ���� ȸ�� ����
    private float yaw = 0f;             // �¿� ȸ�� ����

    public GameObject bulletPrefab;      // �߻��� źȯ ������
    public Transform firePoint;          // źȯ�� �߻�� ��ġ
    public float bulletSpeed = 20f;      // źȯ�� �ӵ�
    public float fireRate = 0.5f;        // �߻� �ӵ� (�ʴ� �߻� ����)

    private float nextFireTime = 0.5f;     // ���� �߻簡 ������ �ð�

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ���� ȭ�� �߾ӿ� ����
    }

    void Update()
    {
        Move();

        // ���콺 ���� ��ư Ŭ�� �� ����
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // ���� �߻� �ð� ����
        }
    }

    void Move()
    {
        // �÷��̾� �̵�
        float horizontal = Input.GetAxis("Horizontal"); // A, D �Ǵ� �¿� ȭ��ǥ Ű
        float vertical = Input.GetAxis("Vertical");     // W, S �Ǵ� ���� ȭ��ǥ Ű
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // ���콺 �Է¿� ���� ȸ��
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // yaw: �¿� ȸ��
        yaw += mouseX;
        // pitch: ���� ȸ��
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -maxVerticalAngle, maxVerticalAngle); // ���� ���� ����

        // �÷��̾� ȸ�� ����
        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void Shoot()
    {
        // źȯ ���� �� �ʱ�ȭ
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.velocity = firePoint.forward * bulletSpeed; // źȯ�� �ӵ� �ο�
        }
    }
}