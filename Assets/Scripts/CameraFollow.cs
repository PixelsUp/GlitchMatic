using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Player to follow
    public Vector3 offset;   // Offset to maintain a distance from the player
    public float smoothSpeed = 0.125f; // Smoothness factor for camera movement

    // Camera boundaries (level-dependent)
    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
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
