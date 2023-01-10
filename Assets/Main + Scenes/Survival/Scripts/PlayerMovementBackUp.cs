using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using Cinemachine;
/*
public enum PlayerState
{
    walk,
    attack,
    interact
}
*/
public class PlayerMovementBackUp : MonoBehaviour
{/*
    [Header("Player")]
    public float speed;
    public float basespeed;
    public bool canMove;
    private Rigidbody2D myRigidbody;
    private Animator animator;
    private PlayerHealth ph;

    [Header("Running")]
    public Joystick joystick;
    GameState gamestate;
    GameManager GM;
    public float restartdelay = 2;
    public PlayerState currentState;
    public Vector3 change;
    // private Transform monster;
    public GameObject Spectre;
    Spectre spec;

    [Header("Lurion")]
    public GameObject Lurion;
    public float lurionboostintensity;
    private bool boost1 = false;
    private bool boost2 = true;

    [Header("Sound")]
    //public AudioClip[] steps;
    private AudioSource ads;
    public AudioClip sword;
    public AudioClip walk;

    [Header("Effects")]
    public float ShakeDuration = 0.1f; //Time Came  ra Shake effect will last
    public float ShakeAmplitude = 1f; //Cinemachine PARAMETER
    public float ShakeFrequency = 2.0f; //Cinemachine PARAMETER
    private float ShakeElapsedTime = 0f;

    //Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    void Start()
    {
        gamestate = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        ads = GetComponent<AudioSource>();
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        canMove = true;
        //monster = GameObject.FindGameObjectsWithTag("Monster");
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spec = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Spectre>();
        ph = GetComponent<PlayerHealth>();

        basespeed = gamestate.basespeed;
        speed = basespeed;
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        if (VirtualCamera != null)
        {
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        }

    }


    void LateUpdate()
    {
        change = Vector3.zero;
        Androidmove();
        if (currentState == PlayerState.attack)
        {
            ShakeElapsedTime = ShakeDuration;

        }
        //If Cinemachine is not attached or set
        if (VirtualCamera != null || VirtualCamera != null)
        {
            if (ShakeElapsedTime > 0)
            {
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                //Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }

            else
            {
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
        //change.x = Input.GetAxisRaw("Horizontal");
        // change.y = Input.GetAxisRaw("Vertical");

        lurionboost();

        if (currentState == PlayerState.walk)
        {
            UpdateAnimationAndMove();


        }

     

        //Remove this in Android
        //  Attack();
    }

    void Androidmove()
    {


        if (joystick.Horizontal >= .2f)
        {
            change.x = joystick.Horizontal;

        }

        else if (joystick.Horizontal <= -.2f)
        {
            change.x = joystick.Horizontal;

        }

        else
        {
            change.x = 0;
            change.y = 0;


        }

        if (joystick.Vertical >= .2f)
        {
            change.y = joystick.Vertical;

        }

        else if (joystick.Vertical <= -.2f)
        {
            change.y = joystick.Vertical;

        }

    }
    
    void UpdateAnimationAndMove()
        {
        if (canMove && change != Vector3.zero)
        {
            if (!ads.isPlaying)
            {
                ads.PlayOneShot(walk);
            }



            Movecharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);



        }
        else
        {
            animator.SetBool("moving", false);
            ads.Stop();
        }
      }

        void Movecharacter()
        {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);

        }

    public void Attack()
    {
        //    if (Input.GetButtonDown("attack") && currentState != PlayerState.attack & canMove == true) 
      

        if (currentState != PlayerState.attack & canMove == true)

            {
                if (ph.lurion == false)
            {
                StartCoroutine(AttackCo());
                GM.console2.text = "";
            }

            else
            {
                GM.console2.text = "You can't attack while using Lurion.";
                UpdateAnimationAndMove();
            }

        }
        
    }
    public void ShushOn()
    {
        ads.mute = true;   

    }
    public void ShushOff()
    {
        ads.mute = false;
    }

    public void lurionboost()
    {
        if (Lurion.activeInHierarchy == true)
        {
            boost1 = true;
            if (boost1 == true && boost2 == true)
            {
                speed += lurionboostintensity;
                boost2 = false;
            }
        }

    }

   

    public void restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        BoxCollider2D cb = GetComponent<BoxCollider2D>();
        cb.isTrigger = false;
    }

    private IEnumerator AttackCo()
    {
        ads.Stop();
        ads.PlayOneShot(sword);

        //Trigger is Attack
       

        
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.4f);
        currentState = PlayerState.walk;
    }


    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LU"))
        {
            spec.speed += 1;
        }       

        
    }*/
}
