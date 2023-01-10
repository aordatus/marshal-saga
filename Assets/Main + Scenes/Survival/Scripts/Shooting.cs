 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firepoint;
    public GameObject fireball;

    public float force;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();

        }

    }

    void Shoot()
    {
        GameObject fire = Instantiate(fireball, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = fire.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * force, ForceMode2D.Impulse);
    }



}
