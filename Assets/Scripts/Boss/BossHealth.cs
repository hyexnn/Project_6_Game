using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;   // ���� �ִ� ü��
    private int currentHealth;    // ���� ���� ü��

    void Start()
    {
        // ���� ü���� �ִ� ü������ �ʱ�ȭ
        currentHealth = maxHealth;
    }

    // ü���� ���ҽ�Ű�� �Լ�
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // �������� ���� ��ŭ ü�� ����
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ü���� 0 �̻����� ����

        Debug.Log("Enemy Health: " + currentHealth);

        // ü���� 0�� �Ǿ��� �� ��� ó��
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ���� ����� �� ȣ��Ǵ� �Լ�
    private void Die()
    {
        Debug.Log("Boss Died!");
        // ��� ó�� ���� (��: �ı�, �ִϸ��̼�, ������ ��� ��)
        Destroy(gameObject); // �� ������Ʈ �ı�
    }
}