using UnityEngine;
using UnityEngine.SceneManagement;

public class _FinalCamera : MonoBehaviour
{
    public static _FinalCamera Instance { get; private set; }

    public Transform player; // Player to follow
    public Vector3 offset;   // Offset to maintain a distance from the player
    public float smoothSpeed = 0.125f; // Smoothness factor for camera movement

    // Camera boundaries (level-dependent)
    public float minX, maxX, minY, maxY;

    void Awake()
    {
        // Singleton pattern to ensure one instance of the camera
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

    void OnEnable()
    {
        // Subscribe to the sceneLoaded event to find the player after each scene load
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called after a scene is fully loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the player object in the new scene (assuming it's tagged as "Player")
        player = GameObject.FindWithTag("Character")?.transform;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Get the player's current position plus the offset
        Vector3 desiredPosition = player.position + offset;

        // Clamp the camera's X and Y position to stay within the level bounds
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // Apply the clamped position with the smooth follow
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
