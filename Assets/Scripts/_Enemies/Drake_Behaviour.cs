using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Drake_Behaviour : MonoBehaviour
{
    // Variables del drag�n
    public float health = 1000f;
    public float maxHealth = 1000f;
    public float playerDistance;
    public float attackCooldown = 3f;
    private float attackTimer = 0f;
    private int countFB = 4;

    // Referencia al jugador
    private _CharacterManager protagonista;
    private Vector3 posicionProtagonista = Vector3.zero;

    // Par�metros que influyen en las utilidades
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



    // M�todo Start: Inicializaci�n del script
    void Start()
    {
        protagonista = FindObjectOfType<_CharacterManager>();
        posicionProtagonista = protagonista.transform.position;

        if (protagonista == null)
        {
            Debug.LogError("No se encontr� al protagonista en la escena.");
        }
    }

    void Update()
    {
        // Actualizar distancia al jugador
        playerDistance = Vector2.Distance(transform.position, posicionProtagonista);
        attackTimer += Time.deltaTime;

        // Evaluar y realizar la mejor acci�n
        UpdateFireballCount();
        EvaluateAndPerformBestAction();
    }

    // M�todo para evaluar y ejecutar la acci�n con mayor utilidad
    private void EvaluateAndPerformBestAction()
    {
        List<DragonAction> actions = new List<DragonAction>();

        // Definir las acciones con sus valores de utilidad
        actions.Add(new DragonAction("Fire Breath", CalculateFireBreathUtility(), FireBreath));
        actions.Add(new DragonAction("Melee Attack", CalculateMeleeAttackUtility(), MeleeAttack));
        actions.Add(new DragonAction("Fire Balls", CalculateHealUtility(), Heal));

        // Elegir la acci�n con el valor de utilidad m�s alto
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

        // Ejecutar la mejor acci�n
        bestAction?.PerformAction();
    }

    // C�lculos de utilidad para cada acci�n
    private float CalculateFireBreathUtility()
    {
        if (attackTimer < attackCooldown || playerDistance > fireBreathRange)
            return 0f;

        // Calcular cu�n a la izquierda est� el jugador en relaci�n al drag�n
        float horizontalOffset = transform.position.x - posicionProtagonista.x;

        // Queremos que la utilidad sea mayor cuanto m�s a la izquierda est� el jugador
        float leftBias = Mathf.Clamp01(horizontalOffset / fireBreathRange);

        // Calcular utilidad basada en distancia y posici�n a la izquierda
        float distanceUtility = Mathf.Clamp01((fireBreathRange - playerDistance) / fireBreathRange);

        // Multiplicar la utilidad de la distancia por el sesgo hacia la izquierda
        return leftBias * distanceUtility;
    }

    private float CalculateMeleeAttackUtility()
    {
        if (attackTimer < attackCooldown || playerDistance > meleeAttackRange)
            return 0f;

        // Calcular cu�n a la derecha est� el jugador en relaci�n al drag�n
        float horizontalOffset = posicionProtagonista.x - transform.position.x;

        // Queremos que la utilidad sea mayor cuanto m�s a la derecha est� el jugador
        float rightBias = Mathf.Clamp01(horizontalOffset / meleeAttackRange);

        // Calcular utilidad basada en distancia y posici�n a la derecha
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

    // Acciones del drag�n
    private void FireBreath()
    {
        if (attackTimer >= attackCooldown)
        {
            Debug.Log("El drag�n lanza un aliento de fuego en abanico!");

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
            Debug.Log("El drag�n realiza un barrido de cola!");

            // GetComponent<Animator>().SetTrigger("TailSwipe");

            // Iniciar el barrido de cola
            StartCoroutine(TailSwipeFan());

            // Reiniciar el temporizador de ataque
            attackTimer = 0f;
        }
    }

    private void Heal()
    {
        // Verificar si la salud est� por debajo del 40% del m�ximo
        if (health < maxHealth * 0.4f && !isHealing)
        {
            Debug.Log("El drag�n ha comenzado a curarse.");
            StartCoroutine(AutoHeal());
        }
    }

    private void FireBalls()
    {
        if (attackTimer >= attackCooldown)
        {
            Debug.Log("El drag�n lanza un ataque de bolas de fuego!");

            // Seleccionar 3 �ngulos de 9 posibles
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
        float sweepDuration = 2f; // Duraci�n del ataque
        int numberOfSteps = 20; // Cantidad de rayos en el abanico
        float startAngle = 180f; // �ngulo inicial 
        float endAngle = 90f; // �ngulo final

        float stepDelay = sweepDuration / numberOfSteps; // Tiempo entre cada paso

        for (int i = 0; i <= numberOfSteps; i++)
        {
            // Interpolar el �ngulo entre inicio y fin
            float t = (float)i / numberOfSteps; // Proporci�n del abanico (0 a 1)
            float currentAngle = Mathf.Lerp(startAngle, endAngle, t);

            // Disparar un rayo o crear un efecto en la direcci�n actual
            FireRayAtAngle(currentAngle, 1);

            // Esperar antes del siguiente paso del abanico
            yield return new WaitForSeconds(stepDelay);
        }
    }

    // Corrutina para realizar el ataque de cola en abanico
    private IEnumerator TailSwipeFan()
    {
        float sweepDuration = 2f; // Duraci�n del ataque
        int numberOfSteps = 20; // Cantidad de rayos en el abanico
        float startAngle = 0f; // �ngulo inicial (completamente a la derecha)
        float endAngle = 90f; // �ngulo final (frontal del drag�n)

        float stepDelay = sweepDuration / numberOfSteps; // Tiempo entre cada paso

        for (int i = 0; i <= numberOfSteps; i++)
        {
            // Interpolar el �ngulo entre inicio y fin
            float t = (float)i / numberOfSteps; // Proporci�n del abanico (0 a 1)
            float currentAngle = Mathf.Lerp(startAngle, endAngle, t);

            // Disparar un rayo o crear un efecto en la direcci�n actual
            FireRayAtAngle(currentAngle, 0);

            // Esperar antes del siguiente paso del abanico
            yield return new WaitForSeconds(stepDelay);
        }
    }

    // M�todo para lanzar una bola de fuego en una direcci�n basada en un �ngulo
    //private void LaunchFireball(float angle)
    //{
    //    // Convertir el �ngulo a una direcci�n en el espacio
    //    float angleInRadians = angle * Mathf.Deg2Rad;
    //    Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

    //    // Crear la bola de fuego en la posici�n del drag�n
    //    GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

    //    // Configurar la direcci�n y velocidad de la bola de fuego
    //    Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
    //    if (rb != null)
    //    {
    //        rb.velocity = direction.normalized * fireballSpeed;
    //    }

    //    // Configurar la rotaci�n de la bola de fuego para que apunte hacia la direcci�n
    //    fireball.transform.rotation = Quaternion.Euler(0, 0, angle);

    //    // Destruir la bola de fuego despu�s de un tiempo
    //    Destroy(fireball, 5f); // Destruir despu�s de 5 segundos
    //}


    // M�todo para disparar un rayo o crear un efecto en una direcci�n espec�fica
    private void FireRayAtAngle(float angle, int type)
    {
        // Convertir el �ngulo a radianes
        float angleInRadians = angle * Mathf.Deg2Rad;

        // Calcular la direcci�n del rayo
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        // Posici�n inicial del rayo (la boca del drag�n)
        Vector2 startPosition = (Vector2)transform.position;

        // Usar un Raycast o instanciar un efecto visual
        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, fireBreathRange);
        Debug.DrawRay(startPosition, direction * fireBreathRange, Color.red, 0.1f);

        // Si golpea algo, aplicar da�o
        if (hit.collider != null)
        {
            // Asumimos que el objeto tiene un script de salud
            var targetHealth = protagonista.hp;
            if (targetHealth != null)
            {
                protagonista.TakeDamage(20);
            }
        }

        // Crear un efecto visual en la direcci�n
        if(type == 0)
        {
            CreateTailEffect(startPosition, direction);
        }else if(type == 1)
        {
            CreateFireEffect(startPosition, direction);
        }
    }

    // M�todo para obtener N �ngulos aleatorios entre las 9 posibles
    private float[] GetRandomFireballAngles(int count)
    {
        // Definir los 9 �ngulos posibles en grados
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

        // Mezclar los �ngulos y seleccionar los primeros N
        System.Random random = new System.Random();
        float[] selectedAngles = possibleAngles.OrderBy(x => random.Next()).Take(count).ToArray();

        return selectedAngles;
    }

    // M�todo para crear un efecto visual en la direcci�n
    private void CreateFireEffect(Vector2 position, Vector2 direction)
    {
        // Suponiendo que tienes un prefab de fuego asignado
        GameObject fireEffect = Instantiate(firePrefab, position, Quaternion.identity); // Descomentar cuando tengamos el prefab de fuego

        // Rotar el efecto hacia la direcci�n del rayo
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        fireEffect.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Configurar la duraci�n del efecto (opcional)
        Destroy(fireEffect, 1f); // Destruir el efecto despu�s de 1 segundo
    }

    // M�todo para crear un efecto visual en la direcci�n
    private void CreateTailEffect(Vector2 position, Vector2 direction)
    {
        /*
        // Suponiendo que tienes un prefab de efecto de cola asignado
        GameObject tailEffect = Instantiate(tailPrefab, position, Quaternion.identity);

        // Rotar el efecto hacia la direcci�n del rayo
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        tailEffect.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Configurar la duraci�n del efecto (opcional)
        Destroy(tailEffect, 1f); // Destruir el efecto despu�s de 1 segundo
        */
    }

    // Corrutina para curar al drag�n
    private IEnumerator AutoHeal()
    {
        isHealing = true;

        while (health < maxHealth * 0.4f)
        {
            health += 4f; // Curar 4 puntos de vida por segundo
            health = Mathf.Min(health, maxHealth); // Limitar la salud al m�ximo

            Debug.Log($"El drag�n se cura. Salud actual: {health}");

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