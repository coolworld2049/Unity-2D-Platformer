using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
	public float Speed = 8f;
	public float DeploymentHeight = 1;
	private bool canJump = false;
	private bool movingUp;
	private bool movingRight;

	private Rigidbody2D rb;

	// траектория  движения объекта
	public Transform point_1;
	public Transform point_2;

	// чекбоксы выбора направления объекта
	public bool VerticalMove;
	public bool HorizontalMove;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		// выбор метода движения в инспекторе
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
				rb.velocity = new Vector2(rb.velocity.x, Speed);
				//обращаемся к компоненту врага RigidBody2D и задаем ему скорость по оси Х, 
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
				rb.velocity = new Vector2(rb.velocity.x, -Speed);
				//обращаемся к компоненту врага RigidBody2D и задаем ему скорость по оси Х, 
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
				rb.velocity = new Vector2(Speed, rb.velocity.y);
				//обращаемся к компоненту врага RigidBody2D и задаем ему скорость по оси Х, 
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
				rb.velocity = new Vector2(-Speed, rb.velocity.y);
				//обращаемся к компоненту врага RigidBody2D и задаем ему скорость по оси Х, 
			}

			if (health.HealthInitial <= 0)
			{
				rb.velocity = new Vector2(0, 0);
			}
		}
	}

	public void Jump()
	{
		RaycastHit hit;
		Ray landingRay = new Ray(transform.position, Vector3.down);
		Debug.DrawRay(transform.position, Vector2.right * DeploymentHeight, Color.red);
		if (Physics.Raycast(landingRay, out hit, DeploymentHeight))
		{
			canJump = hit.collider != null;
		}

		if (canJump)
		{
			rb.AddForce(transform.up * 20f, ForceMode2D.Impulse);
		}
	}

}
