using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet_prefab;    // 생성할 탄알의 원본 프리팹
    public float spawn_rate_min = 0.5f; // 최소 생성 주기
    public float spawn_rate_max = 3f;   // 최대 생성 주기

    public Transform target;   // 발사할 대상 (플레이어의 Transform)
    public float spawn_rate;   // 생성 주기
    float time_after_spawn;    // 최근 생성 시점에서 지난 시간

    void Start()
    {
        // 초기화
        time_after_spawn = 0;
        spawn_rate = Random.Range(spawn_rate_min, spawn_rate_max);
    }

    void Update()
    {
        // time_after_spawn 갱신
        time_after_spawn += Time.deltaTime;

        if (time_after_spawn > spawn_rate)
        {
            // 누적된 시간을 리셋
            time_after_spawn = 0;

            if (target != null)
            {
                // target 방향 계산 및 회전 설정
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // 탄알 생성
                GameObject bullet = Instantiate(bullet_prefab, transform.position, targetRotation);
            }

            // 다음 생성 주기 랜덤 설정
            spawn_rate = Random.Range(spawn_rate_min, spawn_rate_max);
        }
    }
}
