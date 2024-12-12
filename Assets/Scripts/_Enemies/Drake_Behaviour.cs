using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Drake_Behaviour : MonoBehaviour
{
    // Variables del dragón
    public float health = 1000f;
    public float maxHealth = 1000f;
    public float playerDistance;
    public float attackCooldown = 3f;
    private float attackTimer = 0f;
    private int countFB = 4;
    public Transform startFireBreath;
    public Transform startTail;
    public Transform startFBAttack;
    private bool isBreathingFire = false;
    public Slider bossHealthBar;

    // Referencia al jugador
    private _CharacterManager protagonista;
    private Vector3 posicionProtagonista = Vector3.zero;

    // Parámetros que influyen en las utilidades
    public float criticalHealthThreshold = 3000f;
    public float fireBreathRange = 25f;
    public float meleeAttackRange = 25f;
    private bool isHealing = false;
    private bool reached60 = false; // Bandera para el 60%
    private bool reached40 = false; // Bandera para el 40%
    private bool reached20 = false; // Bandera para el 20%
    private bool isDead = false;

    public GameObject firePrefab;
    public GameObject fireballPrefab;
    public GameObject tailPrefab;
    private EnemyManager EnemyManager;
    protected Animator animator;

    public float fireballSpeed = 10f; // Velocidad de las bolas de fuego

    //Canciones de boss
    private bool playing = false;
    private bool up50 = true;
    public AudioClip bossUnder50;
    public AudioClip bossUp50;

    // Método Start: Inicialización del script
    void Start()
    {
        protagonista = FindObjectOfType<_CharacterManager>();
        posicionProtagonista = protagonista.transform.position;
        EnemyManager = FindEnemyManager();
        animator = GetComponent<Animator>();


        if (protagonista == null)
        {
            Debug.LogError("No se encontró al protagonista en la escena.");
        }
    }

    void Update()
    {
        // Actualizar distancia al jugador
        posicionProtagonista = protagonista.transform.position;
        playerDistance = Vector2.Distance(transform.position, posicionProtagonista);
        attackTimer += Time.deltaTime;

        if (!playing)
        {
            playing = true;
            MusicScript.TriggerMusic(bossUnder50);
        }
        else
        {
            if (health < (maxHealth / 2) && health > 0)
            {
                playing = false;
                up50 = false;
            }
            else if (up50)
            {
                up50 = false;
                MusicScript.TriggerMusic(bossUp50);
            }
        }
        // Evaluar y realizar la mejor acción
        Heal();
        UpdateFireballCount();
        EvaluateAndPerformBestAction();
    }

    // Método para evaluar y ejecutar la acción con mayor utilidad
    private void EvaluateAndPerformBestAction()
    {
        List<DragonAction> actions = new List<DragonAction>();

        // Definir las acciones con sus valores de utilidad
        actions.Add(new DragonAction("Fire Breath", CalculateFireBreathUtility(), FireBreath));
        actions.Add(new DragonAction("Melee Attack", CalculateMeleeAttackUtility(), MeleeAttack));
        actions.Add(new DragonAction("Fire Balls", CalculateFBUtility(), FireBalls));

        //Debug.Log("Utilidad Aliento: " + CalculateFireBreathUtility());
        //Debug.Log("Utilidad Melee: " + CalculateMeleeAttackUtility());
        //Debug.Log("Utilidad FireBalls: " + CalculateFBUtility());

        // Elegir la acción con el valor de utilidad más alto
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

        // Ejecutar la mejor acción
        bestAction?.PerformAction();
    }

    // Cálculos de utilidad para cada acción
    private float CalculateFireBreathUtility()
    {
        float horizontalOffset = Mathf.Clamp(transform.position.x - posicionProtagonista.x, -fireBreathRange, fireBreathRange);
        float leftBias = Mathf.Clamp01((horizontalOffset + fireBreathRange) / (2 * fireBreathRange));
        float distanceUtility = Mathf.Clamp01((fireBreathRange - playerDistance) / fireBreathRange);

        float result = leftBias * distanceUtility;

        return result;
    }

    private float CalculateMeleeAttackUtility()
    {
        // Calcular cuán a la derecha está el jugador en relación al dragón
        float horizontalOffset = Mathf.Clamp(posicionProtagonista.x - transform.position.x, -meleeAttackRange, meleeAttackRange);

        // Queremos que la utilidad sea mayor cuanto más a la derecha esté el jugador
        float rightBias = Mathf.Clamp01((horizontalOffset + meleeAttackRange) / (2 * meleeAttackRange));

        // Calcular utilidad basada en distancia y posición a la derecha
        float distanceUtility = Mathf.Clamp01((meleeAttackRange - playerDistance) / meleeAttackRange);

        // Multiplicar la utilidad de la distancia por el sesgo hacia la derecha
        return rightBias * distanceUtility;
    }

    private float CalculateFBUtility()
    {
        // Calcular utilidad basada en la distancia al jugador
        float distanceUtility = Mathf.Clamp01(playerDistance / fireBreathRange);

        // Combinamos la distancia con la necesidad de atacar basada en la salud
        float healthUtility = 1f - (health / criticalHealthThreshold);

        float result = (distanceUtility * healthUtility)*1.1f;

        // La utilidad total pondera ambos factores
        return result;
    }

    // Acciones del dragón
    private void FireBreath()
    {
        if (attackTimer >= attackCooldown)
        {
            // GetComponent<Animator>().SetTrigger("FireBreath");

            // Iniciar el barrido en abanico
            StartCoroutine(FireBreathFan());

            // Reiniciar el temporizador de ataque
            attackTimer = 0f;
        }
    }

    private void MeleeAttack()
    {
        if (attackTimer >= attackCooldown)
        {
            // GetComponent<Animator>().SetTrigger("TailSwipe");

            // Iniciar el barrido de cola
            StartCoroutine(TailSwipeFan());

            // Reiniciar el temporizador de ataque
            attackTimer = 0f;
        }
    }

    private void Heal()
    {
        // Verificar si la salud está por debajo del 40% del máximo
        if (health < maxHealth * 0.4f && !isHealing)
        {
            StartCoroutine(AutoHeal());
        }
    }

    private void FireBalls()
    {
        if (attackTimer >= attackCooldown)
        {
            // Seleccionar 3 ángulos de 9 posibles
            float[] angles = GetRandomFireballAngles(countFB);

            // Disparar bolas de fuego en las direcciones seleccionadas
            foreach (float angle in angles)
            {
                LaunchFireBall(angle);
            }

            // Reiniciar el temporizador de ataque
            attackTimer = 0f;
        }
    }

    // Corrutina para realizar el ataque en abanico
    private IEnumerator FireBreathFan()
    {
        if (isBreathingFire) yield break;
        isBreathingFire = true;




        // Crear una sola llama
        GameObject flame = Instantiate(firePrefab, startFireBreath.position, Quaternion.Euler(0, 0, -90f)); // Orientada hacia abajo
        animator.SetTrigger("IsBreathingFire");



        // Configurar el rango del barrido
        float startAngle = -90f; // Comienza hacia abajo
        float endAngle = 15f;  // Termina hacia la derecha
        int steps = 14;         // Número de pasos en el barrido
        float totalTime = 2f;   // Tiempo total para completar el barrido
        float stepTime = totalTime / steps; // Tiempo por cada paso

        for (int i = 0; i <= steps; i++)
        {
            // Calcular el ángulo actual basado en el progreso del bucle
            float t = (float)i / steps; // Progreso normalizado (de 0 a 1)
            float currentAngle = Mathf.Lerp(startAngle, endAngle, t);

            // Convertir el ángulo a una posición relativa
            float angleInRadians = currentAngle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

            // Mover la llama a la nueva posición
            flame.transform.position = (Vector2)startFireBreath.position + direction * 2f; // Distancia de 2 unidades desde el dragón
            flame.transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            // Esperar antes de mover al siguiente paso
            yield return new WaitForSeconds(stepTime);
        }

        // Destruir la llama al finalizar el barrido
        Destroy(flame);

        isBreathingFire = false; // Marca el final del ataque
    }

    // Corrutina para realizar el ataque de cola en abanico
    private IEnumerator TailSwipeFan()
    {
        // Crear una sola llama
        GameObject tail = Instantiate(tailPrefab, startTail.position, Quaternion.Euler(0, 0, -270f)); // Orientada hacia abajo
        animator.SetTrigger("IsTailAttacking");

        // Configurar el rango del barrido
        float startAngle = 0f; // Comienza hacia abajo
        float endAngle = -90f;  // Termina hacia la derecha
        int steps = 12;         // Número de pasos en el barrido
        float totalTime = 1.6f;   // Tiempo total para completar el barrido
        float stepTime = totalTime / steps; // Tiempo por cada paso

        for (int i = 0; i <= steps; i++)
        {
            // Calcular el ángulo actual basado en el progreso del bucle
            float t = (float)i / steps; // Progreso normalizado (de 0 a 1)
            float currentAngle = Mathf.Lerp(startAngle, endAngle, t);

            // Convertir el ángulo a una posición relativa
            float angleInRadians = currentAngle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

            // Mover la llama a la nueva posición
            tail.transform.position = (Vector2)startTail.position + direction * 2f; // Distancia de 2 unidades desde el dragón
            tail.transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            // Esperar antes de mover al siguiente paso
            yield return new WaitForSeconds(stepTime);
        }

        // Destruir la llama al finalizar el barrido
        Destroy(tail);
    }

    private void LaunchFireBall(float angle)
    {
        // Crear una instancia del prefab de la bola de fuego
        animator.SetTrigger("IsFireball");
        GameObject fireball = Instantiate(fireballPrefab, startFBAttack.position, Quaternion.identity);
        // Convertir el ángulo a radianes
        float angleInRadians = angle * Mathf.Deg2Rad;

        // Calcular la dirección basada en el ángulo
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;

        // Obtener el componente Rigidbody2D de la bola de fuego
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Aplicar una velocidad en la dirección especificada
            rb.velocity = direction * fireballSpeed;
        }

        // Rotar la bola de fuego para que apunte en la dirección de movimiento
        fireball.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Método para obtener N ángulos aleatorios entre las 9 posibles
    private float[] GetRandomFireballAngles(int count)
    {
        // Definir los 9 ángulos posibles en grados
        float[] possibleAngles = new float[]
        {
        181f, 
        200f,  
        220f,  
        240f, 
        260f,   
        280f, 
        300f, 
        320f, 
        340f,
        359f
        };

        // Mezclar los ángulos y seleccionar los primeros N
        System.Random random = new System.Random();
        float[] selectedAngles = possibleAngles.OrderBy(x => random.Next()).Take(count).ToArray();

        return selectedAngles;
    }

    // Corrutina para curar al dragón
    private IEnumerator AutoHeal()
    {
        isHealing = true;

        while (health < maxHealth * 0.4f)
        {
            health += 30f; // Curar 4 puntos de vida por segundo
            health = Mathf.Min(health, maxHealth); // Limitar la salud al máximo
            bossHealthBar.value = health / maxHealth;
            yield return new WaitForSeconds(1f); // Esperar 1 segundo entre curaciones
        }
        isHealing = false;
    }

    private void UpdateFireballCount()
    {
        if (health <= maxHealth * 0.6f && !reached60)
        {
            countFB += 1; // Aumentar en 1 al bajar al 60%
            reached60 = true;
        }

        if (health <= maxHealth * 0.4f && !reached40)
        {
            countFB += 1; // Aumentar en 1 al bajar al 40%
            reached40 = true;
        }

        if (health <= maxHealth * 0.2f && !reached20)
        {
            countFB += 1; // Aumentar en 1 al bajar al 20%
            reached20 = true;
        }
    }

    // dibuja los gizmos para ver los rangos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireBreathRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRange);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        bossHealthBar.value = health / maxHealth;
        Debug.Log("Health: " + health);
        if (health <= 0)
        {
            // Notifica al EnemyManager si ha sido encontrado
            if (EnemyManager != null)
            {
                if (isDead == false)
                {
                    protagonista.Health(true);
                    EnemyManager.OnEnemyDefeated();
                    isDead = true;
                    animator.SetTrigger("IsDead");
                    up50 = true;
                    playing = false;
                    SfxScript.TriggerSfx("SfxBossDefeated");
                    DisableCollider();
                }
            }

            Destroy(gameObject, 2f); // Destruir el objeto después de 0.65 segundos
        }
    }

    protected virtual void DisableCollider()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    private EnemyManager FindEnemyManager()
    {
        // Encuentra el primer EnemyManager en la escena
        return FindObjectOfType<EnemyManager>();
    }
}