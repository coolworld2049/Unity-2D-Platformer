using System.Globalization;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class CharacterMove : MonoBehaviour
{
    private RaycastHit2D raycastHit2D;
    public float rayLenght;
    public float maxSpeed;
    public float jump;
    public float countCrystall; //������� ���������� ��������
    public TMP_Text countCrystallText; // ��������� ���� ��� ������ ����� ����������
    public TMP_Text accelerateText; 
    public GameObject[] flipAxis;
    public Joystick joystick;
    
    private bool isFacingRight = true; //�������� � ����� ������� ��������� �����
    private bool isGrounded = true; //�������� ��������� �� ����� �� ��������� (Tilemap)
    private int keyPressCount; //������� ������� �������
    private bool shiftPressed = true; //�������� ������� Shift
    private bool accelerateMove = true; // ��������� ������
    
    private Animator anim;
    private Rigidbody2D rb;
    private EdgeCollider2D ec;
    private CircleCollider2D ccl;

    
    private static readonly int SpaceKeyPressed = Animator.StringToHash("SpaceKeyPressed");
    private static readonly int TapKeyShift = Animator.StringToHash("TapKeyShift");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private bool canJump = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ec = GetComponent<EdgeCollider2D>();
        ccl = GetComponent<CircleCollider2D>();
        ccl.enabled = false;
    }

    //��������� ������� �� ������ �����
    void Update()
    {   
#if UNITY_ANDROID
        Android_HorizontalMove();
        Android_Jump();
#endif
        
#if UNITY_STANDALONE_WIN
        HorizontalMove();
        Jump();
#endif
        PlayerBall();
        Acceleration();
    }

    void FixedUpdate()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        anim.SetFloat(Speed, Mathf.Abs(horizontalMove)); 
    }
    

    private void HorizontalMove()
    {
        //���������� Input.GetAxis ��� ��� �. ����� ���������� �������� ��� � �������� �� -1 �� 1.
        //-1 ������������ ��� ������� ������� �
        //1 ������������ ��� ������� ������� D
        float move = Input.GetAxis("Horizontal");

        //���������� � ���������� ��������� RigidBody2D � ������ ��� �������� �� ��� �, 
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

    private void Flip()
    {
        FlipObjectAxis(); // ����� ����������� ��� X ��� �������� ������

        isFacingRight = !isFacingRight; //������ ����������� �������� ���������
        Vector3 theScale = transform.localScale; //�������� ������� ���������
        theScale.x *= -1; //��������� �������� ��������� �� ��� �
        transform.localScale = theScale; //������ ����� ������ ���������, ������ �������, �� ��������� ����������
    }

    //����� ������������ ��� X ������� Fire point
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
        
        
        if (isGrounded && joystick.Vertical >= .1f && canJump)
        {
            //keyPressCount = 1;
            rb.velocity = new Vector2(rb.velocity.x, jump);

            //rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
            anim.speed = 0.8f;
            anim.SetBool(SpaceKeyPressed, true);
        }
        else
        {
            anim.SetBool(SpaceKeyPressed, false);
            anim.speed = 1;
        }
    }

    
    //�������� �� ��������������� ������ � ����������
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

    //������� ����������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collected Object"))
        {
            Destroy(collision.gameObject); // ����������� ������� ���� ����� ���������� � ���
            countCrystall++;
            countCrystallText.text = countCrystall.ToString(CultureInfo.CurrentCulture);
        }
    }

    //�������� ����
    private void PlayerBall()
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
}