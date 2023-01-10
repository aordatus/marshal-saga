    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState Instance;
    public int target = 60;

    [Header("Tutorial & Logo Transition")]
    public  bool firsttimeopened;
    public  bool shallwedotutorial;


    [Header("Running")]
    public AudioMixer audiomixer;
    public AudioMixer audiomixer2;
    public bool soundoff = false;
    public bool musicoff = false;
    public bool savegame = true;
    Scene scene;
    GameManager gm;
    GameManagerSecond gm2;

    [Header("Player Data")]
        public int totalexp;
        public int level;
        public int gold;
        public float basehealth;
        public float basespeed;
        public float baseCP = 2;
        public int highestscore;



    void Awake()
    {
        highestscore = PlayerPrefs.GetInt("HighScore");
        gold = PlayerPrefs.GetInt("Wealth"); 
        level = PlayerPrefs.GetInt("Level");
        totalexp = PlayerPrefs.GetInt("Exp");

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }


    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;


    }

    public void SavePlayer()
    {

        //Exp & Gold
        if (savegame == true)
        {
            scene = SceneManager.GetActiveScene();

            int expearned = PlayerPrefs.GetInt("Exp");

            if (scene.name == "Survival")
            {
                gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                //Exp
                expearned += gm.scorepoints;


            }
            else if (scene.name == "Survival Second Stage")
            {
                gm2 = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerSecond>();
                //Exp
                expearned += gm2.scorepoints;


            }

            totalexp = expearned;

            savegame = false;
        }

    }
    public void Levelling()
    {
        level = PlayerPrefs.GetInt("Level");
        PlayerPrefs.SetInt("Exp", totalexp);
        Debug.Log("exp" + PlayerPrefs.GetInt("Exp") + "money" + PlayerPrefs.GetInt("Wealth"));


        if(totalexp >= 0 && totalexp <= 1000)
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        else if (totalexp >= 1000 && totalexp <= 3000)
        {
            PlayerPrefs.SetInt("Level", 2);
        }

        else if (totalexp >= 3000 && totalexp <= 6000)
        {
            PlayerPrefs.SetInt("Level", 3);

        }

        else if (totalexp >= 6000 && totalexp <= 9000)
        {
            PlayerPrefs.SetInt("Level", 4);

        }

        else if (totalexp >= 9000 && totalexp <= 15000)
        {
            PlayerPrefs.SetInt("Level", 5);

        }

        else if (totalexp >= 15000)
        {
            PlayerPrefs.SetInt("Level", 6);
 
        }

        

        if (level == 2)
        {
            baseCP = 4;

        }

        if (level == 3)
        {
            basehealth = 200;
            baseCP = 4;


        }

        if (level == 4)
        {
            baseCP = 6;
            basehealth = 200;


        }

        if (level == 5)
        {
            baseCP = 6;
            basehealth = 300;


        }


        if (level == 6)
        {
            baseCP = 20;
            basehealth = 1000;


        }


    }

    private void Update()
    {
        Levelling();


        if (musicoff == true){
            audiomixer.SetFloat("Music", -80f);

        }

        else if (musicoff == false)
        {
            audiomixer.SetFloat("Music", 0f);

        }

        if (soundoff == true)
        {
            audiomixer2.SetFloat("Sound", -80f);

        }

        else if (soundoff == false)
        {
            audiomixer2.SetFloat("Sound", 0f);

        }

        if (target != Application.targetFrameRate)
        {
            Application.targetFrameRate = target;
        }

        if (PlayerPrefs.GetInt("shallwedotutorial") == 0)
        {
            shallwedotutorial = true;


        }
        else if (PlayerPrefs.GetInt("shallwedotutorial") == 1)
        {
            shallwedotutorial = false;


        }

       

    }

   



}
