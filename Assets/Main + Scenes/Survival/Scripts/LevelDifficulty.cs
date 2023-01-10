using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDifficulty : MonoBehaviour
{
    public GameObject MonsterManagement1;
    public GameObject MonsterManagement2;
    public GameObject MonsterManagement3;
    public GameObject MonsterManagement4;

    private MonsterManagement mm2;

    public int wavenumber;
    public float wave = 1;
    private Spectre spectre;
    GameManager gm;
    PlayerHealth ph;
    //public GameObject Waveone;
    // public GameObject Waveone;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        //MonsterManagement mm2 = MonsterManagement2.GetComponent<MonsterManagement>();
        spectre = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Spectre>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (wavenumber == 1)
            {
                MonsterManagement1.SetActive(false);
                MonsterManagement2.SetActive(true);
                //mm2.next = true;
                gm.scorepoints += 100;

                wave = 2;
                Destroy(this.gameObject);

            }
            if (wavenumber == 2)
            {
               
                MonsterManagement1.SetActive(false);
                MonsterManagement2.SetActive(false);
                MonsterManagement3.SetActive(true);
                gm.scorepoints += 100;

                wave = 3;
                Destroy(this.gameObject);

            }

            else if (wavenumber == 3)
            {
                MonsterManagement1.SetActive(false);
                MonsterManagement2.SetActive(false);
                MonsterManagement3.SetActive(false);
                MonsterManagement4.SetActive(true);
                gm.scorepoints += 100;
                if(PlayerPrefs.GetInt("RulthP") >= 1)
                {
                    ph.RPCD.SetActive(true);
                }
                spectre.enabled = false;
                wave = 4;
                Destroy(this.gameObject);

            }


        }
    }
}
