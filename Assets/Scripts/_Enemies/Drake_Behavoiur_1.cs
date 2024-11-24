using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Drake_Behaviour_1 : MonoBehaviour
{
    // Variables del dragón
    public float health = 1000f;
    public float maxHealth = 1000f;
    public float playerDistance;
    public float attackCooldown = 3f;
    private float attackTimer = 0f;
    private int countFB = 4;

    // Referencia al jugador
    private _CharacterManager protagonista;
    private Vector3 posicionProtagonista = Vector3.zero;

    // Parámetros que influyen en las utilidades
    public float criticalHealthThreshold = 20f;
    public float fireBreathRange = 25f;
    public float meleeAttackRange = 25f;
    private bool isHealing = false;
    private bool reached60 = false; // Bandera para el 60%
    private bool reached40 = false; // Bandera para el 40%
    private bool reached20 = false; // Bandera para el 20%

    public GameObject firePrefab;
    public GameObject fireballPrefab;
    public GameObject tailPrefab;

    // Puntos de generación para ataques
    public Transform fireBallSpawnPoint;
    public Transform tailSpawnPoint;
    public Transform fireBreathSpawnPoint;

    public float fireballSpeed = 10f; // Velocidad de las bolas de fuego

    void Start()
    {
        protagonista = FindObjectOfType<_CharacterManager>();
        if (protagonista == null)
        {
            Debug.LogError("No se encontró al protagonista en la escena.");
        }
    }

    void Update()
    {
        // Actualizar distancia al jugador
        playerDistance = Vector2.Distance(transform.position, protagonista.transform.position);
        attackTimer += Time.deltaTime;

        // Actualizar posiciones
        posicionProtagonista = protagonista.transform.position;

        // Evaluar y realizar la mejor acción
        UpdateFireballCount();
        EvaluateAndPerformBestAction();
    }

    private void EvaluateAndPerformBestAction()
    {
        List<DragonAction> actions = new List<DragonAction>
        {
            new DragonAction("Fire Breath", CalculateFireBreathUtility(), FireBreath),
            new DragonAction("Melee Attack", CalculateMeleeAttackUtility(), MeleeAttack),
            new DragonAction("Heal", CalculateHealUtility(), Heal),
            new DragonAction("Fire Balls", 1.0f, FireBalls) // Fireballs always usable when cooldown allows
        };

        DragonAction bestAction = null;
        float highestUtility = float.MinValue;

        foreach (DragonAction action in actions)
        {
            if (action.UtilityValue > highestUtility)
            {
                highestUtility = action.UtilityValue;
                bestAction = action;
            }
        }

        bestAction?.PerformAction();
    }

    private float CalculateFireBreathUtility()
    {
        if (attackTimer < attackCooldown || playerDistance > fireBreathRange)
            return 0f;

        return Mathf.Clamp01((fireBreathRange - playerDistance) / fireBreathRange);
    }

    private float CalculateMeleeAttackUtility()
    {
        if (attackTimer < attackCooldown || playerDistance > meleeAttackRange)
            return 0f;

        return Mathf.Clamp01((meleeAttackRange - playerDistance) / meleeAttackRange);
    }

    private float CalculateHealUtility()
    {
        if (health > criticalHealthThreshold)
            return 0f;

        return 1f - (health / criticalHealthThreshold);
    }

    private void FireBreath()
    {
        if (attackTimer >= attackCooldown)
        {
            Debug.Log("El dragón lanza un aliento de fuego!");
            StartCoroutine(FireBreathFan());
            attackTimer = 0f;
        }
    }

    private void MeleeAttack()
    {
        if (attackTimer >= attackCooldown)
        {
            Debug.Log("El dragón realiza un barrido de cola!");
            StartCoroutine(TailSwipeFan());
            attackTimer = 0f;
        }
    }

    private void Heal()
    {
        if (health < maxHealth * 0.4f && !isHealing)
        {
            Debug.Log("El dragón ha comenzado a curarse.");
            StartCoroutine(AutoHeal());
        }
    }

    private void FireBalls()
    {
        if (attackTimer >= attackCooldown)
        {
            Debug.Log("El dragón lanza bolas de fuego!");

            float[] angles = GetRandomFireballAngles(countFB);
            foreach (float angle in angles)
            {
                LaunchFireball(angle);
            }

            attackTimer = 0f;
        }
    }

    private IEnumerator FireBreathFan()
    {
        float sweepDuration = 2f;
        int numberOfSteps = 20;
        float startAngle = 180f;
        float endAngle = 90f;

        float stepDelay = sweepDuration / numberOfSteps;

        for (int i = 0; i <= numberOfSteps; i++)
        {
            float t = (float)i / numberOfSteps;
            float currentAngle = Mathf.Lerp(startAngle, endAngle, t);

            FireRayAtAngle(currentAngle, 1);
            yield return new WaitForSeconds(stepDelay);
        }
    }

    private IEnumerator TailSwipeFan()
    {
        float sweepDuration = 2f;
        int numberOfSteps = 20;
        float startAngle = 0f;
        float endAngle = 90f;

        float stepDelay = sweepDuration / numberOfSteps;

        for (int i = 0; i <= numberOfSteps; i++)
        {
            float t = (float)i / numberOfSteps;
            float currentAngle = Mathf.Lerp(startAngle, endAngle, t);

            FireRayAtAngle(currentAngle, 0);
            yield return new WaitForSeconds(stepDelay);
        }
    }

    private void LaunchFireball(float angle)
    {
        if (fireBallSpawnPoint == null || fireballPrefab == null) return;

        float angleInRadians = angle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        GameObject fireball = Instantiate(fireballPrefab, fireBallSpawnPoint.position, Quaternion.identity);

        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * fireballSpeed;
        }

        Destroy(fireball, 5f);
    }

    private void FireRayAtAngle(float angle, int type)
    {
        Transform spawnPoint = (type == 0) ? tailSpawnPoint : fireBreathSpawnPoint;
        if (spawnPoint == null) return;

        float angleInRadians = angle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        Vector2 startPosition = spawnPoint.position;

        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, fireBreathRange);
        Debug.DrawRay(startPosition, direction * fireBreathRange, Color.red, 0.1f);

        if (hit.collider != null && protagonista != null)
        {
            protagonista.TakeDamage(20);
        }

        if (type == 0) CreateTailEffect(startPosition, direction);
        else CreateFireEffect(startPosition, direction);
    }

    private void CreateFireEffect(Vector2 position, Vector2 direction)
    {
        if (firePrefab == null) return;

        GameObject fireEffect = Instantiate(firePrefab, position, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        fireEffect.transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(fireEffect, 1f);
    }

    private void CreateTailEffect(Vector2 position, Vector2 direction)
    {
        if (tailPrefab == null) return;

        GameObject tailEffect = Instantiate(tailPrefab, position, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        tailEffect.transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(tailEffect, 1f);
    }

    private IEnumerator AutoHeal()
    {
        isHealing = true;

        while (health < maxHealth * 0.4f)
        {
            health += 4f;
            health = Mathf.Min(health, maxHealth);
            Debug.Log($"El dragón se cura. Salud actual: {health}");
            yield return new WaitForSeconds(1f);
        }

        isHealing = false;
    }

    private void UpdateFireballCount()
    {
        if (health <= maxHealth * 0.6f && !reached60)
        {
            countFB += 1;
            reached60 = true;
        }

        if (health <= maxHealth * 0.4f && !reached40)
        {
            countFB += 1;
            reached40 = true;
        }

        if (health <= maxHealth * 0.2f && !reached20)
        {
            countFB += 1;
            reached20 = true;
        }
    }

    private float[] GetRandomFireballAngles(int count)
    {
        float[] possibleAngles = new float[]
        {
            30f, 60f, 90f, 120f, 150f, 210f, 240f, 270f, 300f, 330f
        };

        return possibleAngles.OrderBy(x => Random.Range(-1f, 1f)).Take(count).ToArray();
    }

    private class DragonAction
    {
        public string Name { get; }
        public float UtilityValue { get; }
        private readonly System.Action Action;

        public DragonAction(string name, float utilityValue, System.Action action)
        {
            Name = name;
            UtilityValue = utilityValue;
            Action = action;
        }

        public void PerformAction()
        {
            Action?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireBreathRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRange);
    }
}
