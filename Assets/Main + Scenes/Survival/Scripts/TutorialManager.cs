using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    TutorialManager tm;
    public GameObject[] popUps;
    public int popUpsIndex;
    private PlayerMovement ph;
    GameManager GM; 
    public GameObject infobox1;
    public GameObject infobox2;
    public GameObject infobox3;
    public GameObject enabletutorialbutton;
    public GameObject disabletutorialbutton;

    [Header("Running Once")]
    public GameObject tutorial;
    GameState gamestate;
    public int oldmoney;
    float oldmonstercount;

    void Start()
    {
        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        gamestate = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        InvokeRepeating("clearconsole", 1, 15);
        oldmoney = PlayerPrefs.GetInt("Wealth");
        oldmonstercount = GM.monsterskilled;



    }

    void clearconsole()
    {


    }

    public void enabletutorial()
    {
        popUpsIndex = 0;
        PlayerPrefs.SetInt("shallwedotutorial", 0);
        oldmoney = PlayerPrefs.GetInt("Wealth");
        oldmonstercount = GM.monsterskilled;
        GM.tutorialiscompleted = false;

    }

    public void disabletutorial()
    {
        PlayerPrefs.SetInt("shallwedotutorial", 1);
    }

    void Update()
    {

        if (gamestate.shallwedotutorial == true)
        {
            tutorial.SetActive(true);
            enabletutorialbutton.SetActive(false);
            disabletutorialbutton.SetActive(true);

        }
        else
        {
            tutorial.SetActive(false);
            enabletutorialbutton.SetActive(true);
            disabletutorialbutton.SetActive(false);
        }


        for (int i = 0; i < popUps.Length; i++)
        {
        
            if (i == popUpsIndex)
            {
                popUps[i].SetActive(true);
            }

           else if (i != popUpsIndex)
            {
               popUps[i].SetActive(false);
            }

            
        }
        if (popUpsIndex == 0)
        {
            if (ph.change != Vector3.zero)
            {
                popUpsIndex++;
            }
        }

        else if (popUpsIndex == 1)
        {
            if (ph.currentState == PlayerState.attack)
            {
                popUpsIndex++;
            }
        }

        else if (popUpsIndex == 2)
        {
            if (infobox1.activeInHierarchy == true || infobox2.activeInHierarchy == true || infobox3.activeInHierarchy == true)
            {

                popUpsIndex++;
            }
        }

        else if (popUpsIndex == 3)
        {
            float newmonstercount = GM.monsterskilled;
            if (newmonstercount != oldmonstercount)
            {

                popUpsIndex++;
            }
        }

        else if (popUpsIndex == 4)
        {
            int newmoney  = PlayerPrefs.GetInt("Wealth");
            if (newmoney != oldmoney)
            {

                popUpsIndex++;
            }
        }

      

        else if (popUpsIndex == 5)
        {
            GM.tutorialiscompleted = true;
            


        }


    }

   
        
}
