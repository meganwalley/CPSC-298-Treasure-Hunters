using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class TreasureChest : NetworkBehaviour
{
    Rigidbody2D body;
    Animator animator;
    public GameObject coinPrefab;
    public GameObject rubyPrefab;
    public GameObject textEffect; 

    public int valueGood = 1;   //treasure chest has 2 types, type 1 provide treasures after opening, type 2 contains trashes. 
    public int valueBad = -5;

    public bool isGood = true;
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
    }

    private void Update()
    {
        currTimeCount += 1; 
        if (isReleasingTreasure && currTimeCount >= releaseInterval && currReleaseCount <= releaseCount )
        {
            ReleaseTreasure();
            currReleaseCount += 1; 
            currTimeCount = 0; 
        }
    }


    [ServerCallback]
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!isActive)
        {
            return;
        }
        GameObject collidedObject = coll.gameObject;
        if (collidedObject.CompareTag("Player") && collidedObject.GetComponent<Player>().CheckCanPickUp())
        {
            isActive = false;
            DetermineChestType();
            if (isGood)
            {
                TriggerGoodEffect(collidedObject);
            }
            else
            {
                TriggerBadEffect(collidedObject); 
            }
            SoundEffect();
            AfterEffect();
        }
    }


    private void DetermineChestType()
    {
        int randInt = Random.Range(0, 2);
        if(randInt == 1)
        {
            isGood = true; 
        }
        else
        {
            isGood = false; 
        }
    }
    private void TriggerGoodEffect(GameObject collidedObject)
    {
        Player player = collidedObject.GetComponent<Player>();
        player.AddScore(valueGood);
        animator.SetBool("isOpened", true);
        animator.SetBool("isGood", true);
        isReleasingTreasure = true;
        GameObject textEffectInstante = Instantiate(textEffect, transform.position, Quaternion.identity);
        textEffectInstante.GetComponent<RisingText>().content = "+" + valueGood;
        NetworkServer.Spawn(textEffectInstante);
    }
    private void TriggerBadEffect(GameObject collidedObject)
    {
        Player player = collidedObject.GetComponent<Player>();
        player.AddScore(valueBad);
        animator.SetBool("isOpened", true);
        animator.SetBool("isGood", false);
        GameObject textEffectInstante = Instantiate(textEffect, transform.position, Quaternion.identity);
        textEffectInstante.GetComponent<RisingText>().content = "-" + valueBad;
        NetworkServer.Spawn(textEffectInstante);
    }

    private void ReleaseTreasure()
    {
        int randInt = Random.Range(1, 10);
        if (1 <= randInt && randInt <= 8)
        {
            SpawnCoin();
        }
        else
        {
            SpawnRuby();
        }
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
        NetworkServer.Spawn(coinInstante);
    }

    public void SpawnRuby()
    {
        float randFloat1 = Random.Range(0, 1);
        float randFloat2 = Random.Range(0, 3);
        Vector2 spawnPosition = transform.position;
        spawnPosition.x += randFloat1;
        spawnPosition.y += randFloat2;
        GameObject rubyInstante = Instantiate(rubyPrefab,
                spawnPosition,
                Quaternion.identity);
        NetworkServer.Spawn(rubyInstante);
    }

    [Command]
    private void SoundEffect()
    {
        audioSource.Play();
    }

    private void AfterEffect()
    {

    }
}
