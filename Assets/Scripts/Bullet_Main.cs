using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Main : MonoBehaviour
{
    public float velocidad = 10f;        // Velocidad del proyectil
    public int dano = 20;                // Da�o que hace el proyectil
    public float tiempoDeVida = 3f;      // Tiempo en segundos antes de destruir el proyectil

    public GameObject particulas;

    private Vector3 direccion;

    // M�todo para configurar la direcci�n del proyectil
    public void ConfigurarDireccion(Vector3 nuevaDireccion)
    {
        direccion = nuevaDireccion;
    }

    void Start()
    {
        // Destruye el proyectil autom�ticamente despu�s del tiempo de vida
        Destroy(gameObject, tiempoDeVida);
    }

    void Update()
    {
        // Mueve el proyectil en la direcci�n asignada
        transform.position += direccion * velocidad * Time.deltaTime;
    }


    
    // Detecta la colisi�n con un enemigo
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Obtener el script de enemigo y aplicar da�o
            var enemigo = collision.GetComponent<Enemy>();
            //var enemigo = collision.GetComponentInParent<Enemy>();
            if (enemigo != null)
            {
                Debug.Log("Proyectil impact� al enemigo. Da�o aplicado: " + dano);
                enemigo.TakeDamage(dano);
            }

            Instantiate(particulas, transform.position, Quaternion.identity);
            // Destruye el proyectil al impactar
            Destroy(gameObject);
        }
        if (collision.CompareTag("Walls"))
        {
            Instantiate(particulas, transform.position, Quaternion.identity);
            // Destruye el proyectil al impactar
            Destroy(gameObject);
        }

        if (collision.CompareTag("Boss"))
        {
            // Obtener el script de enemigo y aplicar da�o
            var boss = collision.GetComponent<Drake_Behaviour>();
            //var enemigo = collision.GetComponentInParent<Enemy>();
            if (boss != null)
            {
                Debug.Log("Proyectil impact� al enemigo. Da�o aplicado: " + dano);
                boss.TakeDamage(dano);
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
                Debug.Log("Proyectil impact� al enemigo. Da�o aplicado: " + dano);
                enemigo.TakeDamage(dano);
            }
            Destroy(gameObject);
        }
    }*/
}

