using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;   // 적의 최대 체력
    private int currentHealth;    // 적의 현재 체력

    void Start()
    {
        // 적의 체력을 최대 체력으로 초기화
        currentHealth = maxHealth;
    }

    // 체력을 감소시키는 함수
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // 데미지를 받은 만큼 체력 감소
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 체력을 0 이상으로 유지

        Debug.Log("Enemy Health: " + currentHealth);

        // 체력이 0이 되었을 때 사망 처리
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 적이 사망할 때 호출되는 함수
    private void Die()
    {
        Debug.Log("Boss Died!");
        // 사망 처리 로직 (예: 파괴, 애니메이션, 아이템 드랍 등)
        Destroy(gameObject); // 적 오브젝트 파괴
    }
}