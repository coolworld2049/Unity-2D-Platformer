using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int collisonDamage = 20; //наносимый урон
    public string collisionDamageTag; //кому наносить урон

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == collisionDamageTag)
        {
            Health health = collision.collider.GetComponent<Health>(); //экземпляр класса
            health.TakeDamage(collisonDamage); //обращение к методу получения урона из класса Health
        }
    }
}
