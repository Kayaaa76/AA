using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Wave                                //wave class that defines the wave properties
{
    public List<GameObject> enemyPrefabs;        //list of enemy prefabs
    public List<int> enemyCount;                 //list of enemy count
    public float spawnInterval = 2;              //the spawn interval
    public int maxEnemies = 20;                  //the max enemies per wave
}

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] waypoints;               //an array of waypoints
    public GameObject testEnemyPrefab;

    public Wave[] waves;                         //an array of waves
    public int timeBetweenWaves = 5;             //the time between waves

    private GameManagerBehavior gameManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;

    private GameObject newEnemy;

    // Use this for initialization
    void Start()
    {
        lastSpawnTime = Time.time;               //sets the lastSpawnTime to time.time
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    void Update()
    {
        // 1
        int currentWave = gameManager.Wave;      //currentwave is set to the gameManager.wave
        if (currentWave < waves.Length)          //if currentwave is lesser than the total waves length
        {
            // 20
            float timeInterval = Time.time - lastSpawnTime;          //time interval is set to time.time minus the last spawn time
            float spawnInterval = waves[currentWave].spawnInterval;  //the spawn interval is set to the current wave spawn interval
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                 timeInterval > spawnInterval) &&
                enemiesSpawned < waves[currentWave].maxEnemies)      //if enemies spawned is 0 and  time interval is greater than time between waves OR time interval is greater than spawn interval AND enemies spawned is lesser than the current wave max enemies
            {
                // 3  
                lastSpawnTime = Time.time;      //sets last spawn time to time.time
                CheckInstantiate();           
                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints; //sets the newEnemy waypoints to the waypoints in this script
                enemiesSpawned++;                                         //increase enemiesSpawned
            }
            // 4 
            if (enemiesSpawned == waves[currentWave].maxEnemies &&    //if enemies spawned is equal to the current wave max enemies and there are no more enemies
                GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                //gameManager.Wave++;                                   //go to the next wave
                enemiesSpawned = 0;                                   //reset enemiesSpawned to 0
                lastSpawnTime = Time.time;                            //set lastSpawntime to time.time

                if(gameManager.Wave + 1 == Player.tdunlockedlevels) //check if playing level is same as unlocked level
                {
                    if (Player.tdunlockedlevels < 4)            //when completing levels 1-3
                    {
                        Player.coins += 50;                     //award 50 coins for passing level (one time claim)
                        Debug.Log("You got 50 coins for winning this level!");
                    }
                    else if (Player.m3unlockedlevels < 6)       //when completing levels 4-5
                    {
                        Player.coins += 100;                    //award 100 coins for passing level (one time claim)
                        Debug.Log("You got 100 coins for winning this level!");
                    }
                    else if (Player.m3unlockedlevels < 8)       //when completing levels 6-7
                    {
                        Player.coins += 150;                    //award 150 coins for passing level (one time claim)
                        Debug.Log("You got 150 coins for winning this level!");
                    }
                    else if (Player.m3unlockedlevels < 10)      //when completing levels 8-9
                    {
                        Player.coins += 200;                    //award 200 coins for passing level (one time claim)
                        Debug.Log("You got 200 coins for winning this level!");
                    }
                    else if (Player.m3unlockedlevels < 11)      //when completing level 10
                    {
                        Player.coins += 500;                    //award 500 coins for passing level (one time claim)
                        Debug.Log("You got 500 coins for winning this level!");
                    }

                    gameManager.winLevel = true;                //level won is true
                    Player.tdunlockedlevels += 1;               //increase unlocked level
                }
            }
            // 5 
        }
        else
        {
            gameManager.gameOver = true;                                                   //else game over is true
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");         
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }
    }

    void CheckInstantiate()                                                          //function to check the instantiate
    {
        int index = Random.Range(0, waves[gameManager.Wave].enemyPrefabs.Count);     //get a random number from 0 to the enemiesPrefab count
        //Debug.Log(index);

        if (waves[gameManager.Wave].enemyCount[index] > 0)                           //if the enemy type count is greater than 0
        {
            InstantiateEnemy(index);                                                 //spawn the enemy type
        }
        else if (waves[gameManager.Wave].enemyCount[index] == 0)                     //else if the enemy type count is 0
        {
            waves[gameManager.Wave].enemyCount.RemoveAt(index);                      //remove the enemy type count
            waves[gameManager.Wave].enemyPrefabs.RemoveAt(index);                    //remove the enemy type prefab
            int newIndex = Random.Range(0, waves[gameManager.Wave].enemyPrefabs.Count);  //generate a new number from the updated enemyprefabs
            InstantiateEnemy(newIndex);  //instantiate enemy
        }
    }

    void InstantiateEnemy(int index)      //function to instantiate enemy
    {
        newEnemy = Instantiate(waves[gameManager.Wave].enemyPrefabs[index]);   //new enemy is set to the instantiated enemy
        waves[gameManager.Wave].enemyCount[index] -= 1;                        //the enemy type count is reduced by 1
        if (waves[gameManager.Wave].enemyCount[index] == 0)                    //if enemy type count is 0
        {
            waves[gameManager.Wave].enemyCount.RemoveAt(index);               //remove the enemy type count 
            waves[gameManager.Wave].enemyPrefabs.RemoveAt(index);             //remove the enemy prefab
        }
    }
}
