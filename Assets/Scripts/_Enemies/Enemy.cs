using UnityEngine;
using UnityEngine.Video;

public abstract class Enemy : MonoBehaviour
{
    public float health = 100;
    private bool mirandoDerecha = true;
    private EnemyManager enemyManager;

    private void Start()
    {
        // Busca el EnemyManager en la escena al inicio
        enemyManager = FindEnemyManager();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {
            // Notifica al EnemyManager si ha sido encontrado
            if (enemyManager != null)
            {
                enemyManager.OnEnemyDefeated();
            }

            Destroy(gameObject, 2f);
        }
    }

    private EnemyManager FindEnemyManager()
    {
        // Encuentra el primer EnemyManager en la escena
        return FindObjectOfType<EnemyManager>();
    }

    // Método que invierte la escala en X para simular un giro
    public void Girar()
    {
        mirandoDerecha = !mirandoDerecha; // Cambiar el estado de dirección

        // Obtener la escala actual del objeto
        Vector3 escala = transform.localScale;

        // Invertir la escala en X
        escala.x *= -1;

        // Aplicar la nueva escala al objeto
        transform.localScale = escala;
    }

    public void GirarHaciaObjetivo(Vector3 objetivo)
    {
        // Calcula la diferencia en la posición en el eje X entre el enemigo y el objetivo
        float direccion = objetivo.x - transform.position.x;

        // Si el objetivo está a la derecha y el enemigo está mirando a la izquierda, o viceversa, cambiar la escala
        if ((direccion > 0 && !mirandoDerecha) || (direccion < 0 && mirandoDerecha))
        {
            // Cambiar de dirección
            this.Girar();
        }
    }
}