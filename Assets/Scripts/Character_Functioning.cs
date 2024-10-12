using System.Collections;
using UnityEngine;

public class Character_Functioning : MonoBehaviour
{
    public float speed = 5f; // Character movement speed
    public float rollSpeed = 10f; // Speed during the roll
    public float rollCooldown = 4f; // Time to regenerate one roll charge
    public int maxRollCharges = 1; // Maximum roll charges
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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentRollCharges = maxRollCharges; // Initialize roll charges
        StartCoroutine(RegenerateRollCharge()); // Start roll regeneration coroutine
    }

    void Update()
    {
        // Handle movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Check for roll input (space bar or another key)
        if (Input.GetKeyDown(KeyCode.Space) && canRoll && currentRollCharges > 0)
        {
            StartCoroutine(Roll());
        }
    }

    void FixedUpdate()
    {
        if (!isRolling) // Normal movement if not rolling
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    IEnumerator Roll()
    {
        isRolling = true;
        isInvincible = true;
        currentRollCharges--; // Use a roll charge
        canRoll = false;

        // Roll in the current movement direction
        Vector2 rollDirection = movement.normalized;
        rb.velocity = rollDirection * rollSpeed;

        yield return new WaitForSeconds(0.3f); // Duration of the roll

        rb.velocity = Vector2.zero; // Stop the character after the roll
        isRolling = false;

        yield return new WaitForSeconds(invincibilityDuration); // Invincibility frames duration
        isInvincible = false;

        yield return new WaitForSeconds(rollCooldown); // Cooldown between rolls
        canRoll = true;
    }

    // Coroutine to regenerate roll charges
    IEnumerator RegenerateRollCharge()
    {
        while (true)
        {
            yield return new WaitForSeconds(rollCooldown);
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