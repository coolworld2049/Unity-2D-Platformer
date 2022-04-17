using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHeal : MonoBehaviour
{
    public int collisonHeal = 50;
    public string collisionHealTag; //������ ������� ������������ ��������

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == collisionHealTag)
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.HealthRecovery(collisonHeal); //�������������� �������� ����������� �������
        }

        if (gameObject.CompareTag("Bonus") == true) //����������� ������ � �������� ����� ������������ � ���
        {
            Destroy(gameObject);
        }
    }
}
