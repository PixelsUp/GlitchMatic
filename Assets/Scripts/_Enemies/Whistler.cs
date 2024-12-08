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

    //
    // Seccion de códigos de algoritmos que se pueden implementar si lo descomentamos
    //
    // Algoritmo de busqueda
    //

    /*
    // Variables principales
    private Vector2 currentTarget; // Objetivo actual de búsqueda
    private HashSet<Vector2> visitedPoints = new HashSet<Vector2>(); // Puntos ya explorados
    [SerializeField] private Collider2D searchArea; // Límite del área de búsqueda
    [SerializeField] private float pointThreshold = 0.5f; // Distancia mínima para considerar un punto alcanzado
    
    // Método principal: Busca al jugador seleccionando puntos aleatorios
    private void SearchForPlayer()
    {   
        if (currentTarget == Vector2.zero || Vector2.Distance(transform.position, currentTarget) < pointThreshold)
        {
            SelectNewSearchPoint(); // Cambia al siguiente punto si llegó al actual
        }
        else
        {
            MoveTowardsPoint(currentTarget); // Se desplaza hacia el objetivo actual
        }
    }

    // Selecciona un nuevo punto aleatorio dentro del área de búsqueda
    private void SelectNewSearchPoint()
    {
        for (int i = 0; i < 10; i++) // Hasta 10 intentos
        {
            Vector2 randomPoint = GetRandomPointInArea();
            if (!visitedPoints.Contains(randomPoint))
            {
                currentTarget = randomPoint; // Actualiza el objetivo
                visitedPoints.Add(randomPoint); // Registra el punto como visitado
    
                // Limita la memoria de puntos visitados a 10
                if (visitedPoints.Count > 10)
                {
                    visitedPoints.Remove(visitedPoints.First());
                }
                break; // Sale del bucle al encontrar un punto válido
            }
        }
    }
    
    // Genera un punto aleatorio dentro de los límites del área de búsqueda
    private Vector2 GetRandomPointInArea()
    {
        Bounds bounds = searchArea.bounds; // Obtiene los límites del área
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }
    
    // Mueve al enemigo hacia el punto objetivo
    private void MoveTowardsPoint(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized; // Dirección hacia el objetivo
        rb.velocity = direction * moveSpeed; // Actualiza la velocidad
        GirarHaciaObjetivo(target); // Orienta al enemigo hacia el punto
    }

    //
    // Visión en cono
    //

    // Distancia de vision de cono
    //[SerializeField] private float viewDistance = 10f; // Distancia máxima del cono
    //[SerializeField] private float viewAngle = 45f;   // Ángulo del cono en grados
    //[SerializeField] private LayerMask obstacleMask;  // Para detectar obstáculos que bloquean la visión

    private bool PlayerInRange()
    {
        // Verificar si el jugador está dentro de la distancia máxima
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
    
        if (distanceToPlayer > viewDistance)
        {
            return false; // Fuera de la distancia del cono
        }
    
        // Calcular el ángulo entre la dirección del Whistler y el jugador
        float angleToPlayer = Vector3.Angle(transform.right, directionToPlayer);
    
        if (angleToPlayer > viewAngle / 2)
        {
        return false; // Fuera del ángulo del cono
        }
    
        // Verificar si hay obstáculos bloqueando la visión
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, viewDistance, obstacleMask);
    
        if (hit.collider != null && hit.collider.gameObject != player.gameObject)
        {
            return false; // Algo bloquea la visión
        }
    
        return true; // El jugador está dentro del cono de visión
    }
    
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    
        Vector3 leftBoundary = Quaternion.Euler(0, 0, -viewAngle / 2) * transform.right;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, viewAngle / 2) * transform.right;
    
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewDistance);
    }*/

}
