using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletSpeed = 10; 
    public string toDamageTag;
    public int collisonDamage;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.velocity = transform.right * bulletSpeed; //движение префабов пуль
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(toDamageTag))
        {
            Health health = collision.gameObject.GetComponent<Health>(); //экземпляр класса
            health.TakeDamage(collisonDamage); //обращение к методу получения урона из класса Health
            Destroy(gameObject); //пуля уничтожается при соприкосновении его колайдера с любым объектом
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); //пуля уничтожается при соприкосновении его колайдера с любым объектом
        }
        
    }
}
