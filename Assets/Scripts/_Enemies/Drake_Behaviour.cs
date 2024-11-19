using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drake_Behaviour : MonoBehaviour
{
    // Variables del drag�n
    public float health = 100f;
    public float playerDistance;
    public float attackCooldown = 5f;
    private float attackTimer = 0f;

    // Referencia al jugador
    private _CharacterManager protagonista;
    private Vector3 posicionProtagonista = Vector3.zero;

    // Par�metros que influyen en las utilidades
    public float criticalHealthThreshold = 20f;
    public float fireBreathRange = 5f;
    public float meleeAttackRange = 2f;

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
        EvaluateAndPerformBestAction();
    }

    // M�todo para evaluar y ejecutar la acci�n con mayor utilidad
    private void EvaluateAndPerformBestAction()
    {
        List<DragonAction> actions = new List<DragonAction>();

        // Definir las acciones con sus valores de utilidad
        actions.Add(new DragonAction("Fire Breath", CalculateFireBreathUtility(), FireBreath));
        actions.Add(new DragonAction("Melee Attack", CalculateMeleeAttackUtility(), MeleeAttack));
        actions.Add(new DragonAction("Heal", CalculateHealUtility(), Heal));

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

        // Utilidad m�xima si el jugador est� en rango de ataque cuerpo a cuerpo
        return playerDistance <= meleeAttackRange ? 1f : 0f;
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
            Debug.Log("El drag�n lanza un aliento de fuego!");
            // Aqu� ir�a la l�gica para el ataque de aliento de fuego
            attackTimer = 0f;
        }
    }

    private void MeleeAttack()
    {
        if (attackTimer >= attackCooldown)
        {
            Debug.Log("El drag�n ataca con un golpe cuerpo a cuerpo!");
            // Aqu� ir�a la l�gica para el ataque cuerpo a cuerpo
            attackTimer = 0f;
        }
    }

    private void Heal()
    {
        Debug.Log("El drag�n se cura a s� mismo!");
        // Aqu� ir�a la l�gica para curar al drag�n
        health += 10f; // Ajustar seg�n sea necesario
        if (health > 100f) health = 100f;
    }
}
