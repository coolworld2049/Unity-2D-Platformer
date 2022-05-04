using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	public float Speed = 8f;
	private bool movingUp;
	private bool movingRight;

	private Rigidbody2D rb;

	// траектория  движения объекта
	public Transform bound_1;
	public Transform bound_2;

	// чекбоксы выбора направления объекта
	public bool VerticalMove;
	public bool HorizontalMove = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		// выбор метода движения в инспекторе
		if (VerticalMove)
		{
			if (gameObject.transform.position.y <= bound_2.position.y)
			{
				movingUp = true;
			}

			if (gameObject.transform.position.y >= bound_1.position.y)
			{
				movingUp = false;
			}
			VerticalMoving();
		}

		if (HorizontalMove)
		{
			if (gameObject.transform.position.x <= bound_2.position.x)
			{
				movingRight = true;
			}

			if (gameObject.transform.position.x >= bound_1.position.x)
			{
				movingRight = false;
			}
			HorizontalMoving();
		}
	}

	void VerticalMoving()
	{
		if (movingUp)
		{
			rb.velocity = new Vector2(rb.velocity.x, Speed);
		}

		else
		{
			rb.velocity = new Vector2(rb.velocity.x, -Speed);
		}
	}

	void HorizontalMoving()
	{
		if (movingRight)
		{
			rb.velocity = new Vector2(Speed, rb.velocity.y);
		}

		else
		{
			rb.velocity = new Vector2(-Speed, rb.velocity.y);
		}
	}
}
