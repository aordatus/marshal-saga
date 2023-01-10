using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Loading Stuff")]
    public string loadlevel; //Opening 
    public string loadlevelstage2;//Stage 2 or Stage Opening if it is made
    public float loaddelay;
    bool gameopened; //Once Animation of Red Arcana
    public GameObject lockstage2; //Panel with Lock on Stage 2

    public GameObject home;
    //public GameObject campaign; FUTURE REFERENCE 
    public GameObject UIStuff; //All the stuff except logo, presents, title
    public float delay = 5; //Delay for next timeline loop to appear
    public float starttime; //Waittime for timeline to start 
    public float canvasdelay; //Delay for canvas to appear after timeline

    public PlayableDirector timeline; //Vriwonwandering

    [Header("Interactive")]
    //Fire Effect
    public GameObject fire;
    private Animator fireanim;

    //Vriwon
    public GameObject Talk;
    public float waittime;
    public GameObject[] Dlgs;
    public GameObject CampDlg;
    //public GameObject StartCampaign;

    [Header("About & Survival")]
    public GameObject about;
    public GameObject survivaltext;
    private Animator HomeAnimator; //For Screen Blackout
    public GameObject cam;
    private Animator camanimator;
    GameState gamestate;
    public TextMeshProUGUI highestscore;
    public TextMeshProUGUI highestscore2;
    public TextMeshProUGUI level;
    public TextMeshProUGUI exp;
    public TextMeshProUGUI wealth;
    public GameObject musicOn;
    public GameObject musicOff;
    public GameObject soundOn;
    public GameObject soundOff;

    private void Awake()
    {
        UIStuff.SetActive(false);
        gamestate = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        HomeAnimator = home.GetComponent<Animator>();
        
        
    }

    private void Start()
    {

        timeline = GetComponent<PlayableDirector>();
        fireanim = fire.GetComponent<Animator>();
        camanimator = cam.GetComponent<Animator>();
        camanimator.SetBool("firsttimeopen", gamestate.firsttimeopened);


        if (gamestate.firsttimeopened == false)
        {
            InvokeRepeating("playvriwon", 2, delay);
            Invoke("showcanvas", 2 + canvasdelay);

        }



        else
        {
            InvokeRepeating("playvriwon", starttime, delay);
            Invoke("showcanvas", starttime + canvasdelay);

        }
        
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("stage2locked") == 1)
        {
            lockstage2.SetActive(false);
        }

        else if (PlayerPrefs.GetInt("stage2locked") == 0)
        {
            lockstage2.SetActive(true);
        }
      
        highestscore.text = "HighScore Stage 1 : " + PlayerPrefs.GetInt("HighScore");
        wealth.text = "Wealth: " + PlayerPrefs.GetInt("Wealth");
        highestscore2.text = "HighScore Stage 2: " + PlayerPrefs.GetInt("HighScore2");
        level.text = "Level: " + PlayerPrefs.GetInt("Level");
        exp.text = "Total Exp: " + PlayerPrefs.GetInt("Exp");

        if (gamestate.musicoff == true)
        {
           
            musicOff.SetActive(true);
            musicOn.SetActive(false);

        }
        else if (gamestate.musicoff == false)
        {
            musicOff.SetActive(false);
            musicOn.SetActive(true);

        }


        if (gamestate.soundoff == true)
        {

            soundOff.SetActive(true);
            soundOn.SetActive(false);

        }
        else if (gamestate.soundoff == false)
        {
            soundOff.SetActive(false);
            soundOn.SetActive(true);

        }


    }

    IEnumerator talktimeout()
    {
        foreach (GameObject Dlg in Dlgs)
        {
            if (Dlg.activeInHierarchy == true)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    Dlg.SetActive(false);
                    yield return new WaitForSeconds(0);
                }
                // Earlier it was timer but now touch anywhere and dialogue will disappear 
                //yield return new WaitForSeconds(waittime);
              // Dlg.SetActive(false);



            }

            


        }

        if (CampDlg.activeInHierarchy == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CampDlg.SetActive(false);
                yield return new WaitForSeconds(0);
            }
            /*
            StartCampaign.SetActive(false);
            Talk.SetActive(false);

            yield return new WaitForSeconds(waittime);
            CampDlg.SetActive(false);
            Talk.SetActive(true);
            StartCampaign.SetActive(true);
            */

        }

        




    }


    public void MusicOff()
    {
        gamestate.musicoff = true;

    }

    public void MusicOn()
    {
        gamestate.musicoff = false;


    }

    public void SoundOff()
    {
        gamestate.soundoff = true;

    }

    public void SoundOn()
    {
       gamestate.soundoff = false;


    }
    public void ResetData()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("Wealth", 0);
        PlayerPrefs.SetInt("HighScore2", 0);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("Exp", 0);
        gamestate.totalexp = 0;
        PlayerPrefs.SetInt("RulthP", 0);
        PlayerPrefs.SetInt("HealP", 0);
        PlayerPrefs.SetInt("stage2locked", 0);
        
    }

    public void Aboutopen()
    {
        camanimator.SetBool("closeabout", false);

        camanimator.SetBool("openabout", true);
        UIStuff.SetActive(false);
        Invoke("Aboutopen2", 1.5f); 
    }

    void Aboutopen2()
    {
        about.SetActive(true);

    }

    public void Aboutclose()
    {
        camanimator.SetBool("closeabout", true);
        camanimator.SetBool("openabout", false);
        about.SetActive(false);
        Invoke("Aboutclose2", 1.5f);
    }

    void Aboutclose2()
    {
        UIStuff.SetActive(true);


    }

    public void Survivalopen()
    {
        camanimator.SetBool("closeright", false);

        camanimator.SetBool("openright", true);
        UIStuff.SetActive(false);
        Invoke("Survivalopen2", 1.5f);
    }

    void Survivalopen2()
    {
        survivaltext.SetActive(true);

    }

    public void Survivalclose()
    {
        camanimator.SetBool("closeright", true);
        camanimator.SetBool("openright", false);
        survivaltext.SetActive(false);
        Invoke("Survivalclose2", 1.5f);
    }

    void Survivalclose2()
    {
        UIStuff.SetActive(true);


    }

   /* public void talksleep()
    {
        Talk.SetActive(false);
        Invoke("talkwakeup", waittime);


    }

    public void talkwakeup()
    {
        Talk.SetActive(true);


    }
    */
     
    public void vriwonspeak()
    {

        int a = UnityEngine.Random.Range(0, Dlgs.Length);

        foreach (GameObject Dlg in Dlgs)
        {

            Dlg.SetActive(false);

        }

        Dlgs[a].SetActive(true);

        


    }
    void playvriwon()
    {
        timeline.Play();
        

    }

    void showcanvas()
    {
        UIStuff.SetActive(true);
    }


    public void flicker()
    {
        fireanim.speed = 2;
        Invoke("flickerdone", 1);

    }

    private void flickerdone()
    {
        fireanim.speed = 1;
        

    }

    
    /*
    public void campsoon()
    {
        CampDlg.SetActive(true);
        Talk.SetActive(false);
    }*/

    public void survival()
    {
        gamestate.firsttimeopened = false;
        HomeAnimator.SetBool("closed", true);   
        camanimator.SetBool("firsttimeopen", false);   //Camera
        Invoke("survival2", loaddelay);
    }

    private void survival2()
    {
        SceneManager.LoadScene(loadlevel);
    }


    public void survivalstage2()
    {
        gamestate.firsttimeopened = false;
        HomeAnimator.SetBool("closed", true);
        camanimator.SetBool("firsttimeopen", false);   //Camera
        Invoke("survivalstage22", loaddelay);
    }

    private void survivalstage22()
    {
        SceneManager.LoadScene(loadlevelstage2);
    }
}
