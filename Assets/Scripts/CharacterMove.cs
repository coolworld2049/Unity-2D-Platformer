using System.Globalization;
using UnityEngine;
using TMPro;

public class CharacterMove : Weapon
{
    private RaycastHit2D raycastHit2D;
    public float rayLenght;
    public float maxSpeed;
    public float jump;
    public float countCrystall;
    public TMP_Text countCrystallText;
    public TMP_Text bulletsCount;
    public TMP_Text ammoSuplly;


    public TMP_Text accelerateText; 
    public GameObject[] flipAxis;
    public Joystick joystick;
    public RectTransform handleJoystick;
    
    private bool isFacingRight = true;
    private bool isGrounded = true;
    private int keyPressCount; 
    private bool shiftPressed = true; 
    private bool accelerateMove = true;
    
    private Animator anim;
    private Rigidbody2D rb;
    private EdgeCollider2D ec;
    private CircleCollider2D ccl;

    
    private static readonly int SpaceKeyPressed = Animator.StringToHash("SpaceKeyPressed");
    private static readonly int TapKeyShift = Animator.StringToHash("TapKeyShift");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private bool canJump = true;
    public bool IsFacingRight
    {
        get => isFacingRight;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ec = GetComponent<EdgeCollider2D>();
        ccl = GetComponent<CircleCollider2D>();
        ccl.enabled = false;
        ammoSuplly.text = bulletPrefabCount.ToString();
    }

    void Update()
    {
#if UNITY_ANDROID
        Android_HorizontalMove();
        Android_Jump();
        Android_Shoot_Raycast();
        TurnIntoBall_Android();
        Acceleration_Android();
#endif
        
#if UNITY_STANDALONE_WIN
        HorizontalMove();
        Jump();
        Shoot();
        TurnIntoBall();
        Acceleration();
    #endif
        
        bulletsCount.text = bulletPrefabCount.ToString();
    }

    void FixedUpdate()
    {
#if UNITY_STANDALONE_WIN

        float horizontalMove = Input.GetAxis("Horizontal");
        anim.SetFloat(Speed, Mathf.Abs(horizontalMove)); 
    #endif
#if UNITY_ANDROID
        anim.SetFloat(Speed, Mathf.Abs(joystick.Horizontal));
    #endif
    }
    

    private void HorizontalMove()
    {
        //используем Input.GetAxis для оси Х. метод возвращает значение оси в пределах от -1 до 1.
        //-1 возвращается при нажатии клавиши А
        //1 возвращается при нажатии клавиши D
        float move = Input.GetAxis("Horizontal");

        //обращаемся к компоненту персонажа RigidBody2D и задаем ему скорость по оси Х, 
        rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

        if (move > 0 && !isFacingRight) 
        {
            Flip(); 
        }
        else if (move < 0 && isFacingRight)
        {
            Flip();
        }        
    }
    
    private void Android_HorizontalMove()
    {
        float horizontalMove = joystick.Horizontal * maxSpeed;
        /*if (joystick.Horizontal >= .2f)
        {
            horizontalMove = maxSpeed;
        }
        else if (joystick.Horizontal <= -.2f)
        {
            horizontalMove = -maxSpeed;
        }
        else
        {
            horizontalMove = 0f;
        }*/
        
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);

        if (horizontalMove > 0 && !isFacingRight) 
        {
            Flip(); 
        }
        else if (horizontalMove < 0 && isFacingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        FlipObjectAxis(); // смена направления оси X при повороте налево

        isFacingRight = !isFacingRight; //меняем направление движения персонажа
        Vector3 theScale = transform.localScale; //получаем размеры персонажа
        theScale.x *= -1; //зеркально отражаем персонажа по оси Х
        transform.localScale = theScale; //задаем новый размер персонажа, равный старому, но зеркально отраженный
    }

