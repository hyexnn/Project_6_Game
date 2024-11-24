using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet_prefab;    // ������ ź���� ���� ������
    public float spawn_rate_min = 0.5f; // �ּ� ���� �ֱ�
    public float spawn_rate_max = 3f;   // �ִ� ���� �ֱ�

    public Transform target;   // �߻��� ��� (�÷��̾��� Transform)
    public float spawn_rate;   // ���� �ֱ�
    float time_after_spawn;    // �ֱ� ���� �������� ���� �ð�

    void Start()
    {
        // �ʱ�ȭ
        time_after_spawn = 0;
        spawn_rate = Random.Range(spawn_rate_min, spawn_rate_max);
    }

    void Update()
    {
        // time_after_spawn ����
        time_after_spawn += Time.deltaTime;

        if (time_after_spawn > spawn_rate)
        {
            // ������ �ð��� ����
            time_after_spawn = 0;

            if (target != null)
            {
                // target ���� ��� �� ȸ�� ����
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // ź�� ����
                GameObject bullet = Instantiate(bullet_prefab, transform.position, targetRotation);
            }

            // ���� ���� �ֱ� ���� ����
            spawn_rate = Random.Range(spawn_rate_min, spawn_rate_max);
        }
    }
}
