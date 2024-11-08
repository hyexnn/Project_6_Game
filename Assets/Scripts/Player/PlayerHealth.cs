using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;    // �ִ� ü��
    private int currentHealth;     // ���� ü��

    void Start()
    {
        // ü���� �ִ� ü������ �ʱ�ȭ
        currentHealth = maxHealth;
    }

    // ü�� ���� �Լ�
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ü�� �� ����
        Debug.Log("Player Health: " + currentHealth);

        // ü���� 0�� �Ǹ� ��� ó��
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ü�� ȸ�� �Լ�
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ü�� �� ����
        Debug.Log("Player Health: " + currentHealth);
    }

    // ��� ó�� �Լ�
    private void Die()
    {
        Debug.Log("Player Died!");
        // ��� ó�� ���� (��: �����, ���� ���� ȭ��, ������ ��)
    }
}