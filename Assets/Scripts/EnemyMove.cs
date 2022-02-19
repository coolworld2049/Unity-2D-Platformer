using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour 
{
	public float speed = 8f;
	private float direction = -1f;

	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		Health health = gameObject.GetComponent<Health>(); 

		if (health.HealthInitial > 0)
        {
			rb.velocity = new Vector2(speed * direction, rb.velocity.y); //���������� � ���������� ����� RigidBody2D � ������ ��� �������� �� ��� �, 
			transform.localScale = new Vector2(direction, 1); //direction ���������� ����������� �������� �����
		}

		if (health.HealthInitial <= 0)
		{
			rb.velocity = new Vector2(0, 0); 
			transform.localScale = new Vector2(direction, 1);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player") // ������ ����� ������������ �� 180 �������� ���� ������������ � �������
		{
			direction *= -1f;
		}

		if (collision.gameObject.tag == "Enemy Bound") // ������ ����� ������������ �� 180 �������� ���� ������������ � �������
		{
			direction *= -1f;
		}
	}
}
