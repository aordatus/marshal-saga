using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningEnds : MonoBehaviour
{
    public string loadlevel;

    GameState gamestate;


    void OnEnable()
    {
        SceneManager.LoadScene(loadlevel);
        
    }
}
