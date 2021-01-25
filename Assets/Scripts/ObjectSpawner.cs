using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class ObjectSpawner : NetworkManager
{

    //game 
    public bool isStarted = false; 

    //time
    float timeRemaining = 0;
    float timeCount = 0;
    public float spawnInterval = 600; //how long the spawn cycle will be

    //preset spawn points
    public List<GameObject> spawnLocations;
    public List<GameObject> upperSpawnLocations;

    //Prefab - Treasure
    public GameObject coinPrefab;
    public GameObject rubyPrefab;
    public GameObject treasureChestPrefab;
    public GameObject turtlePrefab;

    //Prefab - Enemies
    public GameObject jellyFishPrefab;
    public GameObject swordfishPrefab;
    public GameObject anchorPrefab;
    public GameObject smallSeaweedPrefab;
    public GameObject bigSeaweedPrefab; 


    //these number represents how likely these items will be spawned
    //for treasure
    public int coinSpawnWeight = 0;
    public int rubbySpawnWeight = 0;
    public int treasureChestSpawnWeight = 0;
    public int turtleSpawnWeight = 0; 
    public int emptyTreasureWeight = 0; 
    //for enemies (calculated in a different pool from the treasures); 
    public int jellyFishSpawnWeight = 0;
    public int swordfishSpawnWeight = 0;
    public int anchorSpawnWeight = 0;
    public int smallSeaweedSpawnWeight = 0;
    public int bigSeaweedSpawnWeight = 0;
    public int emptyEnemyWeight = 0;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        isStarted = true;
        GameObject player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player);

        // spawn ball if two players
        if (numPlayers >= 2)
        {
            //do nothing
        }
    }


    private void Update()
    {
        if (!isStarted)
        {
            return; 
        }
        if(timeCount >= spawnInterval)
        {
            WeightedRandomTreasureSpawn();
            WeightedRandomEnemiesSpawn();
            timeCount = 0; 
        }
        UpdateTimeCount();
    }


    private void UpdateTimeCount()
    {
        timeCount += 1; 
    }



    private void WeightedRandomTreasureSpawn()
    {
        int totalWeight = coinSpawnWeight + rubbySpawnWeight + treasureChestSpawnWeight + turtleSpawnWeight + emptyTreasureWeight; 
        int randomNum = Random.Range(0, totalWeight); 
        if(0 <= randomNum && randomNum <= coinSpawnWeight)
        {
            SpawnCoin();
        }
        else if(coinSpawnWeight < randomNum && randomNum <= (coinSpawnWeight + rubbySpawnWeight))
        {
            SpawnRubby();
        }
        else if((coinSpawnWeight + rubbySpawnWeight) < randomNum && randomNum <= (coinSpawnWeight + rubbySpawnWeight + treasureChestSpawnWeight))
        {
            SpawnTreasureChest(); 
        }
        else if((coinSpawnWeight + rubbySpawnWeight + treasureChestSpawnWeight) < randomNum && randomNum <= (coinSpawnWeight + rubbySpawnWeight + treasureChestSpawnWeight + turtleSpawnWeight))
        {
            SpawnTurtle(); 
        }
        else
        {
            //do nothing; 
        }
    }

    private void WeightedRandomEnemiesSpawn()
    {
        int totalWeight = jellyFishSpawnWeight + swordfishSpawnWeight + anchorSpawnWeight + smallSeaweedSpawnWeight + bigSeaweedSpawnWeight + emptyEnemyWeight; 
        int randomNum = Random.Range(0, totalWeight);
        if (0 <= randomNum && randomNum <= jellyFishSpawnWeight)
        {
            SpawnJellyFish();
        }
        else if (jellyFishSpawnWeight < randomNum && randomNum <= (jellyFishSpawnWeight + swordfishSpawnWeight))
        {
            SpawnSwordfish(); 
        }
        else if ((jellyFishSpawnWeight + swordfishSpawnWeight) < randomNum && randomNum <= (jellyFishSpawnWeight + swordfishSpawnWeight + anchorSpawnWeight))
        {
            SpawnAnchor();
        }
        else if ((jellyFishSpawnWeight + swordfishSpawnWeight + anchorSpawnWeight) < randomNum && randomNum <= (jellyFishSpawnWeight + swordfishSpawnWeight + anchorSpawnWeight + smallSeaweedSpawnWeight))
        {
            SpawnSmallSeaweed(); 
        }
        else if ((jellyFishSpawnWeight + swordfishSpawnWeight + anchorSpawnWeight + smallSeaweedSpawnWeight) < randomNum && randomNum <= (jellyFishSpawnWeight + swordfishSpawnWeight + anchorSpawnWeight + smallSeaweedSpawnWeight + bigSeaweedSpawnWeight))
        {
            SpawnBigSeaweed();
        }
        else
        {
            //do nothing; 
        }
    }

    public void SpawnCoin()
    {
        GameObject newObject = Instantiate(coinPrefab,
                spawnLocations[Random.Range(0, spawnLocations.Count)].transform.position,
                Quaternion.identity);
        NetworkServer.Spawn(newObject); 
    }
    public void SpawnRubby()
    {
        GameObject newObject = Instantiate(rubyPrefab,
                spawnLocations[Random.Range(0, spawnLocations.Count)].transform.position,
                Quaternion.identity);
        NetworkServer.Spawn(newObject);
    }
    public void SpawnTreasureChest()
    {
        GameObject newObject = Instantiate(treasureChestPrefab,
                spawnLocations[spawnLocations.Count-1].transform.position,
                Quaternion.identity);
        NetworkServer.Spawn(newObject);
    }
    public void SpawnTurtle()
    {
        GameObject newObject = Instantiate(turtlePrefab,
                spawnLocations[Random.Range(0, spawnLocations.Count)].transform.position,
                Quaternion.identity);
        NetworkServer.Spawn(newObject);
    }

    public void SpawnJellyFish()
    {
        GameObject newObject = Instantiate(jellyFishPrefab,
                spawnLocations[Random.Range(1, spawnLocations.Count - 1)].transform.position,
                Quaternion.identity);
        NetworkServer.Spawn(newObject);
    }
    public void SpawnSwordfish()
    {
        GameObject newObject = Instantiate(swordfishPrefab,
                spawnLocations[Random.Range(1, spawnLocations.Count - 2)].transform.position,
                Quaternion.identity);
        NetworkServer.Spawn(newObject);
    }
    public void SpawnAnchor()
    {
        GameObject newObject = Instantiate(anchorPrefab,
                upperSpawnLocations[Random.Range(1, spawnLocations.Count - 1)].transform.position,
                Quaternion.identity);
        NetworkServer.Spawn(newObject);
    }
    public void SpawnSmallSeaweed()
    {
        GameObject newObject = Instantiate(smallSeaweedPrefab,
                spawnLocations[spawnLocations.Count - 1].transform.position,
                Quaternion.identity);
        NetworkServer.Spawn(newObject);
    }
    public void SpawnBigSeaweed()
    {
        GameObject newObject = Instantiate(bigSeaweedPrefab,
                spawnLocations[spawnLocations.Count - 1].transform.position,
                Quaternion.identity);
        NetworkServer.Spawn(newObject);
    }



    public override void OnServerDisconnect(NetworkConnection conn)
    {
        
        base.OnServerDisconnect(conn);
    }
}
