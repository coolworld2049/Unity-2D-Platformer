using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMove : MonoBehaviour
{
    public float maxSpeed;
    public float jump;

    private bool isFacingRight = true; //�������� � ����� ������� ��������� �����
    private bool isGrounded = true; //�������� ��������� �� ����� �� ��������� (Tilemap)
    public float countCrystall; //������� ���������� ��������
    private int keyPressCount = 0; //������� ������� �������
    private bool shiftPressed = true; //�������� ������� Shift
    private bool accelerateMove = true; // ��������� ������

    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private CircleCollider2D ccl;

    public TMP_Text countCrystallText; // ��������� ���� ��� ������ ����� ����������
    public TMP_Text accelerateText; 
    public GameObject[] flipAxis;

    void Start()
    {
        //������������� ���������� UnityEngine ������������� ������� ������
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        ccl = GetComponent<CircleCollider2D>();
        ccl.enabled = false;
    }

    //��������� ������� �� ������ �����
    void Update()
    {
        HorizontalMove();
        Jump();
        PlayerBall();
        Acceleration();
    }

    //��������� ����� ������������� ���������� ������
    void FixedUpdate()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(horizontalMove)); //� ���������� �������� �������� �������� ��������� Speed �� �������� ��� � (�������� ������)
    }


    //������������� �������� ������
    public void HorizontalMove()
    {
        //���������� Input.GetAxis ��� ��� �. ����� ���������� �������� ��� � �������� �� -1 �� 1.
        //-1 ������������ ��� ������� ������� �
        //1 ������������ ��� ������� ������� D
        float move = Input.GetAxis("Horizontal");

        //���������� � ���������� ��������� RigidBody2D � ������ ��� �������� �� ��� �, 
        rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

        //���� ������ ������� ��� ����������� ������, � �������� ��������� �����
        if (move > 0 && !isFacingRight) 
        {
            Flip(); //�������� ��������� ������
        }
        else if (move < 0 && isFacingRight) 
        {
            Flip(); //�������� ��������� �����
        }        
    }

    //����� ����������� �������� ��������� � ��� ���������� ���������
    public void Flip()
    {
        FlipObjectAxis(); // ����� ����������� ��� X ��� �������� ������

        isFacingRight = !isFacingRight; //������ ����������� �������� ���������
        Vector3 theScale = transform.localScale; //�������� ������� ���������
        theScale.x *= -1; //��������� �������� ��������� �� ��� �
        transform.localScale = theScale; //������ ����� ������ ���������, ������ �������, �� ��������� ����������
    }

    //����� ������������ ��� X ������� Fire point
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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) //������ ���� ����� ������ � ����� ��������� �� ���������
        {
            rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded) //������� ������ � ������� ��� ������� ������� �������
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
        anim.SetBool("SpaceKeyPressed", oncePress); //�������� ������ (���������� ��� ��������� �������)
        oncePress = false;
    }

    //�������� �� ��������������� ������ � ����������
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

    //������� ����������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collected Object")
        {
            Destroy(collision.gameObject); // ����������� ������� ���� ����� ���������� � ���
            countCrystall++;
            countCrystallText.text = countCrystall.ToString();
        }
    }

    //�������� ����
    public void PlayerBall()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            if(shiftPressed)
            {
                bc.enabled = false; //����������� �������� ��������� (BoxCollider2D)
                ccl.enabled = true; //���������� ��������� ����� (CircleCollider2D)
                anim.SetBool("TapKeyShift", true); //�������� ����

                maxSpeed += 2f;
                jump += 4f;
                accelerateText.text = "Ball on [Shift]\n\n +2 speed\n +4 jump";

                shiftPressed = false;
            }

            else
            {
                bc.enabled = true; //����������� �������� ��������� (BoxCollider2D)
                ccl.enabled = false; //���������� ��������� ����� (CircleCollider2D)
                anim.SetBool("TapKeyShift", false); //�������� ����

                maxSpeed -= 2f;
                jump -= 4f;
                accelerateText.text = "Ball off [Shift]";

                shiftPressed = true;
            }
        }
    }

    // ��������� ������
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