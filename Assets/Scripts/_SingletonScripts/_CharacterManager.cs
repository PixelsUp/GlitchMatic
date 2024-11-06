using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class _CharacterManager : MonoBehaviour
{
    public Slider healthBar;
    public static _CharacterManager Instance { get; private set; }

    public float speed = 15f; // Character movement speed
    public float rollSpeed = 35f; // Speed during the roll
    public float rollRegenCooldown = 5f; // Time to regenerate one roll charge
    public float rollCooldownBetweenUses = 2.5f; // Cooldown between roll uses
    public int maxRollCharges = 2; // Maximum roll charges
    public float invincibilityDuration = 0.4f; // Duration of invincibility during roll

    public float hp = 100f; // Character's health
    public float stamina = 100f; // Character's stamina
    public float resistance = 10f; // Character's resistance

    private Vector2 movement; // Movement vector
    private Rigidbody2D rb; // Character Rigidbody2D component
    private bool isRolling = false; // Whether the character is currently rolling
    private bool isInvincible = false; // Whether the character has invincibility frames
    private int currentRollCharges; // Current roll charges
    private bool canRoll = true; // Whether the character can roll
    private bool rollOnCooldown = false; // Flag to manage cooldown between rolls
    private Animator animator;
    public bool puedeDisparar = true;
    private float tiempoEntreDisparos = 2f; // Tiempo de espera entre disparos
    private Transform aimTransform;
    private bool isDead = false; // Variable para controlar si el personaje está muerto


    [SerializeField] public GameOverManagerScript GameOverManager;
    [SerializeField] public PauseScript pauseScript;
    [SerializeField] private GameObject proyectilPrefab;
    [SerializeField] private Transform puntoDisparo;

    private bool isPaused = false; // Estado de pausa
    public AudioClip DeadMusic;

    void Awake()
    {
        // Singleton pattern to ensure one instance of the character
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentRollCharges = maxRollCharges; // Initialize roll charges
        StartCoroutine(RegenerateRollCharge()); // Start roll regeneration coroutine
        animator = GetComponent<Animator>();



    }

    void Update()
    {

        if (isDead) { return; }

        if (Input.GetKeyDown(KeyCode.Escape) && hp > 0)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        bool isMoving = movement.sqrMagnitude > 0;

        if (isMoving)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // Ejemplo de daño para probar
        if (Input.GetKeyDown(KeyCode.K)) // Presiona "K" para simular la muerte
        {
            TakeDamage(200f);
        }

        // Handle movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Click");
            StartCoroutine(DispararProyectilCoroutine());
        }

        // Check for roll input (space bar set default, should use maybe two keys to do so)
        if (Input.GetKeyDown(KeyCode.Space) && canRoll && currentRollCharges > 0 && !rollOnCooldown)
        {
            StartCoroutine(Roll());
        }
    }

    void FixedUpdate()
    {
        if (!isRolling) // Normal movement if not rolling
        {
            // Smooth the movement using Lerp
            Vector2 targetPosition = rb.position + movement * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }


    // Main rolling coroutine
    IEnumerator Roll()
    {
        isRolling = true;
        isInvincible = true;
        currentRollCharges--; // Use a roll charge

        // Roll in the current movement direction
        Vector2 rollDirection = movement.normalized;
        rb.velocity = rollDirection * rollSpeed;

        yield return new WaitForSeconds(0.3f); // Duration of the roll

        rb.velocity = Vector2.zero; // Stop the character after the roll
        isRolling = false;

        yield return new WaitForSeconds(invincibilityDuration); // Invincibility frames duration
        isInvincible = false;

        // Start the cooldown only between consecutive rolls
        StartCoroutine(RollCooldown());
    }

    // Coroutine to handle the cooldown between roll uses
    IEnumerator RollCooldown()
    {
        rollOnCooldown = true;
        yield return new WaitForSeconds(rollCooldownBetweenUses); // Cooldown between rolls
        rollOnCooldown = false;
    }

    // Coroutine to regenerate roll charges
    IEnumerator RegenerateRollCharge()
    {
        while (true)
        {
            yield return new WaitForSeconds(rollRegenCooldown); // Wait time for each charge regeneration
            if (currentRollCharges < maxRollCharges)
            {
                currentRollCharges++; // Regenerate one roll charge
            }
        }
    }

    // Método para obtener la posición del ratón en el mundo, en un plano con Z fijo
    public Vector3 GetMouseWorldPositionWithZ(float zPlane)
    {
        // Crear un rayo desde la cámara en la dirección del ratón
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Crear un plano en el cual interceptar el rayo (en Z=0 o el valor que definas)
        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, zPlane));

        float distance;
        // Determinar el punto de intersección del rayo con el plano
        if (plane.Raycast(ray, out distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);
            return worldPosition;
        }

        return Vector3.zero; // Retorna algo por defecto si no se calcula la posición
    }

    public IEnumerator DispararProyectilCoroutine()
    {
        puedeDisparar = false; // Desactivar disparo hasta que pase el cooldown

        // Instanciar el proyectil en el punto de disparo
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);

        // Obtener la posición del mouse en el mundo
        Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicionMouse.z = puntoDisparo.position.z; // Asegurar que el eje Z sea cero en un juego 2D


        // Obtener la posición del ratón en el plano Z=0
        //Vector3 mousePosition = GetMouseWorldPositionWithZ(0f);

        // Calcular la dirección y el ángulo hacia el ratón
        Vector3 direccion = (posicionMouse - puntoDisparo.position).normalized;
        float angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        // Voltear el arma en el eje X cuando el ratón está a la izquierda del personaje
        //if (posicionMouse.x < transform.position.x)
        //{
        //    // Voltear horizontalmente
        //    direccion = direccion * -1;
        //}

        proyectil.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Asignar la dirección al proyectil
        proyectil.GetComponent<Bullet_Main>().ConfigurarDireccion(direccion);

        SfxScript.TriggerSfx("SfxAttack");
        // Esperar el tiempo entre disparos
        yield return new WaitForSeconds(tiempoEntreDisparos);

        puedeDisparar = true; // Activar disparo nuevamente después del cooldown
    }

    // Example method to take damage
    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            float finalDamage = damage - resistance; // Aplicar resistencia al daño
            hp -= finalDamage;
            healthBar.value = hp / 100f; // Actualiza el slider. Se asume que la vida máxima es 100
            animator.SetTrigger("IsHurt"); // Usar un trigger en lugar de un bool
            StartCoroutine(ActivateInvincibility()); // Iniciar la corutina de invencibilidad

            // Activar animación de daño


            if (hp <= 0)
            {
                Die();
            }
            else
            {
                SfxScript.TriggerSfx("SfxHurt");
            }
        }
    }

    private IEnumerator ActivateInvincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(1f); // Duración de la invencibilidad (1 segundo)
        isInvincible = false;
    }

    // Corrutina para desactivar "isHurt" después de un breve retraso


    // Method for character death
    void Die()
    {
        Debug.Log("Character is dead!");
        isDead = true;
        speed = 0f;
        StartCoroutine(waitForDeath());

    }
    private IEnumerator waitForDeath()
    {
        SfxScript.TriggerSfx("SfxDead");
        yield return new WaitForSeconds(2f); // Duración de la invencibilidad (2 segundo)
        Time.timeScale = 0f;
        MusicScript.TriggerMusic(DeadMusic);
        GameOverManager.gameOver(); // Llama a gameOver() directamente

        DestroySingletonsOnDeath();

    }

    void DestroySingletonsOnDeath()
    {
        // Destruir instancia del RoomManager si existe
        if (RoomManager.Instance != null)
        {
            Destroy(RoomManager.Instance.gameObject);
        }

        /*
        // Destruir instancia del CameraManager si existe
        if (_CameraManager.Instance != null)
        {
            Destroy(_CameraManager.Instance.gameObject);
        }

        // Destruir instancia del MusicScript si existe
        if (MusicScript.Instance != null)
        {
            Destroy(MusicScript.Instance.gameObject);
        }

        // Destruir instancia del SfxScript si existe
        if (SfxScript.Instance != null)
        {
            Destroy(SfxScript.Instance.gameObject);
        }*/
    }

    public void Resume()
    {
        pauseScript.Resume();
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseScript.Pause();
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResetLife() //Intento de hacer para que la segunda partida empiece con hp correctas
    {
        hp = 100f;
        stamina = 100f;
        resistance = 10f;
    }
}
