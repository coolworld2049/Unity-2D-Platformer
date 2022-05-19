using UnityEngine;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour
{
    public Transform[] firePoint; //позиция из которой будут вылетать пули
    public GameObject[] bulletPrefab; //префаб пуль
    [FormerlySerializedAs("weaponName")] public GameObject[] weaponPrefab; //объекты пушек
    public int bulletPrefabCount = 100;

    private int gun_0 = 0;
    private int gun_1 = 1;
    
    void Update()
    {
        //P90
        if(weaponPrefab[gun_0].activeInHierarchy && weaponPrefab[gun_0].gameObject.name == "P90")
        {
            if ((Input.GetButton("Fire1")) && (bulletPrefabCount >= 0))
            {
                Instantiate(bulletPrefab[gun_0], firePoint[gun_0].position, firePoint[gun_0].rotation); //shooting
                bulletPrefabCount -= 1;
            }
        }

        //AWP
        if (weaponPrefab[gun_1].activeInHierarchy && weaponPrefab[gun_1].gameObject.name == "AWP")
        {
            if (Input.GetButton("Fire1")) 
            {
                Instantiate(bulletPrefab[1], firePoint[gun_1].position, firePoint[gun_1].rotation); // shooting
            }
        }
    }
}
