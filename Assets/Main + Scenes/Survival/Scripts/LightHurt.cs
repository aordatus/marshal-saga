using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightHurt : MonoBehaviour
{
    PlayerHealth ph;
    public bool shallwehurt = false;
    private ParticleSystem thunder;
    private ParticleSystem thunder2;

    public int hurtintensity;
    GameManager GM;
    Scene scene;
    private void Start()
    {
       scene = SceneManager.GetActiveScene();

        if (scene.name == "Survival")
        {
            ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            thunder = GameObject.FindGameObjectWithTag("Electricity").GetComponent<ParticleSystem>();

            thunder2 = GameObject.FindGameObjectWithTag("Electricity2").GetComponent<ParticleSystem>();
            GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();


        }
        else if (scene.name == "Survival Second Stage")
        {
            ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            thunder = GameObject.FindGameObjectWithTag("Electricity").GetComponent<ParticleSystem>();



        }

       

        

    }

    private void Update()
    {
       /* if (scene.name == "Survival")
        {
            if (GM.distancefromspectre < 15)
            {
                ph.health -= Time.deltaTime * hurtintensity;
                thunder2.Play();
            }

            else if (GM.distancefromspectre > 15)
            {
                thunder2.Stop();
            }

        }
        
    */
     
       
       
       

        
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            ph.health -= Time.deltaTime * hurtintensity;
            thunder.Play();

        }


    }
        
        
   
    private void OnTriggerExit2D(Collider2D other)
    {  
        if (other.CompareTag("Player"))
        {
            thunder.Stop();



        }
    }
}
