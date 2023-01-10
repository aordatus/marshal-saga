using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBreak : MonoBehaviour
{
    private Animator anim;
    public ParticleSystem effect;
    GameManager GM;
    void Start()
    {
        anim = GetComponent<Animator>();
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

   
    void Update()
    {
        
    }

    public void smash()
    {
        anim.SetBool("smash", true);
        effect.Play();
        StartCoroutine(breakCo());
        PlayerHealth ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        int addcoin = UnityEngine.Random.Range(1, 3);
        int wealthpresent = PlayerPrefs.GetInt("Wealth");
        wealthpresent += addcoin;
        PlayerPrefs.SetInt("Wealth", wealthpresent);
        GM.console2.text = "Wealth  +" + addcoin;
    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(.4f);
        this.gameObject.SetActive(false);

    }

}
