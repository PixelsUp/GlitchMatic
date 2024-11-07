using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Main : MonoBehaviour
{
    public float velocidad = 10f;        // Velocidad del proyectil
    public int dano = 20;                // Daño que hace el proyectil
    public float tiempoDeVida = 3f;      // Tiempo en segundos antes de destruir el proyectil

    private Vector3 direccion;

    // Método para configurar la dirección del proyectil
    public void ConfigurarDireccion(Vector3 nuevaDireccion)
    {
        direccion = nuevaDireccion;
    }

    void Start()
    {
        // Destruye el proyectil automáticamente después del tiempo de vida
        Destroy(gameObject, tiempoDeVida);
    }

    void Update()
    {
        // Mueve el proyectil en la dirección asignada
        transform.position += direccion * velocidad * Time.deltaTime;
    }


    
    // Detecta la colisión con un enemigo
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Obtener el script de enemigo y aplicar daño
            var enemigo = collision.GetComponent<Enemy>();
            //var enemigo = collision.GetComponentInParent<Enemy>();
            if (enemigo != null)
            {
                Debug.Log("Proyectil impactó al enemigo. Daño aplicado: " + dano);
                enemigo.TakeDamage(dano);
            }

            // Destruye el proyectil al impactar
            Destroy(gameObject);
        }
    }
    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemigo = collision.gameObject.GetComponent<Enemy>();
            if (enemigo != null)
            {
                Debug.Log("Proyectil impactó al enemigo. Daño aplicado: " + dano);
                enemigo.TakeDamage(dano);
            }
            Destroy(gameObject);
        }
    }*/
}

