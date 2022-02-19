using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform[] firePoint; //позиция из которой будут вылетать пули
    public GameObject[] bulletPrefab; //префаб пуль
    public GameObject[] weaponName; //объекты пушек

    private int gun_0 = 0;
    private int gun_1 = 1;


    void Update()
    {
        //P90
        if(weaponName[gun_0].activeInHierarchy && weaponName[gun_0].gameObject.name == "P90")
        {
            if (Input.GetButtonDown("Fire1"))
            {  
                Instantiate(bulletPrefab[gun_0], firePoint[gun_0].position, firePoint[gun_0].rotation); //shooting
            }
        }

        //AWP
        if (weaponName[gun_1].activeInHierarchy && weaponName[gun_1].gameObject.name == "AWP")
        {
            if (Input.GetButtonDown("Fire1")) 
            {
                Instantiate(bulletPrefab[1], firePoint[gun_1].position, firePoint[gun_1].rotation); // shooting
            }
        }
    }
}
