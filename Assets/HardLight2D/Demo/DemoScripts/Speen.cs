using UnityEngine;

public class Speen : MonoBehaviour
{
    public float speed = 1f;
    void Update ()
    {
        transform.Rotate (Vector3.forward, speed * Time.deltaTime * 100f);
    }
}