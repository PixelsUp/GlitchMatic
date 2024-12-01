using System.Collections;
using UnityEngine;

public class Whistlers : Enemy
{
    private enum WhistlersState { Searching, Alerting, KeepingDistance, Escaping }
    private WhistlersState currentState = WhistlersState.Searching;

    [SerializeField] private float escapeSpeed = 5f;
    [SerializeField] private LayerMask playerLayer;
    private Vector3 playerPosition;
    private bool isPlayerDetected = false;

    [SerializeField] private float coneAngle = 45f;
    [SerializeField] private Transform detectionOrigin;

    private EnemyManager enemyManager;

    void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdatePlayerDetection();
        ExecuteBehaviorTree();
    }

    void UpdatePlayerDetection()
    {
        float currentDetectionRadius = GetDetectionRadius();

        Collider[] hits = Physics.OverlapSphere(transform.position, currentDetectionRadius, playerLayer);
        foreach (var hit in hits)
        {
            Vector3 directionToPlayer = (hit.transform.position - detectionOrigin.position).normalized;
            float angle = Vector3.Angle(detectionOrigin.forward, directionToPlayer);

            if (angle < coneAngle / 2)
            {
                isPlayerDetected = true;
                playerPosition = hit.transform.position;
                AlertOtherEnemies();
                return;  // Exit early if detected
            }
        }
        isPlayerDetected = false;
    }

    void AlertOtherEnemies()
    {
        Debug.Log("Player detected! Alerting all enemies.");
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            if (enemy != this)
            {
                StartCoroutine(TemporarilyIncreaseDetection(enemy, 2.0f, 10f));
            }
        }
    }

    void ExecuteBehaviorTree()
    {
        switch (currentState)
        {
            case WhistlersState.Searching:
                if (isPlayerDetected)
                {
                    int remainingEnemies = enemyManager.GetRemainingEnemies();
                    currentState = (remainingEnemies >= 6) ? WhistlersState.Alerting :
                                   (remainingEnemies <= 3) ? WhistlersState.Escaping :
                                                             WhistlersState.KeepingDistance;
                }
                Patrol();  // Add patrol behavior
                break;

            case WhistlersState.Alerting:
                MoveTowardsPlayer();
                break;

            case WhistlersState.KeepingDistance:
                KeepPrudentDistance();
                break;

            case WhistlersState.Escaping:
                EscapeFromPlayer();
                break;
        }
    }

    void Patrol()
    {
        // Simple patrol or idle behavior
        animator.SetBool("IsWalking", true);
        // Implement basic movement logic or leave idle
    }

    void MoveTowardsPlayer()
    {
        animator.SetBool("IsRunning", true);
        Vector3 direction = (playerPosition - transform.position).normalized;
        transform.position += direction * escapeSpeed * Time.deltaTime;
    }

    void KeepPrudentDistance()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
        if (distanceToPlayer < GetDetectionRadius() / 2)
        {
            Vector3 awayFromPlayer = (transform.position - playerPosition).normalized;
            transform.position += awayFromPlayer * escapeSpeed * Time.deltaTime;
        }
    }

    void EscapeFromPlayer()
    {
        animator.SetBool("IsRunning", true);
        Vector3 direction = (transform.position - playerPosition).normalized;
        transform.position += direction * escapeSpeed * Time.deltaTime;
    }

    private IEnumerator TemporarilyIncreaseDetection(Enemy enemy, float multiplier, float duration)
    {
        float originalRadius = enemy.GetDetectionRadius();
        enemy.SetDetectionRadiusMultiplier(multiplier);

        yield return new WaitForSeconds(duration);

        enemy.SetDetectionRadiusMultiplier(originalRadius / enemy.GetDetectionRadius());
    }
}
