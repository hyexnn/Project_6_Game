using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;    // 최대 체력
    private int currentHealth;     // 현재 체력

    void Start()
    {
        // 체력을 최대 체력으로 초기화
        currentHealth = maxHealth;
    }

    // 체력 감소 함수
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 체력 값 제한
        Debug.Log("Player Health: " + currentHealth);

        // 체력이 0이 되면 사망 처리
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 체력 회복 함수
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 체력 값 제한
        Debug.Log("Player Health: " + currentHealth);
    }

    // 사망 처리 함수
    private void Die()
    {
        Debug.Log("Player Died!");
        // 사망 처리 로직 (예: 재시작, 게임 오버 화면, 리스폰 등)
    }
}