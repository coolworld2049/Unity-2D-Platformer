using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour
{
    public Transform[] firePoint; //позиция из которой будут вылетать пули
    public GameObject[] bulletPrefab; //префаб пуль
    [FormerlySerializedAs("weaponName")] public GameObject[] weaponPrefab; //объекты пушек
    public int bulletPrefabCount = 1000;
    
    
#if UNITY_STANDALONE_WIN
    protected void Shoot()
    {
        for (int j = 0; j < weaponPrefab.Length; j++)
        {
            if(weaponPrefab[j].activeInHierarchy && bulletPrefabCount >= 0)
            {
                Instantiate(bulletPrefab[j], firePoint[j].position, firePoint[j].rotation);
                bulletPrefabCount -= 1;
            }
        }
    }
        
#endif    
#if UNITY_ANDROID
    protected void Android_Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) 
        {
                        
        }
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase is TouchPhase.Stationary or TouchPhase.Moved)
            {
                /*Vector3 newDir = Vector3.RotateTowards(
                    transform.forward, 
                    (new Vector3(Input.GetTouch(i).position.x,Input.GetTouch(i).position.y, 0.0f)), 
                    3.14159F, 0.0F);
                Quaternion rotation = Quaternion.LookRotation(newDir);*/
                for (int j = 0; j < weaponPrefab.Length; j++)
                {
                    if(weaponPrefab[j].activeInHierarchy && bulletPrefabCount >= 0)
                    {
                        Instantiate(bulletPrefab[j], firePoint[j].position, rotation);
                        bulletPrefabCount -= 1;
                    }
                }
            }
        }
    }
#endif

}
