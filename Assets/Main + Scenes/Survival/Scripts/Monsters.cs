using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monsters : MonoBehaviour
{
    [Header("Monster Data")]
    public int monstertype;
    public ParticleSystem Type2Effect;
    
    public float speed; //Monster's Base Speed
    public float health = 10; //Monster's Base Health
    public bool canMove = true; //Monster's Status
    public float chaserange = 20; //Monster will chase the target in this range


    [Header("Running")]
    private Transform target; //Vriwon
    public bool isSlowed = false; //Vriwon Slowed 
    public float damage = 0; //Damage recieved from Vriwon
    private GameManager gm;
    private SoundManager sm;

    bool takingAway = false;

    [Header("Moving")]
    private Vector3 change;
    private Animator animator;
    private Transform monster;
    Spectre spec;
    private BoxCollider2D bc;

    private MonsterManagement monman;
    PlayerHealth ph;
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("dead", false);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        monman = GameObject.FindGameObjectWithTag("MM").GetComponent<MonsterManagement>();
        monster = GetComponent<Transform>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        bc = GetComponent<BoxCollider2D>();

    }

    void Update()
    {

        monstermove();

        deathanim();

        PlayerMovement pm = target.GetComponent<PlayerMovement>();
        
        spec = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Spectre>();
        canMove = !spec.gamehasended;

      if (monstertype == 1)
        {
            slowdown();
            move1();

        }

        else if (monstertype == 2)
        {
           attack();
            move2();

        }



    }

    void attack()
    {
        Type2Effect.Play(true);

        if (canMove == true && Vector2.Distance(transform.position, target.position) < 2 && takingAway == false)
        {
            StartCoroutine(Healthout());
        }
    }

    IEnumerator Healthout()
    {
        takingAway = true;
        yield return new WaitForSeconds(3);
        
        ph.health -= 10;


        takingAway = false;



    }


    void deathanim()
    {
        if (animator.GetBool("downright") == true || animator.GetBool("downleft") == true )
        {
            


            animator.SetBool("frontburst", true);
        }

        if (animator.GetBool("upleft") == true || animator.GetBool("upright") == true )
        {

            animator.SetBool("backburst", true);
        }
    }

    void monstermove()
    {
        change = Vector2.zero;
    
        change.x = target.position.x - monster.position.x;
        change.y = target.position.y - monster.position.y;
        
    
        if (change.y > 0 && change.x > 0)
        {

            animator.SetBool("downright", false);
            animator.SetBool("upleft", true);
            animator.SetBool("downleft", false);
            animator.SetBool("upright", false);
            //animator.SetBool("moving", true);
        }
        else if (change.y < 0 && change.x > 0)
        {

            animator.SetBool("upright", false);
            animator.SetBool("upleft", false);
            animator.SetBool("downleft", false);
            animator.SetBool("downright", true);

        }

        else if (change.y > 0 && change.x < 0)
        {

            animator.SetBool("upright", true);
            animator.SetBool("downleft", false);
            animator.SetBool("downright", false);
            animator.SetBool("upleft", false);

        }

        else if (change.y < 0 && change.x < 0)
        {

            animator.SetBool("upleft", false);
            animator.SetBool("downleft", true);
            animator.SetBool("downright", false);
            animator.SetBool("upright", false);

        }


        else
        {
            // animator.SetBool("moving", false);

        }
        

    }
    public void slowdown()
    {
        if (Vector2.Distance(transform.position, target.position) <= 6)
        {
            slowed();

        }
    }

    public void slowed()
    {
        PlayerMovement pm = target.GetComponent<PlayerMovement>();

        if (Vector2.Distance(transform.position, target.position) <= 6 && isSlowed == false)
        {
           
            pm.speed = 1;
            isSlowed = true;

        }

        if  (Vector2.Distance(transform.position, target.position) >= 6)
        {
            resetspeed();

        }

        /*if (Vector2.Distance(transform.position, target.position) > rangeslow)
        {
            return;
        }
        */
    }

    public void resetspeed()
    {
        PlayerMovement pm = target.GetComponent<PlayerMovement>();
        pm.speed = pm.basespeed;
    }

    public void kill()
    {

       
        //hurt animation
        PlayerHealth ph = target.GetComponent<PlayerHealth>();
        damage += ph.CP;

        if (damage >= health)   
        {
            Type2Effect.Stop();

            bc.isTrigger = true;
            animator.SetBool("dead", true);
            sm.audiosource.PlayOneShot(sm.clip);

            //if(animator.)

            Invoke("DestroyMonster", 0.5f);

        }
    }

    public void DestroyMonster()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Survival")
        {
            GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            monman.next = true;
            if (monstertype == 1)
            {
                resetspeed();
            }




                gm.isKilled = true;
            Destroy(this.gameObject, 1f);


        }
        else if (scene.name == "Survival Second Stage")
        {
            GameManagerSecond gm2 = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerSecond>();
            if (monstertype == 1)
            {
                resetspeed();
            }
            gm2.isKilled = true;

            Destroy(this.gameObject, 1f);


        }





    }

    void move1()
    {
        if (canMove == true && Vector2.Distance(transform.position, target.position) > 3 && Vector2.Distance(transform.position, target.position) < 20) 
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        }

    }

    void move2()
    {
        if (canMove == true && Vector2.Distance(transform.position, target.position) > 2 && Vector2.Distance(transform.position, target.position) < 10)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        }

    }
}


