using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
        [SerializeField] private float velocidad = 10f;  // Velocidad del proyectil
        [SerializeField] private float dano = 10f;       // Daño que hace el proyectil
        [SerializeField] private float tiempoDeVida = 5f; // Tiempo que el proyectil permanecerá antes de destruirse

        public GameObject proyectil;

        private Vector3 direccion;  // Dirección en la que se moverá el proyectil

        // Método para configurar la dirección del proyectil
        public void ConfigurarDireccion(Vector3 nuevaDireccion)
        {
            direccion = nuevaDireccion;
        }

        private void Start()
        {
            // Destruir el proyectil después de un tiempo si no colisiona
            Destroy(gameObject, tiempoDeVida);
        }

        private void Update()
        {
            // Mover el proyectil en la dirección establecida
            transform.position += direccion * velocidad * Time.deltaTime;
        }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        // Verifica si el proyectil colisiona con el protagonista
        if (colision.CompareTag("Player")) // Asegúrate de que el protagonista tiene el tag "Protagonista"
        {
            // Accede al script de vida del protagonista y le quita vida
            _CharacterManager protagonista = colision.GetComponent<_CharacterManager>();
            if (protagonista != null)
            {
                protagonista.TakeDamage(dano); // Aplica el daño al protagonista
            }

            // Destruye el proyectil después de la colisión
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
