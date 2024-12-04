using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
        [SerializeField] private float velocidad = 10f;  // Velocidad del proyectil
        [SerializeField] private float dano = 10f;       // Da�o que hace el proyectil
        [SerializeField] private float tiempoDeVida = 5f; // Tiempo que el proyectil permanecer� antes de destruirse

        public GameObject proyectil;

        private Vector3 direccion;  // Direcci�n en la que se mover� el proyectil

        // M�todo para configurar la direcci�n del proyectil
        public void ConfigurarDireccion(Vector3 nuevaDireccion)
        {
            direccion = nuevaDireccion;
        }

        private void Start()
        {
            // Destruir el proyectil despu�s de un tiempo si no colisiona
            Destroy(gameObject, tiempoDeVida);
        }

        private void Update()
        {
            // Mover el proyectil en la direcci�n establecida
            transform.position += direccion * velocidad * Time.deltaTime;
        }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        // Verifica si el proyectil colisiona con el protagonista
        if (colision.CompareTag("Player")) // Aseg�rate de que el protagonista tiene el tag "Protagonista"
        {
            // Accede al script de vida del protagonista y le quita vida
            _CharacterManager protagonista = colision.GetComponent<_CharacterManager>();
            if (protagonista != null)
            {
                protagonista.TakeDamage(dano); // Aplica el da�o al protagonista
            }

            // Destruye el proyectil despu�s de la colisi�n
            Destroy(gameObject);
        }
        else if (colision.CompareTag("Walls"))
        {
            Instantiate(proyectil, transform.position, transform.rotation);
            // Destruye el proyectil al impactar
            Destroy(gameObject);
        }
    }
}
