using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthBar;
    public Health playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(); // ������ � ������� Health ����� Player
        healthBar = GetComponent<Slider>();
    }

    private void Update()
    {
        healthBar.value = playerHealth.HealthInitial; // �������� �������� �������� ����� ��������
    }
}
