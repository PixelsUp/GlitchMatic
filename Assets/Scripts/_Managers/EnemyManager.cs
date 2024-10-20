using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Transform> spawnPoints; // List of spawn points in the room.
    public GameObject[] tier1Enemies;
    public GameObject[] tier2Enemies;
    public GameObject[] tier3Enemies;
    public GameObject[] bosses;

    public int currentRoom = 1; // Track room progression (linked with RoomManager)
    public float scalingFactor = 1.1f; // Health scaling factor per room

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        // Determine enemy tiers based on room number.
        int tier1Chance = Mathf.Clamp(90 - (currentRoom * 2), 10, 90); // Adjusted probability over time
        int tier2Chance = Mathf.Clamp(8 + (currentRoom * 2), 5, 40);
        int tier3Chance = 100 - tier1Chance - tier2Chance;

        foreach (Transform spawnPoint in spawnPoints)
        {
            int roll = Random.Range(1, 101); // Random number between 1 and 100

            if (roll <= tier1Chance)
            {
                SpawnEnemy(tier1Enemies, spawnPoint);
            }
            else if (roll <= tier1Chance + tier2Chance)
            {
                SpawnEnemy(tier2Enemies, spawnPoint);
            }
            else
            {
                SpawnEnemy(tier3Enemies, spawnPoint);
            }
        }
    }

    void SpawnEnemy(GameObject[] enemyArray, Transform spawnPoint)
    {
        // Choose a random enemy prefab from the given tier
        GameObject enemyPrefab = enemyArray[Random.Range(0, enemyArray.Length)];
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Scale enemy health based on room progression
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.health *= Mathf.Pow(scalingFactor, currentRoom);
    }
}
