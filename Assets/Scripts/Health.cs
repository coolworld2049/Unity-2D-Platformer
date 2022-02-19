using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float HealthInitial = 100f;
    public float healthMax = 100f;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void HealthRecovery(int bonusHealth) //bonusHealth �������������� � ������� CollisionHeal
    {
        if (gameObject.tag == "Player")
        {
            if (HealthInitial < 100)
            {
                HealthInitial += bonusHealth; // ������ ������ � ���������� ��������
            }

            if (HealthInitial >= 100)
            {
                HealthInitial = healthMax; //������������ �������� ��������� 100
            }
            Debug.Log(HealthInitial);
        }  
    }

    public void TakeDamage(int damage) //damage �������������� � ������� CollisionDamage
    {
        HealthInitial -= damage;

        if (gameObject.tag == "Player")
        {
            rb.AddForce(transform.up * 10, ForceMode2D.Impulse); //������ �� ����� ��� ������������ � ��� � ��������� �����
            rb.velocity = new Vector2(6, rb.velocity.y);

            if (HealthInitial <= 0)
            {
                animator.SetFloat("playerHealth", 0); //�������� ������ ������ � ��� ����������� 
                Destroy(gameObject, 2f);
            }
            Debug.Log(HealthInitial);
        }

        if (gameObject.tag == "Enemy")
        {
            rb.AddForce(transform.up * 5, ForceMode2D.Impulse);
            rb.velocity = new Vector2(6, rb.velocity.y);

            if (HealthInitial <= 0)
            {
                animator.SetFloat("enemyHealth", 0); //�������� ������ �����
                animator.SetFloat("enemy_2Health", 0);
                animator.SetFloat("enemy_3Health", 0);
                Destroy(gameObject, 0.7f);
            }
            Debug.Log(HealthInitial);
        }
    }
}
