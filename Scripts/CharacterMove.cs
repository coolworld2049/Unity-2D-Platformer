using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMove : MonoBehaviour
{
    public float maxSpeed;
    public float jump;

    private bool isFacingRight = true; //проверка в какую сторону направлен игрок
    private bool isGrounded = true; //проверка находится ли игрок на платформе (Tilemap)
    public float countCrystall; //счетчик собираемых объектов
    private int keyPressCount = 0; //счетчик нажатий клавиши
    private bool shiftPressed = true; //проверка нажатия Shift
    private bool accelerateMove = true; // ускорение игрока

    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private CircleCollider2D ccl;

    public TMP_Text countCrystallText; // текстовое поле для вывода счета кристаллов
    public TMP_Text accelerateText; 
    public GameObject[] flipAxis;

    void Start()
    {
        //инициализация компнентов UnityEngine принадлежащих объекту игрока
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        ccl = GetComponent<CircleCollider2D>();
        ccl.enabled = false;
    }

    //обработка методов на каждом кадре
    void Update()
    {
        HorizontalMove();
        Jump();
        PlayerBall();
        Acceleration();
    }

    //обработка через фиксированное количество кадров
    void FixedUpdate()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(horizontalMove)); //в компоненте анимаций изменяем значение параметра Speed на значение оси Х (анимация ходьбы)
    }


    //горизональное движение игрока
    public void HorizontalMove()
    {
        //используем Input.GetAxis для оси Х. метод возвращает значение оси в пределах от -1 до 1.
        //-1 возвращается при нажатии клавиши А
        //1 возвращается при нажатии клавиши D
        float move = Input.GetAxis("Horizontal");

        //обращаемся к компоненту персонажа RigidBody2D и задаем ему скорость по оси Х, 
        rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

        //если нажали клавишу для перемещения вправо, а персонаж направлен влево
        if (move > 0 && !isFacingRight) 
        {
            Flip(); //отражаем персонажа вправо
        }
        else if (move < 0 && isFacingRight) 
        {
            Flip(); //отражаем персонажа влево
        }        
    }

    //смена направления движения персонажа и его зеркальное отражение
    public void Flip()
    {
        FlipObjectAxis(); // смена направления оси X при повороте налево

        isFacingRight = !isFacingRight; //меняем направление движения персонажа
        Vector3 theScale = transform.localScale; //получаем размеры персонажа
        theScale.x *= -1; //зеркально отражаем персонажа по оси Х
        transform.localScale = theScale; //задаем новый размер персонажа, равный старому, но зеркально отраженный
    }

    //смена напаравления оси X объекта Fire point
    public void FlipObjectAxis()
    {
        int i = 0;
        while (i < flipAxis.Length)
        {
            flipAxis[i].transform.Rotate(0f, 180f, 0f);
            i++;
        } 
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) //прыжок если нажат пробел и игрок находится на платформе
        {
            rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded) //двойной прыжок в воздухе при двойном нажатии пробела
        {
            keyPressCount++;

            if (keyPressCount == 2)
            {
                rb.AddForce(transform.up * jump * 0.6f, ForceMode2D.Impulse);
                keyPressCount = 0;
            }
        }

        bool oncePress = false;
        oncePress = Input.GetKey(KeyCode.Space);
        anim.SetBool("SpaceKeyPressed", oncePress); //анимация прыжка (включается при удержании пробела)
        oncePress = false;
    }

    //проверка на соприкосновение игрока с платформой
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    //счетчик кристаллов
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collected Object")
        {
            Destroy(collision.gameObject); // уничтожение объекта если игрок столкнулся с ним
            countCrystall++;
            countCrystallText.text = countCrystall.ToString();
        }
    }

    //анимация шара
    public void PlayerBall()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            if(shiftPressed)
            {
                bc.enabled = false; //выключается исходный коллайдер (BoxCollider2D)
                ccl.enabled = true; //включается коллайдер круга (CircleCollider2D)
                anim.SetBool("TapKeyShift", true); //анимация шара

                maxSpeed += 2f;
                jump += 4f;
                accelerateText.text = "Ball on [Shift]\n\n +2 speed\n +4 jump";

                shiftPressed = false;
            }

            else
            {
                bc.enabled = true; //выключается исходный коллайдер (BoxCollider2D)
                ccl.enabled = false; //включается коллайдер круга (CircleCollider2D)
                anim.SetBool("TapKeyShift", false); //анимация шара

                maxSpeed -= 2f;
                jump -= 4f;
                accelerateText.text = "Ball off [Shift]";

                shiftPressed = true;
            }
        }
    }

    // ускорение игрока
    public void Acceleration()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            if(accelerateMove)
            {
                maxSpeed += 5f;
                accelerateText.text = "Acceleration on [V]\n\n +5 speed";
                accelerateMove = false;
            }
            else
            {
                maxSpeed -= 5f;
                accelerateText.text = "Acceleration off [V]";
                accelerateMove = true;
            }
            
        }    
    }
}