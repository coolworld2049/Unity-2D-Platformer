using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed = 10; //скорость пули
    public bool fastBullet = false;
    public string toDamageTag; //кому нанести урон
    public int collisonDamage;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (fastBullet == true)
        {
            rb.velocity = transform.right * speed * 2; //движение префабов пуль ускоренно
        }

        if (fastBullet == false)
        {
            rb.velocity = transform.right * speed; //движение префабов пуль
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != null)
        {
            if (collision.gameObject.tag == toDamageTag)
            {
                Health health = collision.gameObject.GetComponent<Health>(); //экземпл€р класса
                health.TakeDamage(collisonDamage); //обращение к методу получени€ урона из класса Health
            }
            Destroy(gameObject); //пул€ уничтожаетс€ при соприкосновении его колайдера с любым объектом
        }
    }
}
