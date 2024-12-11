using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathScript : MonoBehaviour
{
    public int damage = 20; // Daño que inflige la llama
    private _CharacterManager protagonista;
    private PolygonCollider2D polygonCollider;
    private Vector2[][] colliderPoints;
    private Vector2[][] colliderPoints2;




    // Método Start: Inicialización del script
    void Start()
    {
        protagonista = FindObjectOfType<_CharacterManager>();
        polygonCollider = GetComponent<PolygonCollider2D>();

        colliderPoints = new Vector2[][]
        {
            new Vector2[] {
                new Vector2(-0.1954775f, -0.1341511f),
                new Vector2(-0.1303188f, -0.325796f),
                new Vector2(-0.3692353f, -0.3513489f),
                new Vector2(-0.4054347f, -0.1469278f)
            },

            new Vector2[] {
                new Vector2(-0.1954775f, -0.1341511f),
                new Vector2(-0.1303188f, -0.325796f),
                new Vector2(-0.3692353f, -0.3513489f),
                new Vector2(-0.4054347f, -0.1469278f)
            }, // Frame 1

            new Vector2[] {
                new Vector2(-1.44944f, -0.3520615f),
                new Vector2(-0.2442876f, -0.2658422f),
                new Vector2(-1.123724f, -0.8837467f),
                new Vector2(-1.767014f, -0.6969389f)
            }, // Frame 2

            new Vector2[] {
                new Vector2(-0.1768942f, -0.4608174f),
                new Vector2(-0.5306838f, -1.620293f),
                new Vector2(-1.095061f, -1.865567f),
                new Vector2(-1.33092f, -1.449345f)
            }, // Frame 3
            new Vector2[] {
                new Vector2(-0.09265883f, -0.5574404f),
                new Vector2(-1.002403f, -1.43448f),
                new Vector2 (-0.8507776f, -2.133139f),
                new Vector2(-0.2695536f, -1.768944f)
            }, // Frame 4

            new Vector2[] {
                new Vector2(0.03149164f, -0.5279509f),
                new Vector2(-0.4275269f, -1.937496f),
                new Vector2(-0.02539934f, -2.24257f),
                new Vector2(0.3705077f, -1.981864f)
            }, // Frame 5

            new Vector2[] {
                new Vector2(0.3089511f, -1.741312f),
                new Vector2(0.06298392f, -0.4168031f),
                new Vector2(-0.4198907f, -1.639427f),
                new Vector2(-0.07348071f, -1.982132f)
            }, // Frame 6

            new Vector2[] {
                new Vector2(-0.1954775f, -0.1341511f),
                new Vector2(-0.1303188f, -0.325796f),
                new Vector2(-0.3692353f, -0.3513489f),
                new Vector2(-0.4054347f, -0.1469278f)
            }, // Frame 7
        };

        colliderPoints2 = new Vector2[][]
        {
            new Vector2[] {
    new Vector2(0.7444451f, -0.2772242f),
    new Vector2(0.5258612f, -0.4174047f),
    new Vector2(0.6202375f, -0.2149649f),
    new Vector2(0.5362203f, -0.1453549f),
    new Vector2(-0.1258792f,-0.1252949f),
    new Vector2(-0.01312404f, 0.1224191f),
    new Vector2(0.5923343f, -0.02137702f)
}, // Frame 0
            new Vector2[] {
    new Vector2(0.9490228f, 0.1539722f),
    new Vector2(0.6004911f, 0.2418281f),
    new Vector2(0.3590318f, 0.1250167f),
    new Vector2(0.2086769f, -0.03340951f),
    new Vector2(-0.1258796f, -0.1252949f),
    new Vector2(-0.01312404f, 0.1224191f),
    new Vector2(0.6545263f, 0.3600657f)
}, // Frame 1
            new Vector2[] {
    new Vector2(0.05415571f, 0.8978615f),
    new Vector2(0.4158771f, 0.6254196f),
    new Vector2(0.4195613f, -0.2149f),
    new Vector2(0.2752593f, -0.03340951f),
    new Vector2(-0.1258796f, -0.1252949f),
    new Vector2(-0.01312404f,0.1224191f),
    new Vector2(0.30043f, 0.4145421f)
}, // Frame 2
            new Vector2[] {
    new Vector2(-0.0004085973f, -0.07139195f),
    new Vector2(0.175734f, -0.0988505f),
    new Vector2(0.6754769f, -0.6644939f),
    new Vector2(0.4887599f, -0.1592588f),
    new Vector2(-0.005491503f, 0.1482754f),
    new Vector2(-0.1867176f, -0.1153253f),
}, // Frame 3
            new Vector2[] {
    new Vector2(0.02196317f, -0.8064664f),
    new Vector2(0.1742365f, -0.5973826f),
    new Vector2(0.2090835f, -0.1642798f),
    new Vector2(0.00497815f, 0.1792146f),
    new Vector2(-0.1742368f, -0.02489108f),
    new Vector2(0.05973809f, -0.194149f),
}, // Frame 4
            new Vector2[] {
    new Vector2(0.09510893f, -0.1095199f),
    new Vector2(-0.3086478f, -0.4828842f),
    new Vector2(-0.4131895f, -0.7965101f),
    new Vector2(-0.4878624f, -0.5625355f),
    new Vector2(-0.07965101f, 0.03484736f),
    new Vector2(0.05973843f, 0.1642802f),
}, // Frame 5
            new Vector2[] {
    new Vector2(0.1872672f, -0.06007047f),
    new Vector2(0.03553726f, 0.1492561f),
    new Vector2(-0.04975202f, 0.01776846f),
    new Vector2(-0.1528098f, -0.0568594f),
    new Vector2(0.08880316f, -0.3026214f),
    new Vector2(0.0074654f, -0.8221151f),
    new Vector2(0.2393491f, -0.532243f)
}, // Frame 6
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

    public void SetCollider2(int index)
    {
        if (index >= 0 && index < colliderPoints.Length && polygonCollider != null)
        {
            polygonCollider.SetPath(0, colliderPoints2[index]); // Establecer los puntos del collider para ese índice
        }
    }
}
