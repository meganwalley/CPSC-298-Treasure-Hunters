using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class Turtle : NetworkBehaviour
{

    Rigidbody2D body;
    Animator animator;
    public GameObject coinPrefab;
    public GameObject rubyPrefab;

    public bool isReleasingTreasure = false;
    public int currTimeCount = 0;
    public int releaseInterval = 30;
    public int currReleaseCount = 0;
    public int releaseCount = 10;

    private AudioSource audioSource;
    public bool isActive = true;


    public override void OnStartServer()
    {
        base.OnStartServer();

        body = GetComponent<Rigidbody2D>();
        body.simulated = true;

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        InvokeRepeating("ReleaseTreasure", 1, 0.5f); 
    }

    private void Update()
    {

    }



    void OnTriggerEnter2D(Collider2D coll)
    {

    }


    private void ReleaseTreasure()
    {
        SpawnCoin(); 
    }

    public void SpawnCoin()
    {
        float randFloat1 = Random.Range(0, 1);
        float randFloat2 = Random.Range(0, 3);
        Vector2 spawnPosition = transform.position;
        spawnPosition.x += randFloat1;
        spawnPosition.y += randFloat2;
        GameObject coinInstante = Instantiate(coinPrefab,
                spawnPosition,
                Quaternion.identity);
        coinInstante.GetComponent<LeftMoving>().enabled = false;
        coinInstante.GetComponent<Sinking>().enabled = true;
        NetworkServer.Spawn(coinInstante); 
    }

    private void SoundEffect()
    {
        audioSource.Play();
    }

    private void AfterEffect()
    {

    }
}
