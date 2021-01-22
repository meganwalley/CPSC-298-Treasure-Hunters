using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawner : MonoBehaviour
{
    //time
    float timeRemaining = 0;
    float timeCount = 0;
    public float spawnInterval = 600; //how long the spawn cycle will be

    //Prefab
    public GameObject coinPrefab;
    public GameObject rubbyPrefab;
    public GameObject treasureChestPrefab;
    public List<GameObject> spawnLocations;

    //these number represents how likely these items will be spawned
    public int coinSpawnWeight = 0;
    public int rubbySpawnWeight = 0;
    public int treasureChestSpawnWeight = 0;

    private void Update()
    {
        if(timeCount >= spawnInterval)
        {
            WeightedRandomSpawn();
            timeCount = 0; 
        }
        UpdateTimeCount();
    }


    private void UpdateTimeCount()
    {
        timeCount += 1; 
    }



    private void WeightedRandomSpawn()
    {
        int totalWeight = coinSpawnWeight + rubbySpawnWeight + treasureChestSpawnWeight;
        int randomNum = Random.Range(0, totalWeight); 
        if(0 <= randomNum && randomNum <= coinSpawnWeight)
        {
            SpawnCoin();
            Debug.Log("helo"); 
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

    }
    public void SpawnTreasureChest()
    {

    }
}
