using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int collisonDamage = 20; //��������� ����
    public string collisionDamageTag; //���� �������� ����

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == collisionDamageTag)
        {
            Health health = collision.collider.GetComponent<Health>(); //��������� ������
            health.TakeDamage(collisonDamage); //��������� � ������ ��������� ����� �� ������ Health
        }
    }
}
