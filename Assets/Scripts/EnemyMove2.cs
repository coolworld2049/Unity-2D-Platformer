using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove2 : MonoBehaviour
{
	public float Speed = 8f;
	private bool movingUp;
	private bool movingRight;

	private Rigidbody2D rb;

	// ����������  �������� �������
	public Transform point_1;
	public Transform point_2;

	// �������� ������ ����������� �������
	public bool VerticalMove;
	public bool HorizontalMove = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		// ����� ������ �������� � ����������
		if (VerticalMove)
        {
			if (gameObject.transform.position.y <= point_2.position.y)
			{
				movingUp = true;
			}

			if (gameObject.transform.position.y >= point_1.position.y)
			{
				movingUp = false;
			}
			VerticalMoving();
		}

		if (HorizontalMove)
		{
			if (gameObject.transform.position.x <= point_2.position.x)
			{
				movingRight = true;
			}

			if (gameObject.transform.position.x >= point_1.position.x)
			{
				movingRight = false;
			}
			HorizontalMoving();
		}

	}

	void VerticalMoving()
	{
		Health health = gameObject.GetComponent<Health>();

		if (movingUp)
		{
			if (health.HealthInitial > 0)
			{
				rb.velocity = new Vector2(rb.velocity.x, Speed ); //���������� � ���������� ����� RigidBody2D � ������ ��� �������� �� ��� �, 
			}

			if (health.HealthInitial <= 0)
			{
				rb.velocity = new Vector2(0, 0);
			}
		}

		else
		{
			if (health.HealthInitial > 0)
			{
				rb.velocity = new Vector2(rb.velocity.x, -Speed); //���������� � ���������� ����� RigidBody2D � ������ ��� �������� �� ��� �, 
			}

			if (health.HealthInitial <= 0)
			{
				rb.velocity = new Vector2(0, 0);
			}
		}
	}

	void HorizontalMoving()
	{
		Health health = gameObject.GetComponent<Health>();

		if (movingRight)
		{
			if (health.HealthInitial > 0)
			{
				rb.velocity = new Vector2(Speed, rb.velocity.y); //���������� � ���������� ����� RigidBody2D � ������ ��� �������� �� ��� �, 
			}

			if (health.HealthInitial <= 0)
			{
				rb.velocity = new Vector2(0, 0);
			}
		}

		else
		{
			if (health.HealthInitial > 0)
			{
				rb.velocity = new Vector2(-Speed, rb.velocity.y); //���������� � ���������� ����� RigidBody2D � ������ ��� �������� �� ��� �, 
			}

			if (health.HealthInitial <= 0)
			{
				rb.velocity = new Vector2(0, 0);
			}
		}
	}
}
