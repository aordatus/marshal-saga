using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endless : MonoBehaviour
{

    private Transform player;

    [Header("Endless")]
    public GameObject[] routes;
    private float spawnY = 0.0f;
    public float tilelength = 23.0f;
    private float amnTilesOnScreen = 3;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        for (int i = 0; i < amnTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    private void Update()   
    {
         if(player.position.y > (spawnY - amnTilesOnScreen * tilelength))
        {
            SpawnTile();
        }
    }
    private void SpawnTile(int prefabindex = -1)
    {
        GameObject go;
        go = Instantiate(routes[UnityEngine.Random.Range(0, routes.Length)]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = transform.up * spawnY ;
        spawnY += tilelength;




    }
}
