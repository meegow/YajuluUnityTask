using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class EnemySpawnManager : MonoBehaviour
{
    private float laneLength;
    private int currentLaneIndex;
    private bool platformFullOfEnemies;
    private int enemyFlipDirection;
    private Transform enemyBotInstance;
    private List<PlatformLane> tempLanes;
    [SerializeField] private float enemyYOffset;
    [SerializeField] private float minSpacingBetweenEnemyRows;
    [SerializeField] private float maxSpacingBetweenEnemyRows;
    [SerializeField] private float minDistanceToSpawnEnemyFromPlatformEnd;
    [SerializeField] private GameObject enemyBot;
    [SerializeField] private List<PlatformLane> platform_1_lanes;
    [SerializeField] private List<PlatformLane> platform_2_lanes;

    public Transform platform_1;
    public Transform platform_2;
    private Transform platformRoot;

    void Awake()
    {
        enemyFlipDirection = 1;
        tempLanes = new List<PlatformLane>();
        laneLength = platform_1_lanes[0].laneEnd.position.z - platform_1_lanes[0].laneStart.position.z;
    }

    private void Start() 
    {
        platformRoot = platform_1;
         SpawnPlatformEnemies(platform_1_lanes);
        //platformRoot = platform_2;
        //SpawnPlatformEnemies(platform_2_lanes);
    }

    public void SpawnPlatformEnemies(List<PlatformLane> lanes)
    {
        enemyFlipDirection = 1;
        //StartCoroutine(SpawnEnemies(lanes));
        SpawnEnemies(lanes);
        enemyFlipDirection = -1;
        SpawnEnemies(lanes);
        //StartCoroutine(SpawnEnemies(lanes));
    }
    //bool setNeighbourEnemy;
//     IEnumerator SpawnEnemies(List<PlatformLane> lanes)
//     {
//         SpawnEnemyInNewRow(lanes);

//         while(!platformFullOfEnemies)
//         {
//             setNeighbourEnemy = Random.Range(1, 5) <= 3;
// yield return new WaitForSeconds(2f);
//             if(setNeighbourEnemy)
//             {
//                 currentLaneIndex = Random.Range(0, tempLanes.Count);
//                 Vector3 spawnPosition = new Vector3(
//                         tempLanes[currentLaneIndex].laneStart.position.x, 
//                         tempLanes[currentLaneIndex].laneStart.position.y + (enemyYOffset * enemyFlipDirection),
//                         enemyBotInstance.position.z);

//                 enemyBotInstance = PoolManager.Pools[Constants.BOTS_POOL].Spawn(enemyBot, spawnPosition, 
//                     Quaternion.identity);
//                 enemyBotInstance.SetParent(platformRoot);
//                 CheckEndOfPlatform(lanes);
//             }
//             else
//             {
//                 CheckEndOfPlatform(lanes);
//             }

            
//         }

//         enemyBotInstance = null;
//         platformFullOfEnemies = false;
//     }

    void SpawnEnemies(List<PlatformLane> lanes)
    {
        SpawnEnemyInNewRow(lanes);

        while(!platformFullOfEnemies)
        {
            bool setNeighbourEnemy = Random.Range(1, 5) <= 3;

            if(setNeighbourEnemy)
            {
                currentLaneIndex = Random.Range(0, tempLanes.Count);
                Vector3 spawnPosition = new Vector3(
                        tempLanes[currentLaneIndex].laneStart.position.x, 
                        tempLanes[currentLaneIndex].laneStart.position.y + (enemyYOffset * enemyFlipDirection),
                        enemyBotInstance.position.z);

                enemyBotInstance = PoolManager.Pools[Constants.BOTS_POOL].Spawn(enemyBot, spawnPosition, 
                    Quaternion.identity);
                enemyBotInstance.SetParent(platformRoot);
                CheckEndOfPlatform(lanes);
            }
            else
            {
                CheckEndOfPlatform(lanes);
            }
        }

        enemyBotInstance = null;
        platformFullOfEnemies = false;
    }

    void CheckEndOfPlatform(List<PlatformLane> lanes)
    {
        Debug.Log(enemyBotInstance.name + " position: "+Mathf.Abs(enemyBotInstance.localPosition.z));
        Debug.Log(lanes[0].laneEnd.position.z - Mathf.Abs(enemyBotInstance.position.z));
        platformFullOfEnemies = lanes[0].laneEnd.position.z - Mathf.Abs(enemyBotInstance.position.z) <= 
                    minDistanceToSpawnEnemyFromPlatformEnd;

        if(!platformFullOfEnemies)
        {
            SpawnEnemyInNewRow(lanes);
        }
    }

    void SpawnEnemyInNewRow(List<PlatformLane> lanes)
    {
        FillLanesList(lanes);

        float previousBotZPos = 0f;
        float spacingBetweenBotsInRow = 0f;
        currentLaneIndex = Random.Range(0, tempLanes.Count);

        if(enemyBotInstance != null)
        {
            previousBotZPos = enemyBotInstance.position.z;
            spacingBetweenBotsInRow = Random.Range(minSpacingBetweenEnemyRows, maxSpacingBetweenEnemyRows);
        }

        Vector3 spawnPosition = new Vector3(
                tempLanes[currentLaneIndex].laneStart.position.x,
                tempLanes[currentLaneIndex].laneStart.position.y + (enemyYOffset * enemyFlipDirection),
                tempLanes[currentLaneIndex].laneStart.position.z + previousBotZPos + spacingBetweenBotsInRow
            );

        enemyBotInstance = PoolManager.Pools[Constants.BOTS_POOL].Spawn(enemyBot, spawnPosition, 
            Quaternion.identity);
        enemyBotInstance.SetParent(platformRoot);
        //FlipEnemyInstance();
        tempLanes.RemoveAt(currentLaneIndex);
    }

    void FillLanesList(List<PlatformLane> lanes)
    {
        if(tempLanes.Count > 0)
        {
            tempLanes.Clear();
        }

        for (int i = 0; i < lanes.Count; i++)
        {
            tempLanes.Add(lanes[i]);
        }
    }

    void FlipEnemyInstance()
    {
        Vector3 localScale = enemyBotInstance.transform.localScale;
        localScale.y *= enemyFlipDirection;
    }
}