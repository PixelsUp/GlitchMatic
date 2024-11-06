using UnityEngine;
using System.Collections;

public class _CameraManager : MonoBehaviour
{
    public static _CameraManager Instance { get; private set; }

    public Transform player; // Player to follow
    public Vector3 offset;   // Offset to maintain a distance from the player
    public float smoothSpeed = 0.125f; // Smoothness factor for camera movement

    // Camera boundaries (level-dependent)
    public float minX, maxX, minY, maxY;

    // Variables for screenshake effect
    public float shakeDuration = 0.3f; // Duration of the screenshake effect
    public float shakeMagnitude = 0.1f; // Magnitude of the shake effect

    private bool isShaking = false;


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

        // If shaking, apply random offset to the smoothed position
        if (isShaking)
        {
            smoothedPosition += (Vector3)Random.insideUnitCircle * shakeMagnitude;
        }

        // Update the camera's position
        transform.position = smoothedPosition;

        
    }

    // Method to start the screenshake effect
    public void ShakeCamera()
    {
        if (!isShaking) // Start shaking if not already shaking
        {
            StartCoroutine(ShakeCoroutine());
        }
    }

    private IEnumerator ShakeCoroutine()
    {
        isShaking = true;
        yield return new WaitForSeconds(shakeDuration);
        isShaking = false;
    }

}
