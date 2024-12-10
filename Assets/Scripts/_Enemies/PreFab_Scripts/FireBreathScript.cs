using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathScript : MonoBehaviour
{
    public int damage = 20; // Daño que inflige la llama
    private _CharacterManager protagonista;
    private PolygonCollider2D polygonCollider;
    private Vector2[][] colliderPoints;



    // Método Start: Inicialización del script
    void Start()
    {
        protagonista = FindObjectOfType<_CharacterManager>();
        polygonCollider = GetComponent<PolygonCollider2D>();

        colliderPoints = new Vector2[][]
        {
            new Vector2[] {}, // Frame 0
            new Vector2[] { new Vector2(-0.06f, -0.1831361f), new Vector2(0.06301934f, -0.08201993f), new Vector2(-0.03285524f, 0.1208549f), new Vector2(-0.1955725f, -0.08890831f) }, // Frame 1
            new Vector2[] { new Vector2(0.02398551f, 0.004656598f), new Vector2(-0.5771415f, -0.5366539f), new Vector2(-1.352211f, -0.5128773f), new Vector2(-1.397825f, -0.1577924f) }, // Frame 2
            new Vector2[] { new Vector2(0.008371711f, -0.01377687f), new Vector2(-0.3819704f, -1.218604f), new Vector2(-0.9306422f, -1.387703f), new Vector2(-1.155814f, -0.9568456f) }, // Frame 3
            new Vector2[] { new Vector2(0.0005648136f, 0.01133061f), new Vector2(-0.2336401f, -1.342596f), new Vector2 (- 0.7745065f, -1.48414f), new Vector2(-0.9606425f, -0.9843994f) }, // Frame 4
            new Vector2[] { new Vector2(0.007082462f, 0.02351511f), new Vector2(0.3831006f, -1.328819f), new Vector2(-0.06408346f, -1.683903f), new Vector2(-0.4688113f, -1.328819f) }, // Frame 5
            new Vector2[] { new Vector2(0.3909076f, -1.305944f), new Vector2(-0.05408281f, -1.673238f), new Vector2(-0.4778457f, -1.325707f), new Vector2(0.03863323f, -0.1853459f) }, // Frame 6
            new Vector2[] { new Vector2(-0.06f, -0.1831361f), new Vector2(0.06301934f, -0.08201993f), new Vector2(-0.03285524f, 0.1208549f), new Vector2(-0.1955725f, -0.08890831f)}, // Frame 7
        };



        if (protagonista == null)
        {
            Debug.LogError("No se encontró al protagonista en la escena.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto que toca la llama es el jugador
        if (collision.CompareTag("Player"))
        {
           protagonista.TakeDamage(damage); // Infligir daño al jugador
        }
    }

    public void SetCollider(int index)
    {
        if (index >= 0 && index < colliderPoints.Length && polygonCollider != null)
        {
            polygonCollider.SetPath(0, colliderPoints[index]); // Establecer los puntos del collider para ese índice
        }
    }
}
