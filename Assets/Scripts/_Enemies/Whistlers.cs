using UnityEngine;

public class Whistlers : Enemy
{
    private enum WhistlersState { Searching, Alerting, KeepingDistance, Escaping }
    private WhistlersState currentState = WhistlersState.Searching;

    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float escapeSpeed = 5f;
    [SerializeField] private LayerMask playerLayer;
    private Vector3 playerPosition;
    private bool isPlayerDetected = false;

    // Cone detection parameters
    [SerializeField] private float coneAngle = 45f;
    [SerializeField] private Transform detectionOrigin;

    // EnemyManager reference for remaining enemies
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
        // Perform a cone-like detection check
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        foreach (var hit in hits)
        {
            Vector3 directionToPlayer = (hit.transform.position - detectionOrigin.position).normalized;
            float angle = Vector3.Angle(detectionOrigin.forward, directionToPlayer);

            if (angle < coneAngle / 2)
            {
                isPlayerDetected = true;
                playerPosition = hit.transform.position;
                AlertOtherEnemies();
                break;
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
                enemy.SetDetectionRadiusMultiplier(2.0f);
            }
        }
    }

    void ExecuteBehaviorTree()
    {
        int remainingEnemies = enemyManager.GetRemainingEnemies();
        switch (currentState)
        {
            case WhistlersState.Searching:
                if (isPlayerDetected)
                {
                    if (remainingEnemies >= 6)
                        currentState = WhistlersState.Alerting;
                    else if (remainingEnemies <= 3)
                        currentState = WhistlersState.Escaping;
                    else
                        currentState = WhistlersState.KeepingDistance;
                }
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

    void MoveTowardsPlayer()
    {
        animator.SetBool("IsRunning", true);
        Vector3 direction = (playerPosition - transform.position).normalized;
        transform.position += direction * escapeSpeed * Time.deltaTime;

        if (enemyManager.GetRemainingEnemies() < 6)
            currentState = WhistlersState.Searching;
    }

    void KeepPrudentDistance()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
        if (distanceToPlayer < detectionRadius / 2)
        {
            Vector3 awayFromPlayer = (transform.position - playerPosition).normalized;
            transform.position += awayFromPlayer * escapeSpeed * Time.deltaTime;
        }

        if (enemyManager.GetRemainingEnemies() <= 3)
            currentState = WhistlersState.Escaping;
    }

    void EscapeFromPlayer()
    {
        animator.SetBool("IsRunning", true);
        Vector3 direction = (transform.position - playerPosition).normalized;
        transform.position += direction * escapeSpeed * Time.deltaTime;
    }


    /*
    
    // Metodo ejemplo que podriamos usar para ver el cono
    void OnDrawGizmos()
    {
        if (detectionOrigin == null) return;

        Vector3 leftBoundary = Quaternion.Euler(0, -coneAngle / 2, 0) * detectionOrigin.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, coneAngle / 2, 0) * detectionOrigin.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(detectionOrigin.position, detectionOrigin.position + leftBoundary * detectionRadius);
        Gizmos.DrawLine(detectionOrigin.position, detectionOrigin.position + rightBoundary * detectionRadius);
        Gizmos.color = new Color(1, 1, 0, 0.2f);
        Gizmos.DrawMesh(MeshGenerator.CreateCone(coneAngle, detectionRadius), detectionOrigin.position, detectionOrigin.rotation);
    }
    */
}