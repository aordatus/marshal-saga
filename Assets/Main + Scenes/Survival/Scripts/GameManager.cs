using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("Bottom UI")]
    public GameObject bottomUI;
    //Monsters
    public float monsterskilled = 0;
    public TextMeshProUGUI monsterkilledtext;
    public bool isKilled = false;
    //Distanceparticle
    public float distancefromspectre;
    public TextMeshProUGUI distancefromspectretext;
    private Transform player;
    private Transform spectre;
    //TimeLeft
    public int secondsleft = 120;
    public bool takingAway = false;
    public TextMeshProUGUI timertext;
    public string loadlevel;

    //Difficulty Level
    private LevelDifficulty leveldifficulty;
    public TextMeshProUGUI difficultystage;
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
    int previouslevel;
    public TextMeshProUGUI exp;
    public TextMeshProUGUI leveladvancetext;
    private PlayerHealth ph;


    [Header("Scoreboard")]
    // public Gameobject scoreboard;
    public int scorepoints;
    bool scorefilled = false;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highestscore;
    public GameObject highestscoregameobject;


    [Header("Management")]
    public float hurryboost;
    Animator gmanimator;
    GameState GameState;
    public GameObject musicOn;
    public GameObject musicOff;
    public GameObject soundOn;
    public GameObject soundOff;
    public GameObject Stage2;
    public GameObject restart;
    public float restartdelay = 6;
    Spectre spectrescript;
    //Sound Effects
    public AudioClip VriwonDeath;
    public AudioSource VriwonDies;
    private bool doneEffect1 = false;
    public bool tutorialiscompleted = false;


    [Header("Shop")]
    public TextMeshProUGUI wealth;
    public GameObject Shop;
    Transform Eogin;
    public GameObject plus;
    public GameObject plus2;
    public GameObject minus;
    public GameObject minus2;
    public TextMeshProUGUI HPPAmount;
    public TextMeshProUGUI RPAmount;

    private void Awake()
    {
      
    }
    void Start()
    {


        previouslevel = PlayerPrefs.GetInt("Level");
        Shop.SetActive(true);

        GameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();

        spectrescript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Spectre>();
        spectre = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        Eogin = GameObject.FindGameObjectWithTag("Eogin").GetComponent<Transform>();
   
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gmanimator = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Animator>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        console.text = "";
        InvokeRepeating("consoleclearinvoke", 0f, 1f);
        //TimeLeft Bottom UI
        timertext.text = "Survive " + secondsleft + " Seconds";

        //Difficult Bottom UI
        leveldifficulty = GameObject.FindGameObjectWithTag("LU").GetComponent<LevelDifficulty>();
        difficultystage.text = "1" + " - Difficulty Stage";

        GameState.savegame = true;



    }

    void ShopWorking()
    {
        float distancefromEoginx = Mathf.Abs(player.transform.position.x - Eogin.transform.position.x);
        float distancefromEoginy = Mathf.Abs(player.transform.position.y - Eogin.transform.position.y);

        wealth.text = "Wealth: " + PlayerPrefs.GetInt("Wealth");

        if (distancefromEoginx <= 2 && distancefromEoginy <= 2)
        {
            Shop.SetActive(true);
            
        }

        if (distancefromEoginx > 2 || distancefromEoginy > 2)
        {
            Shop.SetActive(false);

        }
    }

    void shopbuttons()
    {
        


        if (PlayerPrefs.GetInt("Wealth") < 20)
        {
            plus.SetActive(false);
            plus2.SetActive(false);

        }

        else if (PlayerPrefs.GetInt("Wealth") >= 20)
        {
            plus.SetActive(true);
            plus2.SetActive(true);

        }

        if (PlayerPrefs.GetInt("HealP") == 0)
        {
            minus.SetActive(false);
        }

        else if (PlayerPrefs.GetInt("HealP") != 0)
        {
            minus.SetActive(true);
        }

        if (PlayerPrefs.GetInt("RulthP") == 0)
        {
            minus2.SetActive(false);
        }

        else if (PlayerPrefs.GetInt("RulthP") != 0)
        {
            minus2.SetActive(true);
        }

    }
    void Update()
    {
        Debug.Log("Score" + scorepoints);
        ShopWorking();
        shopbuttons();

        VriwonUI();
        BottomUI();
        healthout();
        if (distancefromspectre <= 10 && pm.canMove == true && consolechanged2 == false)
        {
            console2.text = "Hurry, it is close!";
            consolechanged = false;
            spectrescript.speed += hurryboost;


        }

        if (secondsleft <= 1)
        {

            GoStage2();
        }
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
    void GoStage2()
    {
        GameState.SavePlayer();

        Scoreboard();
      
        bottomUI.SetActive(false);
        VriwonStatus.SetActive(false);
        gmanimator.SetBool("die", true);
      

       if (distancefromspectre > 1 && ph.health > 0)
            {
                console.text = "Stage 1 Cleared";
            }

        Invoke("Stage2Active", restartdelay);
        spectrescript.enabled = false;


    }
    void Stage2Active()
    {

        Stage2.SetActive(true);
        PlayerPrefs.SetInt("stage2locked", 1);

    }

    public void GoStage2Scene()
    {
        SceneManager.LoadScene(loadlevel);

    }




    public void home()
    {
        SceneManager.LoadScene("menu");

    }


    void Scoreboard()
    {

        //Score //This is Updated from Seconds left = 0, Captured and Health
        if (scorefilled == false)
        {
            scorepoints += (int)(monsterskilled * 10);
            scorefilled = true;

        }
        

        if (scorepoints > PlayerPrefs.GetInt("HighScore"))
        {
            
            PlayerPrefs.SetInt("HighScore", scorepoints);
            score.text = "Wow! It is your Highest Score: " + scorepoints;
            highestscoregameobject.SetActive(false);

        }

        else if (PlayerPrefs.GetInt("HighScore") >= scorepoints)
        {
            score.text = "Score: " + scorepoints;
            highestscore.text = "Highest Score: " + PlayerPrefs.GetInt("HighScore");

        }

        int currentlevel = PlayerPrefs.GetInt("Level");
        if (currentlevel != previouslevel)
        {
            leveladvancetext.text = "Congratulations you leveled up!" + " Level: " + currentlevel;


        }



    }
    void VriwonUI()
    {

        if (PlayerPrefs.GetInt("HealP") >= 1 )
        {
            HPPAmount.text = PlayerPrefs.GetInt("HealP").ToString();


        }

        if (PlayerPrefs.GetInt("HealP") <= 0)
        {
            HPPAmount.text = "";


        }

        if (PlayerPrefs.GetInt("RulthP") >= 1)
        {
            RPAmount.text = PlayerPrefs.GetInt("RulthP").ToString();


        }

        if (PlayerPrefs.GetInt("RulthP") <= 0)
        {
            RPAmount.text = "";


        }



        //GoldDisplay
        gold.text = "Wealth: " + PlayerPrefs.GetInt("Wealth");

        //LevelDisplay
        level.text = "Level: " + PlayerPrefs.GetInt("Level");
        level2.text = "" + PlayerPrefs.GetInt("Level");

        exp.text = "Your Experience: " + PlayerPrefs.GetInt("Exp");
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

        //Updating Spectre
        UpdateDistance();


        //Updating Time Left Text
        if (takingAway == false && secondsleft > 0)
        {
            
            StartCoroutine(TimeUpdate());
      
        }

        //Updating Level
        if (leveldifficulty.wave >= 2)
        {
            difficultystage.text = leveldifficulty.wave + " - Difficulty Stage";


        }

        //Buttons
        if (Input.GetButtonDown("box1") && pm.canMove == true )
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
        Scoreboard();
        ph.health = 100000;
        secondsleft = 10000;

        console.text = "You were Captured...";
        console2.text = "";
        spectrescript.gamehasended = true;
        if (doneEffect1 == false)
        {
            Invoke("Vriwondies", restartdelay - 5);
        }
        Invoke("restartactive", restartdelay);

        spectrescript.enabled = false;

    }




    void healthout()
    {
        if (ph.health <= 15 && pm.canMove == true && consolechanged == false)
        {
            console2.text = "Health is low";
            consolechanged = true;
            
        }

        if (ph.health <= 0)
        {
            console2.text = "";
            secondsleft = 10000;

            Scoreboard();

            if (doneEffect1 == false)
            {
                Invoke("Vriwondies", restartdelay-5);
  

            }

            pm.canMove = false;

            Invoke("restartactive", restartdelay);
            spectrescript.enabled = false;
  

        }

    }

    void Vriwondies()
    {
        VriwonDies.PlayOneShot(VriwonDeath);

        if (tutorialiscompleted == true)
        {
            PlayerPrefs.SetInt("shallwedotutorial", 1);

        }

        doneEffect1 = true;

    }
    void restartactive()
    {
        GameState.SavePlayer();

        restart.SetActive(true);


    }

    public void restartstage1()
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
        secondsleft -= 1;

            

        timertext.text = "Survive " + secondsleft + " Seconds";
        takingAway = false;

       

    }
}
