using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] spawnPoints; // List of spawn points in the room.
    public GameObject[] tier1Enemies;
    public GameObject[] tier2Enemies;
    public GameObject[] tier3Enemies;
    public GameObject[] bosses;

    private EnemyManager enemyManager;

    private int remainingEnemies;

    public int currentRoom = 1; // Track room progression (linked with RoomManager)
    public float scalingFactor = 1.1f; // Health scaling factor per room

    void Start()
    {
        Debug.Log("Total spawn points: " + spawnPoints.Length); // Verifica el número de spawn points
        SpawnEnemies();
        CountEnemiesInScene();
        Debug.Log("Enemies remaining: " + remainingEnemies);
    }

    /*
    // el metodo esta para ver si han muerto todos los enemigos, necesito aun alguna manera de ver cuantos se spawnean en cada escena
    public void EnemyDefeated()
    {
        remainingEnemies--;
        if (remainingEnemies <= 0)
        {
            RoomManager.Instance.EnableTransitionPoint(); // Habilita el punto de transición
        }
        else
        {
        }
    }  */

    void SpawnEnemies()
    {
        // Determine enemy tiers based on room number.
        int tier1Chance = Mathf.Clamp(90 - (currentRoom * 2), 10, 90); // Adjusted probability over time
        int tier2Chance = Mathf.Clamp(8 + (currentRoom * 2), 5, 40);
        int tier3Chance = 100 - tier1Chance - tier2Chance;

        // remainingEnemies = x

        foreach (Transform spawnPoint in spawnPoints)


        {
            Debug.Log("Spawning at: " + spawnPoint.name);  // Verifica que está recorriendo todos los spawn points

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

        if (enemy != null)
        {
            enemy.SetEnemyManager(this);
            enemy.health *= Mathf.Pow(scalingFactor, currentRoom);
        }
    }

    public int GetRemainingEnemies()
    {
        return remainingEnemies;
    }

    public void CountEnemiesInScene()
    {
        // Cuenta todos los objetos con el componente Enemy en la escena
        int enemyCount = FindObjectsOfType<Enemy>().Length;
        int bossCount = FindObjectsOfType<Drake_Behaviour>().Length;

        // Sumar ambos conteos
        remainingEnemies = enemyCount + bossCount;
    }

    // Llamado cada vez que un enemigo es eliminado
    public void OnEnemyDefeated()
    {
        remainingEnemies--;
        Debug.Log("Enemies remaining: " + remainingEnemies);

        if (remainingEnemies <= 0)
        {
            Debug.Log("Todos los enemigos derrotados. El jugador puede avanzar.");
            //RoomManager.Instance.LoadNextRoom();
            RoomManager.Instance.EnableTransitionPoint();
        }
    }
}
