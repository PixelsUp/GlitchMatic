using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _FinalCharacterManager : MonoBehaviour
{
    public static _FinalCharacterManager Instance { get; private set; }

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
    private Vector2 smoothMovement; // Smoothed movement vector for smooth transitions
    private Rigidbody2D rb; // Character Rigidbody2D component
    private bool isRolling = false; // Whether the character is currently rolling
    private bool isInvincible = false; // Whether the character has invincibility frames
    private int currentRollCharges; // Current roll charges
    private bool canRoll = true; // Whether the character can roll
    private bool rollOnCooldown = false; // Flag to manage cooldown between rolls

    private float smoothTime = 0.05f; // Time for smoothing movement

    void Awake()
    {
        // Ensure that the instance persists between scenes
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        }
    }

    // This will be called after every scene is loaded
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void LoadCharacter()
    {
        // Check if character instance exists
        if (Instance == null)
        {
            // Instantiate character from resources
            GameObject characterPrefab = Resources.Load("Playable/Character") as GameObject;
            if (characterPrefab != null)
            {
                GameObject newCharacter = Instantiate(characterPrefab);
                DontDestroyOnLoad(newCharacter);
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentRollCharges = maxRollCharges; // Initialize roll charges
        StartCoroutine(RegenerateRollCharge()); // Start roll regeneration coroutine
    }

    void Update()
    {
        // Handle movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Normalize movement to avoid faster diagonal movement
        movement = new Vector2(moveX, moveY).normalized;

        // Check for roll input
        if (Input.GetKeyDown(KeyCode.Space) && canRoll && currentRollCharges > 0 && !rollOnCooldown)
        {
            StartCoroutine(Roll());
        }
    }

    void FixedUpdate()
    {
        // Smooth the movement over time using interpolation
        smoothMovement = Vector2.Lerp(smoothMovement, movement, smoothTime / Time.fixedDeltaTime);

        if (!isRolling) // Normal movement if not rolling
        {
            rb.MovePosition(rb.position + smoothMovement * speed * Time.fixedDeltaTime);
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

    // Example method to take damage
    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            float finalDamage = damage - resistance; // Apply resistance to damage
            hp -= finalDamage;
            if (hp <= 0)
            {
                Die();
            }
        }
    }

    // Method for character death
    void Die()
    {
        Debug.Log("Character is dead!");
        // Handle death logic here (e.g., respawn, game over)
    }
}
