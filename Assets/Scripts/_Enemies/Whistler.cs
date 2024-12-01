using System.Collections;
using UnityEngine;

public class Whistler : Enemy
{
    private _CharacterManager player;
    private float originalDetectionRadius;
    private bool isAlerting = false;

    [SerializeField] private float alertDuration = 5f;
    [SerializeField] private float distanceToMaintain = 10f;

    void Start()
    {
        player = FindObjectOfType<_CharacterManager>();
        originalDetectionRadius = GetDetectionRadius();
        StartCoroutine(BehaviorTree());
    }

    IEnumerator BehaviorTree()
    {
        while (true)
        {
            if (PlayerInRange())
            {
                MaintainDistance();
                if (!isAlerting)
                {
                    StartCoroutine(AlertEnemies());
                }
            }
            else
            {
                SearchForPlayer();
            }
            yield return null;
        }
    }

    private bool PlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance <= originalDetectionRadius * 2;
    }

    private void MaintainDistance()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < distanceToMaintain)
        {
            MoveAwayFromPlayer();
        }
        else if (distance > distanceToMaintain + 2)
        {
            MoveTowardsPlayer();
        }
        GirarHaciaObjetivo(player.transform.position);
    }

    private void MoveAwayFromPlayer()
    {
        Vector3 direction = (transform.position - player.transform.position).normalized;
        transform.position += direction * Time.deltaTime * 2f;
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * Time.deltaTime * 2f;
    }

    private void SearchForPlayer()
    {
        // podemos hacer que busque al jugador si queremos
    }

    private IEnumerator AlertEnemies()
    {
        isAlerting = true;
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            enemy.SetDetectionRadiusMultiplier(2.0f);
        }

        yield return new WaitForSeconds(alertDuration);

        foreach (Enemy enemy in enemies)
        {
            enemy.SetDetectionRadiusMultiplier(0.5f);
        }

        isAlerting = false;
    }
}
