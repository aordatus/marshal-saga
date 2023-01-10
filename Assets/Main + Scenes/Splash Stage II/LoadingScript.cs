using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScript : MonoBehaviour
{

    public GameObject extra;
    public string loadlevel;
    public float delay;
    public TextMeshProUGUI gameloading;



    IEnumerator Start()
    {
            
        InvokeRepeating("loading", 0, 4);


        yield return new WaitForSeconds(1f);
        extra.SetActive(true);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(loadlevel);



    }

    void loading()
    {
         
        gameloading.text = "Game Loading" + ".";
        Invoke("loading1", 1);


    }

    void loading1()
    {

       
        gameloading.text = "Game Loading" + "..";
        Invoke("loading2", 1);



    }

    void loading2()
    {


        gameloading.text = "Game Loading" + "...";
        Invoke("loading", 1);


    }


}
