using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spectre : MonoBehaviour
{


    public float speed;
    private Vector3 change;
    public Animator animator;

    public float endgamedistance;
    private Transform spectre;
    private Transform target;

    public bool gamehasended = false;

    public bool shouldplaycomehere = false;
    public bool shouldplaycomehere2 = true;

    public AudioSource comehere;

    GameState GameState;
    GameManager GameManager;
    GameManagerSecond GameManagerSecond;
    Animator gmanimator;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spectre = GetComponent<Transform>();

        GameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Survival")
        {
            GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        }
        else if (scene.name == "Survival Second Stage")
        {

            GameManagerSecond = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerSecond>();

        }


    }

    // Update is called once per frame
    void Update()
    {
        spectremove();

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        //DEATH
        if (Vector2.Distance(transform.position, target.position) < endgamedistance)
        {
            PlayerMovement pm = target.GetComponent<PlayerMovement>();
            pm.canMove = false;

            BoxCollider2D cb = GetComponent<BoxCollider2D>();
            cb.isTrigger = true;

            endgame();

        }

        if (Vector2.Distance(transform.position, target.position) < (endgamedistance))
        {
            shouldplaycomehere = true;

            if (shouldplaycomehere == true && shouldplaycomehere2 == true)
            {
                comehere.Play();
                shouldplaycomehere2 = false;
            }
        }


    }

    void spectremove()
    {
        change = Vector2.zero;
        change.y = target.position.y - spectre.position.y;


        //change.x = Input.GetAxisRaw("Horizontal");
        //change.y = Input.GetAxisRaw("Vertical");

        if (!gamehasended && change.y > 0)
        {
            animator.SetBool("down", false);
            animator.SetBool("up", true);

            //animator.SetBool("moving", true);

        }
        else if (!gamehasended && change.y < 0)
        {
            animator.SetBool("down", true);
            animator.SetBool("up", false);

        }

        else
        {
            // animator.SetBool("moving", false);

        }
    }
    void endgame()
    {
        if (gamehasended == false)
        {
            Scene scene = SceneManager.GetActiveScene();

            if (scene.name == "Survival")
            {
                GameManager.captured();
                Invoke("restart", GameManager.restartdelay);

            }
            else if (scene.name == "Survival Second Stage")
            {

                GameManagerSecond.captured();
                Invoke("restart", GameManagerSecond.restartdelay);


            }
        }



    }

    public void restart()
    {


        BoxCollider2D cb = GetComponent<BoxCollider2D>();
        cb.isTrigger = false;


    }


}
