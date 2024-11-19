using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drake_Behaviour : MonoBehaviour
{
    // Variables del dragón
    public float health = 100f;
    public float playerDistance;
    public float attackCooldown = 5f;
    private float attackTimer = 0f;

    // Referencia al jugador
    private _CharacterManager protagonista;
    private Vector3 posicionProtagonista = Vector3.zero;

    // Parámetros que influyen en las utilidades
    public float criticalHealthThreshold = 20f;
    public float fireBreathRange = 5f;
    public float meleeAttackRange = 2f;

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
        EvaluateAndPerformBestAction();
    }

    // Método para evaluar y ejecutar la acción con mayor utilidad
    private void EvaluateAndPerformBestAction()
    {
        List<DragonAction> actions = new List<DragonAction>();

        // Definir las acciones con sus valores de utilidad
        actions.Add(new DragonAction("Fire Breath", CalculateFireBreathUtility(), FireBreath));
        actions.Add(new DragonAction("Melee Attack", CalculateMeleeAttackUtility(), MeleeAttack));
        actions.Add(new DragonAction("Heal", CalculateHealUtility(), Heal));

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

        // Utilidad máxima si el jugador está en rango de ataque cuerpo a cuerpo
        return playerDistance <= meleeAttackRange ? 1f : 0f;
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
            Debug.Log("El dragón lanza un aliento de fuego!");
            // Aquí iría la lógica para el ataque de aliento de fuego
            attackTimer = 0f;
        }
    }

    private void MeleeAttack()
    {
        if (attackTimer >= attackCooldown)
        {
            Debug.Log("El dragón ataca con un golpe cuerpo a cuerpo!");
            // Aquí iría la lógica para el ataque cuerpo a cuerpo
            attackTimer = 0f;
        }
    }

    private void Heal()
    {
        Debug.Log("El dragón se cura a sí mismo!");
        // Aquí iría la lógica para curar al dragón
        health += 10f; // Ajustar según sea necesario
        if (health > 100f) health = 100f;
    }
}
