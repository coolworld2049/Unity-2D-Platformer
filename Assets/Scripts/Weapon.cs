using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour
{
    public Transform[] firePoint; //позиция из которой будут вылетать пули
    public GameObject[] bulletPrefab; //префаб пуль
    [FormerlySerializedAs("weaponName")] public GameObject[] weaponPrefab; //объекты пушек
    public int bulletPrefabCount = 1000;
    public Camera mainCamera;
    private GameObject playerPrefab;
    private CharacterMove chMove;

    protected bool CanShoot { get; set; } = true;

    private void Awake()
    {
        chMove = GetComponent<CharacterMove>();
    }
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
    protected void Android_Shoot_Raycast()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase is TouchPhase.Stationary or TouchPhase.Moved && CanShoot)
            {
                Android_Shoot(i);
            }
        }
    }

    private void Android_Shoot(int touchIndex)
    {
        for (int j = 0; j < weaponPrefab.Length; j++)
        {
            if (weaponPrefab[j].activeInHierarchy && bulletPrefabCount > 0)
            {
                Vector3 touch = mainCamera.ScreenToWorldPoint(Input.GetTouch(touchIndex).position);
                var deltaX = touch.x - firePoint[j].position.x;
                var deltaY = touch.y -  firePoint[j].position.y;
                float rotationDegrees = (180 / Mathf.PI) * Mathf.Atan2(deltaY, deltaX);
                if ((rotationDegrees is <= 90 and >= -50) && !(chMove.IsFacingRight))
                {
                    chMove.Flip();
                    weaponPrefab[j].transform.localScale = new Vector3(-1f,1f,1f);
                }
                if ((rotationDegrees is >= 90 and >= 0) && (chMove.IsFacingRight))
                {
                    chMove.Flip();
                    weaponPrefab[j].transform.localScale = new Vector3(1f,-1f,1f);
                }
                weaponPrefab[j].transform.rotation = Quaternion.Euler(0,0,rotationDegrees);
                firePoint[j].rotation = Quaternion.Euler(0, 0, rotationDegrees);
                Instantiate(bulletPrefab[j], firePoint[j].position, firePoint[j].rotation);
                bulletPrefabCount -= 1;
            }
        }
    }
    #endif
}
