using System.Collections;
using UnityEngine;

public class Whistler : Enemy
{
    private _CharacterManager player;
    private float originalDetectionRadius;
    private bool isAlerting = false;
    private Rigidbody2D rb;  // Referencia al Rigidbody2D

    [SerializeField] private float alertDuration = 5f;
    [SerializeField] private float distanceToMaintain = 10f;
    [SerializeField] private float moveSpeed = 2f;

    void Start()
    {
        player = FindObjectOfType<_CharacterManager>();
        originalDetectionRadius = GetDetectionRadius();
        rb = GetComponent<Rigidbody2D>();  // Obtener el Rigidbody2D
        rb.isKinematic = true;
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
        Vector2 direction = (transform.position - player.transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    private void SearchForPlayer()
    {
        rb.velocity = Vector2.zero;
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
