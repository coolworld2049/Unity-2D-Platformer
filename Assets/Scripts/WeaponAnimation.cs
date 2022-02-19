using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            anim.SetBool("PressedFire1", true);
        }

        if (Input.GetButton("Fire1") == false)
        {
            anim.SetBool("PressedFire1", false);
        }
    }
}
