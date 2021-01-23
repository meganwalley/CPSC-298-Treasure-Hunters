using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //time
    float timeRemaining = 0;
    float timeCount = 0;
    public float spawnInterval = 600; //how long the spawn cycle will be

    //Prefab - Treasure
    public GameObject coinPrefab;
    public GameObject rubyPrefab;
    public GameObject treasureChestPrefab;
    public List<GameObject> spawnLocations;

    //Prefab - Enemies
    public GameObject jellyFishPrefab; 


    //these number represents how likely these items will be spawned
    //for treasure
    public int coinSpawnWeight = 0;
    public int rubbySpawnWeight = 0;
    public int treasureChestSpawnWeight = 0;
    public int emptyTreasureWeight = 0; 
    //for enemies (calculated in a different pool from the treasures); 
    public int jellyFishSpawnWeight = 0;
    public int emptyEnemyWeight = 0; 


    private void Update()
    {
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
        int totalWeight = coinSpawnWeight + rubbySpawnWeight + treasureChestSpawnWeight;
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
    }

    private void WeightedRandomEnemiesSpawn()
    {
        int totalWeight = jellyFishSpawnWeight + emptyEnemyWeight;
        int randomNum = Random.Range(0, totalWeight);
        if (0 <= randomNum && randomNum <= jellyFishSpawnWeight)
        {
            SpawnJellyFish();
        }
        //else if (jellyFishSpawnWeight < randomNum && randomNum <= (jellyFishSpawnWeight + emptyEnemyWeight))
        //{

        //}
        //else if ((coinSpawnWeight + rubbySpawnWeight) < randomNum && randomNum <= (coinSpawnWeight + rubbySpawnWeight + treasureChestSpawnWeight))
        //{
        //    SpawnTreasureChest();
        //}
        else
        {
            //do nothing; 
        }
    }

    public void SpawnCoin()
    {
        Instantiate(coinPrefab,
                spawnLocations[Random.Range(0, spawnLocations.Count)].transform.position,
                Quaternion.identity);
    }
    public void SpawnRubby()
    {
        Instantiate(rubyPrefab,
                spawnLocations[Random.Range(0, spawnLocations.Count)].transform.position,
                Quaternion.identity);
    }
    public void SpawnTreasureChest()
    {
        Instantiate(treasureChestPrefab,
                spawnLocations[spawnLocations.Count-1].transform.position,
                Quaternion.identity);
    }

    public void SpawnJellyFish()
    {
        Instantiate(jellyFishPrefab,
                spawnLocations[Random.Range(1, spawnLocations.Count - 1)].transform.position,
                Quaternion.identity);
    }
}