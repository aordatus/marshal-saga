using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour

{
    [Header("Player")]
    public float sliderhealth;
    public float health;
    public float CP = 2;
    public int level;

    [Header("Running")]
    public float restartdelay = 6;
    public bool lurion = false;
    public bool isDead = false;
    public bool Damaged = true;
    Scene scene;

    PlayerMovement pm;
    GameManager gm;
    GameManagerSecond gm2;

    GameState gamestate;
    [Header("Hurt")]
    public GameObject Heartbeat;
    private AudioSource heartsource;
    public AudioClip Heartbeatsound;
   // private bool work1 = false;
    private bool work2 = true;
    public Image damageImage;
    public float flashspeed = 1f;
    public Color flashColour = new Color(1f, 0f, 0f, 1f);

    [Header("Items")]
    public GameObject HPP; //HP Potion
    public GameObject RP; //Rulth Potion
    public GameObject RPCD; // Rulth Potion Cooldown Panel Lock
   
    public bool hppotionclick = false;
    public bool rulthpotionclick = false;
    Spectre spectrescript;
    float PreviousSpeed;

    
    void Start()
    {
        heartsource = Heartbeat.GetComponent<AudioSource>();
        gamestate = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spectrescript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Spectre>();

        sliderhealth = gamestate.basehealth;
        health = gamestate.basehealth;

        CP = gamestate.baseCP;

    }

    void Update() {
        
        hpuse();
        rulthuse();
        powerlight();

        if (health <= sliderhealth/5)
        {
            if (work2 == true)
            {
                StartCoroutine(Hurt());

            }
            
        }

        else
        {
            StopCoroutine(Hurt());
            damageImage.color = new Color(0, 0, 0, 0);

        }

        Damaged = false;
        
    }


    IEnumerator Hurt()
    {
        work2 = false;

        heartsource.PlayOneShot(Heartbeatsound);
        damageImage.color = flashColour;
        yield return new WaitForSeconds(.5f);

        damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashspeed * Time.deltaTime);
        yield return new WaitForSeconds(1f);

        work2 = true;


    }



    public void TakeDamage(int amount)
    {
        Damaged = true;
        health -= amount;

        //playaudio 




    }

   

    public void hpuse()
    {
        int hpno = PlayerPrefs.GetInt("HealP");

        if (hpno == 0)
        {
            HPP.SetActive(false);
        }
        else
        {
            HPP.SetActive(true);
            if (hppotionclick == true)
            {
                float healthlost = sliderhealth - health;
                health += healthlost;
                hppotionclick = false;
                hpno = PlayerPrefs.GetInt("HealP");

                PlayerPrefs.SetInt("HealP", hpno - 1);
            }
        }
    }


    public void rulthuse()
    {
        int rulthno = PlayerPrefs.GetInt("RulthP");

        if (rulthno == 0)
        {
            RP.SetActive(false);
        }
        else
        {
            RP.SetActive(true);
            if (rulthpotionclick == true)
            {
                PreviousSpeed = spectrescript.speed;
                //spectrescript.speed = 0;
                spectrescript.enabled = false;
                rulthpotionclick = false;
                RPCD.SetActive(true);

                Invoke("resetspectre", 5f);
                
                rulthno = PlayerPrefs.GetInt("RulthP");

                PlayerPrefs.SetInt("RulthP", rulthno - 1);
            }
        }
    }
    public void hpon()
    {
        hppotionclick = true;
    }
    public void rulthon()   
    {
        rulthpotionclick = true;

    }
    void resetspectre()
    {
        RPCD.SetActive(false);
        spectrescript.enabled = true;
        spectrescript.speed = PreviousSpeed;


    }

    public void hpplus()
    {
        int hpno = PlayerPrefs.GetInt("HealP");
        PlayerPrefs.SetInt("HealP", hpno + 1);

        int basewealth = PlayerPrefs.GetInt("Wealth");
        PlayerPrefs.SetInt("Wealth", basewealth - 20);
    
    }

    public void hpminus()
    {
        int hpno = PlayerPrefs.GetInt("HealP");
        PlayerPrefs.SetInt("HealP", hpno - 1);

        int basewealth = PlayerPrefs.GetInt("Wealth");
        PlayerPrefs.SetInt("Wealth", basewealth + 20);
    }

    public void rulthplus()
    {
        int rulthno = PlayerPrefs.GetInt("RulthP");
        PlayerPrefs.SetInt("RulthP", rulthno + 1);

        int basewealth = PlayerPrefs.GetInt("Wealth");
        PlayerPrefs.SetInt("Wealth", basewealth - 20);

    }

    public void rulthminus()
    {
        int rulthno = PlayerPrefs.GetInt("RulthP");
        PlayerPrefs.SetInt("RulthP", rulthno - 1);

        int basewealth = PlayerPrefs.GetInt("Wealth");
        PlayerPrefs.SetInt("Wealth", basewealth + 20);

    }


    private void powerlight()
    {
        if (lurion == true && pm.canMove == true)
        {
            health -= Time.deltaTime * 1;

        }
    }

    public void turnonbool()
    {

        lurion = true;

        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();


    }


    public void turnoffbool()
    {
        lurion = false;

    }
}
