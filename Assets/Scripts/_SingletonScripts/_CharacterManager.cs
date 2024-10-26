using System.Collections;
using UnityEngine;

public class _CharacterManager : MonoBehaviour
{
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

        // Handle movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        bool isMoving = movement.sqrMagnitude > 0;
        if (isMoving)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
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
