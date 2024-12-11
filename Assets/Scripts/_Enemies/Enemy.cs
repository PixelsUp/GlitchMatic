using UnityEngine;
using UnityEngine.Video;
using System.Collections;


public abstract class Enemy : MonoBehaviour
{
    public float health = 100;
    private bool mirandoDerecha = true;
    private EnemyManager EnemyManager;
    protected Animator animator;
    private bool isInvincible = false;
    private bool isDead = false;


    [SerializeField]
    private float detectionRadius = 5f;

    // Variables para el sistema de gruñidos
    //private float gruntTimer;       // Temporizador de gruñido
    //private float gruntInterval;    // Intervalo aleatorio entre gruñidos

    private void Start()
    {
        // Busca el EnemyManager en la escena al inicio
        animator = GetComponent<Animator>();
        EnemyManager = FindEnemyManager();

        // Configura un primer intervalo aleatorio para los gruñidos
        //ResetGruntTimer();
    }
    /*
    private void Update()
    {
        
        // Actualizar el temporizador de gruñido
        Debug.Log("si");
        if (!isDead)
        {
            Debug.Log("Timer");
            gruntTimer -= Time.deltaTime;
            if (gruntTimer <= 0f)
            {
                Debug.Log("GRUNT");
                PlayGruntSound();
                ResetGruntTimer();
            }
        }
    }
*/
    /*
    private void PlayGruntSound()
    {
        // Selecciona aleatoriamente uno de los sonidos de gruñido
        string gruntSound = Random.value > 0.5f ? "SfxGrunt1" : "SfxGrunt2";
        Debug.Log("Playing grunt sound: " + gruntSound);
        SfxScript.TriggerSfx(gruntSound);
        SfxScript.TriggerSfx("SfxGrunt1");
    }

    private void ResetGruntTimer()
    {
        // Configura un intervalo aleatorio entre 3 y 8 segundos
        gruntInterval = Random.Range(3f, 8f);
        gruntTimer = gruntInterval;
    }
    */

    public void SetDetectionRadiusMultiplier(float multiplier)
    {
        detectionRadius *= multiplier;
        //Debug.Log($"{name}'s detection radius set to {detectionRadius}.");
    }

    public float GetDetectionRadius()
    {
        return detectionRadius;
    }

    public void SetEnemyManager(EnemyManager manager)
    {
        EnemyManager = manager;
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            return; // Si es invencible, no recibe daño
        }
        else
        {
            SfxScript.TriggerSfx("SfxImpactGun");
        }

        health -= damage;

        // Activar la animación de daño solo si el animator está asignado
        if (animator != null && health > 0)
        {
            animator.SetTrigger("IsHurt");
        }

        if (health <= 0)
        {
            // Notifica al EnemyManager si ha sido encontrado
            if (EnemyManager != null)
            {
                if (isDead == false)
                {
                    EnemyManager.OnEnemyDefeated();
                    isDead = true;
                }
                // Activar la animación de muerte antes de destruir el objeto
                if (animator != null)
                {
                    
                    animator.SetTrigger("IsDead");
                    //Debug.Log("Enemy defeated. Calling EnemyManager.OnEnemyDefeated.");
                }
                DisableCollider();
                //Debug.Log("Enemy defeated. Calling EnemyManager.OnEnemyDefeated.");
            }

            Destroy(gameObject, 0.65f); // Destruir el objeto después de 0.65 segundos
        }
        else
        {
            // Inicia la invencibilidad temporal
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    protected virtual void DisableCollider()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
            //Debug.Log("Collider desactivado.");
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

    private IEnumerator GruntSoundCoroutine()
    {
        while (!isDead)
        {
            // Espera un tiempo aleatorio entre 3 y 8 segundos (ajustado para pruebas)
            float waitTime = Random.Range(3f, 8f);
            Debug.Log("Waiting for " + waitTime + " seconds for grunt sound.");
            yield return new WaitForSeconds(waitTime);

            // Selecciona aleatoriamente uno de los sonidos de gruñido
            string gruntSound = Random.value > 0.5f ? "SfxGrunt1" : "SfxGrunt2";
            Debug.Log("Playing grunt sound: " + gruntSound);
            SfxScript.TriggerSfx(gruntSound);
        }
    }
}