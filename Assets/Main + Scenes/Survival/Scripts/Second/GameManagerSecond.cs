using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManagerSecond : MonoBehaviour
{
    
    [Header("Bottom UI")]
    public GameObject bottomUI;

    //Monsters
    public float monsterskilled = 0;
    public TextMeshProUGUI monsterkilledtext;
    public bool isKilled = false;

    //Distance
    public float distancefromspectre;
    public TextMeshProUGUI distancefromspectretext;
    private Transform player;
    private Transform spectre;

    //TimeLeft
    public int secondslived = 0;
    public bool takingAway = false;
    public TextMeshProUGUI timertext;
    public bool timeshouldupdate;

    //Buttons
    public Button sword;
    public Button lurion;
    PlayerMovement pm;

    //Console
    public TextMeshProUGUI console;
    public TextMeshProUGUI console2;
    private bool consolechanged = false;
    private bool consolechanged2 = false;

    [Header("Vriwon Status")]
    public GameObject VriwonStatus;
    public Slider healthslider;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI level;
    public TextMeshProUGUI level2;
    public TextMeshProUGUI exp;
    int previouslevel;
    public TextMeshProUGUI leveladvancetext;
    private PlayerHealth ph;
    public TextMeshProUGUI HPPAmount;
    public TextMeshProUGUI RPAmount;

    [Header("Scoreboard")]
    // public Gameobject scoreboard;
    public int scorepoints;
    bool scorefilled = false;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highestscore;
    public GameObject highestscoregameobject;


    
    [Header("Spectre")]    //Speeding
    //public float glacialboost;
    Spectre spectrescript;

    [Header("Management")]
    Animator gmanimator;
    GameState GameState;
    public GameObject musicOn;
    public GameObject musicOff;
    public GameObject soundOn;
    public GameObject soundOff;
    public GameObject restart;
    public float restartdelay = 6;

    //Sound
    public AudioClip VriwonDeath;
    public AudioSource VriwonDies;
    private bool doneEffect1 = false;
    void Start()
    {
        previouslevel = PlayerPrefs.GetInt("Level");

        timeshouldupdate = true; 
        GameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();

        spectrescript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Spectre>();
        spectre = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gmanimator = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Animator>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        console.text = "";
        InvokeRepeating("consoleclearinvoke", 0f, 1f);



        //TimeLeft Bottom UI
        timertext.text = secondslived + " - Seconds Survived";
        GameState.savegame = true;

    }

    void Update()
    {
      
        VriwonUI();
        BottomUI();
        healthout();
        if (GameState.musicoff == true)
        {

            musicOff.SetActive(true);
            musicOn.SetActive(false);

        }
        else if (GameState.musicoff == false)
        {
            musicOff.SetActive(false);
            musicOn.SetActive(true);

        }

        if (GameState.soundoff == true)
        {

            soundOff.SetActive(true);
            soundOn.SetActive(false);

        }
        else if (GameState.soundoff == false)
        {
            soundOff.SetActive(false);
            soundOn.SetActive(true);

        }
    }

    void Vriwondies()
    {
        VriwonDies.PlayOneShot(VriwonDeath);
        doneEffect1 = true;

    }

    public void MusicOff()
    {
        GameState.musicoff = true;

    }

    public void MusicOn()
    {
        GameState.musicoff = false;


    }

    public void SoundOff()
    {
        GameState.soundoff = true;

    }

    public void SoundOn()
    {
        GameState.soundoff = false;

    }
    void consoleclearinvoke()
    {
        StartCoroutine(Consoleclear());
    }
    IEnumerator Consoleclear()
    {
        if (console2.text != "")
        {
            yield return new WaitForSeconds(4f);
            console2.text = "";

        }
        else
        {
            yield return new WaitForSeconds(0f);

        }
    }
    public void home()
    {
        SceneManager.LoadScene("menu");

    }


    void Scoreboard()
    {
        //Score //This is Updated fro Captured and Health
        if (scorefilled == false && pm.canMove == false)
        {
            scorepoints += (int)(monsterskilled * 10);
            scorepoints += secondslived;
            scorefilled = true;
        }


        if (scorepoints > PlayerPrefs.GetInt("HighScore2"))
        {

            PlayerPrefs.SetInt("HighScore2", scorepoints);

            score.text = "Wow! It is your Highest Score: " + scorepoints;
            highestscoregameobject.SetActive(false);


        }

        else if (PlayerPrefs.GetInt("HighScore2") >= scorepoints)
        {   
            score.text = "Score: " + scorepoints;
            highestscore.text = "Highest Score: " + PlayerPrefs.GetInt("HighScore2");
        }

        int currentlevel = PlayerPrefs.GetInt("Level");
        if (currentlevel != previouslevel)
        {
            leveladvancetext.text = "Congratulations you leveled up!" + " Level: " + currentlevel;


        }


    }
    void VriwonUI()
    {
        if (PlayerPrefs.GetInt("HealP") >= 1)
        {
            HPPAmount.text = PlayerPrefs.GetInt("HealP").ToString();


        }

        if (PlayerPrefs.GetInt("HealP") <= 0)
        {
            HPPAmount.text = "";


        }

        if (PlayerPrefs.GetInt("RulthP") >= 1)
        {
            RPAmount.text = PlayerPrefs.GetInt("Rulth").ToString();


        }

        if (PlayerPrefs.GetInt("RulthP") <= 0)
        {
            RPAmount.text = "";

        }

            //GoldDisplay
            gold.text = "Wealth: " + PlayerPrefs.GetInt("Wealth");

        //LevelDisplay
        level.text = "Level: " + PlayerPrefs.GetInt("Level");
        exp.text = "Your Experience: " + PlayerPrefs.GetInt("Exp");
        level2.text = "" + PlayerPrefs.GetInt("Level");

        //Health
        healthslider.value = ph.health;
        healthslider.maxValue = ph.sliderhealth;

    }

    void BottomUI()
    {
        //Updating Monsters Killed Text
        monsterkilledtext.text = "Monsters Killed: " + monsterskilled.ToString();
        if (isKilled == true)
        {
            Monsterkilled();
        }
       

        //Updating Monsters Killed Text
        UpdateDistance();


        //Updating Time Left Text
        if (takingAway == false)
        {
            if (timeshouldupdate == true)
            {
                StartCoroutine(TimeUpdate());

            }
        }


        //Buttons
        if (Input.GetButtonDown("box1") && pm.canMove == true)
        {
            sword.onClick.Invoke();

        }

        if (Input.GetButtonDown("box2") && pm.canMove == true)
            NewMethod();








        ifdead();
    }

    private void NewMethod()
    {
        lurion.onClick.Invoke();

    }

    public void pausegame()
    {
        Time.timeScale = 0;
        pm.enabled = false;


    }
    public void resumegame()
    {
        Time.timeScale = 1;
        pm.enabled = true;

        //Enable Scripts that still work

    }


    void ifdead()
    {




        if (pm.canMove == false)
        {


            bottomUI.SetActive(false);
            VriwonStatus.SetActive(false);
            gmanimator.SetBool("die", true);




            if (ph.health <= 0)
            {
                console.text = "You died...";
            }



        }
    }

    public void captured()
    {
        timeshouldupdate = false;

        Scoreboard();
        if (doneEffect1 == false)
        {
            Invoke("Vriwondies", restartdelay - 5);

        }


        console.text = "You were Captured...";
        console2.text = "";

        spectrescript.gamehasended = true;


        Invoke("restartactive", restartdelay);
        spectrescript.enabled = false;

    }



    void healthout()
    {
        if (ph.health <= 15 && pm.canMove == true)
        {
            console2.text = "Health is low";


        }
        if (ph.health <= 0)
        {
            timeshouldupdate = false;

            Scoreboard();
            console2.text = "";
            if (doneEffect1 == false)
            {
                Invoke("Vriwondies", restartdelay - 5);

            }
            pm.canMove = false;
            //Death Animation
            Invoke("restartactive", restartdelay);
            spectrescript.enabled = false;
        }

    }

    void restartactive()
    {
        GameState.SavePlayer();

        restart.SetActive(true);

    }
    public void GoStage1Scene()
    {
        SceneManager.LoadScene("Survival");

    }
    public void restartstage2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    void Monsterkilled()
    {
        monsterskilled += 1;
        isKilled = false;
    }

    void UpdateDistance()
    {
        distancefromspectre = (player.transform.position.y - spectre.transform.position.y);
        int distancefromspectreint = Mathf.Abs((int)distancefromspectre);

        distancefromspectretext.text = "Distance From Spectre: " + distancefromspectreint.ToString();

    }

    IEnumerator TimeUpdate()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondslived += 1;


        //spectrescript.speed += glacialboost;
        timertext.text = secondslived + " - Seconds Survived";

        takingAway = false;



    }
}


