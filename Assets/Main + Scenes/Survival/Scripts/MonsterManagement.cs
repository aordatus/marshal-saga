using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManagement : MonoBehaviour
{
    public Spectre spectre;
    public float SpawnTime;
    public Transform[] spawnPoints;
    public GameObject monster;

    
    public bool next = false;

    // Start is called before the first frame update
    void Start()
    {
        spectre = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Spectre>();
        Spawn();
        InvokeRepeating("Spawn", SpawnTime, SpawnTime);
      
    }

    private void Update()
    {
        
    }

    

    void Spawn ()
    {
        if(spectre.gamehasended == true | next == false)
        {
            return;
        }

       

        if (next == true)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(monster, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            next = false;
        }
        
    }

}
