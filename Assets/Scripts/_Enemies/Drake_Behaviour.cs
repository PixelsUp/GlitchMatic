using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

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
    private bool isBreathingFire = false;

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

    public float fireballSpeed = 10f; // Velocidad de las bolas de fuego



    // Método Start: Inicialización del script
    void Start()
    {
        protagonista = FindObjectOfType<_CharacterManager>();
        posicionProtagonista = protagonista.transform.position;

        if (protagonista == null)
        {
            Debug.LogError("No se encontró al protagonista en la escena.");
        }
    }

    void Update()
    {
        // Actualizar distancia al jugador
        playerDistance = Vector2.Distance(transform.position, posicionProtagonista);
        attackTimer += Time.deltaTime;

        // Evaluar y realizar la mejor acción
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
        actions.Add(new DragonAction("Fire Balls", CalculateHealUtility(), Heal));

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
        if (attackTimer < attackCooldown || playerDistance > fireBreathRange)
            return 0f;

        // Calcular cuán a la izquierda está el jugador en relación al dragón
        float horizontalOffset = transform.position.x - posicionProtagonista.x;

        // Queremos que la utilidad sea mayor cuanto más a la izquierda esté el jugador
        float leftBias = Mathf.Clamp01(horizontalOffset / fireBreathRange);

        // Calcular utilidad basada en distancia y posición a la izquierda
        float distanceUtility = Mathf.Clamp01((fireBreathRange - playerDistance) / fireBreathRange);

        // Multiplicar la utilidad de la distancia por el sesgo hacia la izquierda
        return leftBias * distanceUtility;
    }

    private float CalculateMeleeAttackUtility()
    {
        if (attackTimer < attackCooldown || playerDistance > meleeAttackRange)
            return 0f;

        // Calcular cuán a la derecha está el jugador en relación al dragón
        float horizontalOffset = posicionProtagonista.x - transform.position.x;

        // Queremos que la utilidad sea mayor cuanto más a la derecha esté el jugador
        float rightBias = Mathf.Clamp01(horizontalOffset / meleeAttackRange);

        // Calcular utilidad basada en distancia y posición a la derecha
        float distanceUtility = Mathf.Clamp01((meleeAttackRange - playerDistance) / meleeAttackRange);

        // Multiplicar la utilidad de la distancia por el sesgo hacia la derecha
        return rightBias * distanceUtility;
    }

    private float CalculateHealUtility()
    {
        if (health > criticalHealthThreshold)
            return 0f;

        // Mayor utilidad cuando la salud es baja
        return 1f - (health / criticalHealthThreshold);
    }

    // Acciones del dragón
    private void FireBreath()
    {
        if (attackTimer >= attackCooldown)
        {
            Debug.Log("El dragón lanza un aliento de fuego en abanico!");

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
            Debug.Log("El dragón realiza un barrido de cola!");

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
            Debug.Log("El dragón ha comenzado a curarse.");
            StartCoroutine(AutoHeal());
        }
    }

    private void FireBalls()
    {
        if (attackTimer >= attackCooldown)
        {
            Debug.Log("El dragón lanza un ataque de bolas de fuego!");

            // Seleccionar 3 ángulos de 9 posibles
            float[] angles = GetRandomFireballAngles(countFB);

            // Disparar bolas de fuego en las direcciones seleccionadas
            foreach (float angle in angles)
            {
                //LaunchFireball(angle);
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

        Debug.Log("Ataque de Aliento");
        // Crear una sola llama
        GameObject flame = Instantiate(firePrefab, startFireBreath.position, Quaternion.Euler(0, 0, -270f)); // Orientada hacia abajo

        // Configurar el rango del barrido
        float startAngle = -90f; // Comienza hacia abajo
        float endAngle = 0f;  // Termina hacia la derecha
        int steps = 10;         // Número de pasos en el barrido
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
        Debug.Log("Ataque de cola");
        // Crear una sola llama
        GameObject tail = Instantiate(tailPrefab, startTail.position, Quaternion.Euler(0, 0, 270f)); // Orientada hacia abajo

        // Configurar el rango del barrido
        float startAngle = 90f; // Comienza hacia abajo
        float endAngle = 0f;  // Termina hacia la derecha
        int steps = 10;         // Número de pasos en el barrido
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
            tail.transform.position = (Vector2)startTail.position + direction * 2f; // Distancia de 2 unidades desde el dragón
            tail.transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            // Esperar antes de mover al siguiente paso
            yield return new WaitForSeconds(stepTime);
        }

        // Destruir la llama al finalizar el barrido
        Destroy(tail);
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
            health += 4f; // Curar 4 puntos de vida por segundo
            health = Mathf.Min(health, maxHealth); // Limitar la salud al máximo

            Debug.Log($"El dragón se cura. Salud actual: {health}");

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
}