    //смена напаравления оси X объекта Fire point
    private void FlipObjectAxis()
    {
        int i = 0;
        while (i < flipAxis.Length)
        {
            flipAxis[i].transform.Rotate(0f, 180f, 0f);
            i++;
        } 
    }

    private void Jump()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            anim.SetBool(SpaceKeyPressed, false);
            anim.speed = 1;
        }
        else
        {
            anim.SetBool(SpaceKeyPressed, true);
            anim.speed = 0.8f;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            keyPressCount = 1;
            rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
            anim.SetBool(SpaceKeyPressed, Input.GetKeyDown(KeyCode.Space));
            anim.speed = 0.8f;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && keyPressCount == 1)
        {
            keyPressCount = 2;
            float dJump = jump * 0.5f;
            rb.AddForce(transform.up * dJump , ForceMode2D.Impulse);
        }
    }
    
    private void Android_Jump()
    {   
        /*RaycastHit2D raycastHit2D = Physics2D.Raycast(
            new Vector2(transform.position.x, (transform.position.y - 0.9f)), 
            transform.localScale * Vector2.right * rayLenght,
            rayLenght);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y -0.9f, transform.position.z), Vector2.right * rayLenght, Color.green);
        
        if (raycastHit2D.collider is not null)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }*/
        if ((handleJoystick.localPosition.x != 0) && (handleJoystick.transform.position.y != 0))
        {
            CanShoot = false;
        }
        else
        {
            CanShoot = true;
        }
        
        if (isGrounded)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
        
        if (joystick.Vertical >= .1f && canJump)
        {
            keyPressCount = 1;
            rb.velocity = new Vector2(rb.velocity.x, jump);
            anim.speed = 0.8f;
            anim.SetBool(SpaceKeyPressed, true);
        }
        else
        {
            anim.SetBool(SpaceKeyPressed, false);
            anim.speed = 1;
        }
    }

    
    //проверка на соприкосновение игрока с платформой
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    //счетчик кристаллов
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collected Object"))
        {
            Destroy(collision.gameObject); // уничтожение объекта если игрок столкнулся с ним
            countCrystall++;
            countCrystallText.text = countCrystall.ToString(CultureInfo.CurrentCulture);
        }
    }

    //анимация шара
    private void TurnIntoBall()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            if(shiftPressed)
            {
                ec.enabled = false; 
                ccl.enabled = true;
                anim.SetBool(TapKeyShift, true);

                maxSpeed += 2f;
                jump += 4f;
                accelerateText.text = "Ball on [Shift]\n\n +2 speed\n +4 jump";

                shiftPressed = false;
            }

            else
            {
                ec.enabled = true;
                ccl.enabled = false;
                anim.SetBool(TapKeyShift, false);

                maxSpeed -= 2f;
                jump -= 4f;
                accelerateText.text = "Ball off [Shift]";

                shiftPressed = true;
            }
        }
    }

    private void TurnIntoBall_Android()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.collider.name);
                    if (hit.collider.CompareTag("Player"))
                    {
                        if(shiftPressed)
                        {
                            ec.enabled = false; 
                            ccl.enabled = true;
                            anim.SetBool(TapKeyShift, true);

                            maxSpeed += 2f;
                            jump += 4f;
                            accelerateText.text = "Ball on [Shift] +2 speed +4 jump";

                            shiftPressed = false;
                        }
                        else
                        {
                            ec.enabled = true;
                            ccl.enabled = false;
                            anim.SetBool(TapKeyShift, false);

                            maxSpeed -= 2f;
                            jump -= 4f;
                            accelerateText.text = "Ball off [Shift]";

                            shiftPressed = true;
                        }
                    }
                }
            }
        }
    }

    private void Acceleration()
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

    private void Acceleration_Android()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        if(accelerateMove && shiftPressed)
                        {
                            maxSpeed += 5f;
                            accelerateText.text = "Acceleration on [V] +5 speed";
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
        }
    }
}