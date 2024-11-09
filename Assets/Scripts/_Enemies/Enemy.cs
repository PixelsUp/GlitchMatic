using UnityEngine;
using UnityEngine.Video;
using System.Collections;


public abstract class Enemy : MonoBehaviour
{
    public float health = 100;
    private bool mirandoDerecha = true;
    private EnemyManager enemyManager;
    protected Animator animator;
    private bool isInvincible = false;


    private void Start()
    {
        // Busca el EnemyManager en la escena al inicio
        animator = GetComponent<Animator>();
        enemyManager = FindEnemyManager();
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            return; // Si es invencible, no recibe da�o
        }

        health -= damage;

        // Activar la animaci�n de da�o solo si el animator est� asignado
        if (animator != null && health > 0)
        {
            animator.SetTrigger("IsHurt");
        }

        if (health <= 0)
        {
            // Activar la animaci�n de muerte antes de destruir el objeto
            if (animator != null)
            {
                animator.SetTrigger("IsDead");
            }

            // Notifica al EnemyManager si ha sido encontrado
            if (enemyManager != null)
            {
                enemyManager.OnEnemyDefeated();
            }

            Destroy(gameObject, 0.65f); // Destruir el objeto despu�s de 0.65 segundos
        }
        else
        {
            // Inicia la invencibilidad temporal
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    // Coroutine para el tiempo de invencibilidad
    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true; // Activa la invencibilidad
        yield return new WaitForSeconds(0.2f); // Espera 0,2 segundos
        isInvincible = false; // Desactiva la invencibilidad
    }

    private EnemyManager FindEnemyManager()
    {
        // Encuentra el primer EnemyManager en la escena
        return FindObjectOfType<EnemyManager>();
    }

    // M�todo que invierte la escala en X para simular un giro
    public void Girar()
    {
        mirandoDerecha = !mirandoDerecha; // Cambiar el estado de direcci�n

        // Obtener la escala actual del objeto
        Vector3 escala = transform.localScale;

        // Invertir la escala en X
        escala.x *= -1;

        // Aplicar la nueva escala al objeto
        transform.localScale = escala;
    }

    public void GirarHaciaObjetivo(Vector3 objetivo)
    {
        // Calcula la diferencia en la posici�n en el eje X entre el enemigo y el objetivo
        float direccion = objetivo.x - transform.position.x;

        // Si el objetivo est� a la derecha y el enemigo est� mirando a la izquierda, o viceversa, cambiar la escala
        if ((direccion > 0 && !mirandoDerecha) || (direccion < 0 && mirandoDerecha))
        {
            // Cambiar de direcci�n
            this.Girar();
        }
    }
}