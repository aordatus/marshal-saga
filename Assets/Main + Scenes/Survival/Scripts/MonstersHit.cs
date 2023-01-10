using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersHit : MonoBehaviour
{
    private Transform target;
    private bool inRange = false;
    public float timebetweenattacks;
    public int attackdamage;
    private float timer;
    

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        shallweassault();

    }

    public void shallweassault()
    {
        timer += Time.deltaTime;

        if (timer >= timebetweenattacks && inRange == true)
        {

            yesassault();
        }
    }

    public void yesassault()
    {
        timer = 0f;
        PlayerHealth ph = target.GetComponent<PlayerHealth>();
        if (ph.health > 0)
        {
            ph.TakeDamage(attackdamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
