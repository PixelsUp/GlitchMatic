using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

        int index = SceneManager.GetActiveScene().buildIndex;
        switch (index)
        {
            case 10:
                minX = 141;
                maxX = 205;
                minY = 130;
                maxY = 175;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("131926");
                break;
            case 11:
                minX = 141;
                maxX = 205;
                minY = 122;
                maxY = 178;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("131926");
                break;
            case 12:
                minX = 141;
                maxX = 206;
                minY = 135;
                maxY = 165;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("131926");
                break;
            case 13:
                minX = 141;
                maxX = 205;
                minY = 126;
                maxY = 175;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("211826");
                break;
            case 14:
                minX = 141;
                maxX = 205;
                minY = 136;
                maxY = 174;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("211826");
                break;
            case 15:
                minX = 141;
                maxX = 205;
                minY = 118;
                maxY = 183;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("211826");
                break;
            case 16:
                minX = 141;
                maxX = 205;
                minY = 127;
                maxY = 177;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("250814");
                break;
            case 17:
                minX = 141;
                maxX = 205;
                minY = 139;
                maxY = 176;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("250814");
                break;
            case 18:
                minX = 141;
                maxX = 205;
                minY = 120;
                maxY = 180;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("250814");
                break;
            case 19:
                minX = 171;
                maxX = 175;
                minY = 139;
                maxY = 161;
                GetComponent<Camera>().orthographicSize = 40;
                GetComponent<Camera>().backgroundColor = HexToColor("131926");
                break;
            case 20:
                minX = 171;
                maxX = 175;
                minY = 139;
                maxY = 161;
                GetComponent<Camera>().orthographicSize = 40;
                GetComponent<Camera>().backgroundColor = HexToColor("211826");
                break;
            case 21:
                minX = 171;
                maxX = 175;
                minY = 139;
                maxY = 161;
                GetComponent<Camera>().orthographicSize = 40;
                GetComponent<Camera>().backgroundColor = HexToColor("250814");
                break;
            case 22:
                minX = 141;
                maxX = 205;
                minY = 130;
                maxY = 175;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("131926");
                break;
            case 23:
                minX = 141;
                maxX = 205;
                minY = 122;
                maxY = 178;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("131926");
                break;
            case 24:
                minX = 141;
                maxX = 206;
                minY = 135;
                maxY = 165;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("131926");
                break;
            case 25:
                minX = 141;
                maxX = 205;
                minY = 126;
                maxY = 175;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("211826");
                break;
            case 26:
                minX = 141;
                maxX = 205;
                minY = 136;
                maxY = 174;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("211826");
                break;
            case 27:
                minX = 141;
                maxX = 205;
                minY = 118;
                maxY = 183;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("211826");
                break;
            case 28:
                minX = 141;
                maxX = 205;
                minY = 127;
                maxY = 177;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("250814");
                break;
            case 29:
                minX = 141;
                maxX = 205;
                minY = 139;
                maxY = 176;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("250814");
                break;
            case 30:
                minX = 141;
                maxX = 205;
                minY = 120;
                maxY = 180;
                GetComponent<Camera>().orthographicSize = 23;
                GetComponent<Camera>().backgroundColor = HexToColor("250814");
                break;
            case 31:
                minX = 171;
                maxX = 175;
                minY = 139;
                maxY = 161;
                GetComponent<Camera>().orthographicSize = 40;
                GetComponent<Camera>().backgroundColor = HexToColor("131926");
                break;
            case 32:
                minX = 171;
                maxX = 175;
                minY = 139;
                maxY = 161;
                GetComponent<Camera>().orthographicSize = 40;
                GetComponent<Camera>().backgroundColor = HexToColor("211826");
                break;
            case 33:
                minX = 171;
                maxX = 175;
                minY = 139;
                maxY = 161;
                GetComponent<Camera>().orthographicSize = 40;
                GetComponent<Camera>().backgroundColor = HexToColor("250814");
                break;
            default:
                break;
        }


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

    // Función para convertir un código hexadecimal en un Color
    Color HexToColor(string hex)
    {
        // Asegúrate de que el código tenga exactamente 6 caracteres (RGB)
        if (hex.Length != 6)
        {
            Debug.LogError("Código hexadecimal no válido.");
            return Color.black;
        }

        // Convertir las partes RGB
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color32(r, g, b, 255); // RGBA con opacidad completa
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
