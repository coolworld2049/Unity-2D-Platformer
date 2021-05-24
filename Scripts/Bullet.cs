using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed = 10; //�������� ����
    public bool fastBullet = false;
    public string toDamageTag; //���� ������� ����
    public int collisonDamage;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (fastBullet == true)
        {
            rb.velocity = transform.right * speed * 2; //�������� �������� ���� ���������
        }

        if (fastBullet == false)
        {
            rb.velocity = transform.right * speed; //�������� �������� ����
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != null)
        {
            if (collision.gameObject.tag == toDamageTag)
            {
                Health health = collision.gameObject.GetComponent<Health>(); //��������� ������
                health.TakeDamage(collisonDamage); //��������� � ������ ��������� ����� �� ������ Health
            }
            Destroy(gameObject); //���� ������������ ��� ��������������� ��� ��������� � ����� ��������
        }
    }
}
