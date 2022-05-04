using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHeal : MonoBehaviour
{
    public int collisonHeal = 50;
    public string collisionHealTag; //какому объекту восстановить здоровье

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == collisionHealTag)
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.HealthRecovery(collisonHeal); //восстановление здоровья подбираемым бонусом
        }

        if (gameObject.CompareTag("Bonus") == true) //уничтожение бонуса к здоровью после столкновения с ним
        {
            Destroy(gameObject);
        }
    }
}
