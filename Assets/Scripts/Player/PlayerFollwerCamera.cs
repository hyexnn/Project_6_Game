using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollwerCamera : MonoBehaviour
{
    public Transform player;            // ���� �÷��̾� ������Ʈ
    public Vector3 offset = new Vector3(0, 5, -10); // �÷��̾�� ī�޶� ���� �Ÿ�
    public float followSpeed = 10f;     // ī�޶� �÷��̾ ���󰡴� �ӵ�
    public float rotationSpeed = 100f;  // ���콺�� ����� ī�޶� ȸ�� �ӵ�

    private float yaw = 0f;             // �¿� ȸ�� ����
    private float pitch = 0f;           // ���� ȸ�� ����
    public float minPitch = -30f;       // ī�޶��� ���� ȸ�� ���� �ּҰ�
    public float maxPitch = 60f;        // ī�޶��� ���� ȸ�� ���� �ִ밪

    void LateUpdate()
    {
        if (player == null) return; // �÷��̾ ���� ��� ��ũ��Ʈ �ߴ�

        // ���콺 �Է��� ���� ī�޶� ȸ��
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch); // ���� ���� ����

        // ī�޶� ��ġ�� ȸ�� ���
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetPosition = player.position + rotation * offset;

        // ī�޶� ��ġ�� ȸ���� �ε巴�� ����
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.LookAt(player.position);
    }
}
