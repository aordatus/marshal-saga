using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHit : MonoBehaviour
{
    private SoundManager sm;


    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Survival" || scene.name == "Survival Second Stage" )
        {
            
                sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

           

        }



        }

        void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soda"))
        {
            other.GetComponent<ItemBreak>().smash();
            sm.audiosource.PlayOneShot(sm.clip2);

        }


        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Monsters>().kill();
            sm.audiosource.PlayOneShot(sm.clip1);

        }
    }
}
