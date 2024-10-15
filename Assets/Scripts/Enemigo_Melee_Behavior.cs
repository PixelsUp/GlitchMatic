using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Melee_Behavior : MonoBehaviour
{
    // Enlace al protagonista
    [SerializeField] Character_Functioning protagonista;

    // Definimos los posibles estados del enemigo
    private enum TEstado { BUSCANDO, AVANZANDO, ATACANDO }
    private TEstado estado = TEstado.BUSCANDO;

    // Variables relacionadas con el comportamiento del enemigo
    private Vector3 posicionProtagonista = Vector3.zero;
    [SerializeField] private float velocidadMovimiento = 2f; // Velocidad del enemigo
    private float danoAtaque = 20f;

    // Distancias relevantes
    [SerializeField] private float distanciaDeteccion = 10f; // Distancia para detectar al protagonista
    [SerializeField] private float distanciaAtaque = 2f; // Distancia para atacar
    [SerializeField] private float tiempoEntreAtaques = 1f; // Tiempo de espera entre ataques
    [SerializeField] private int vida = 50; 
    private bool puedeAtacar = true; // Controla si el enemigo puede atacar
    private bool mirandoDerecha = true;

    // Método Start: Inicialización del script
    void Start()
    {
    }

    // Método Update: Llamamos a la máquina de estados cada frame
    void Update()
    {
        FSMMeleeEnemy(); // Lógica de la máquina de estados
    }

    // Máquina de estados finita (FSM) para el enemigo
    void FSMMeleeEnemy()
    {
        // Obtenemos la posición del protagonista
        posicionProtagonista = protagonista.transform.position;
        float distanciaAlProtagonista = Vector3.Distance(transform.position, posicionProtagonista);

        // Cambia la lógica en función del estado actual
        switch (estado)
        {
            case TEstado.BUSCANDO:
                // El enemigo mira y avanza hacia el protagonista si está dentro del rango de detección
                if (distanciaAlProtagonista <= distanciaDeteccion)
                {
                    // El enemigo mira hacia el protagonista
                    //MirarHacia(posicionProtagonista);

                    // Si el enemigo ya está mirando al protagonista, comienza a avanzar
                    Avanzar(posicionProtagonista);
                    estado = TEstado.AVANZANDO;

                }
                break;

            case TEstado.AVANZANDO:
                // El enemigo continúa avanzando hacia el protagonista

                if (distanciaAlProtagonista <= distanciaAtaque)
                {
                    estado = TEstado.ATACANDO;
                    Atacar();
                }
                else if (distanciaAlProtagonista > distanciaDeteccion)
                {
                    estado = TEstado.BUSCANDO;
                }
                else
                {
                    Avanzar(posicionProtagonista);
                }
                break;


            case TEstado.ATACANDO:
                // Verificar la distancia actual entre el enemigo y el protagonista
                if ((distanciaAlProtagonista <= distanciaAtaque) && puedeAtacar)
                {
                    // Si el enemigo está dentro del rango de ataque, realiza el ataque
                    StartCoroutine(AtacarCoroutine());
                }
                else
                {
                    // Si el protagonista se aleja, volver a avanzar
                    estado = TEstado.AVANZANDO;
                    Avanzar(posicionProtagonista);
                }
                break;

        }
    }

    public void Avanzar(Vector3 objetivo)
    {
        // Calculamos la dirección hacia el protagonista (objetivo)
        Vector3 direccion = (objetivo - transform.position).normalized;

        GirarHaciaObjetivo(objetivo);
        // Movemos al enemigo en la dirección del objetivo a una velocidad constante
        transform.position += direccion * velocidadMovimiento * Time.deltaTime;
    }

    public void Atacar()
    {
        if (puedeAtacar && protagonista.hp > 0)
        {
            StartCoroutine(AtacarCoroutine());
        }
    }

    // Coroutine para manejar el ataque y el tiempo de espera
    private IEnumerator AtacarCoroutine()
    {
        puedeAtacar = false; // Desactivar el ataque
        protagonista.TakeDamage(danoAtaque); // Aplicar daño
        Debug.Log("Salud restante del protagonista: " + protagonista.hp);

        yield return new WaitForSeconds(tiempoEntreAtaques); // Esperar un segundo

        puedeAtacar = true; // Activar el ataque de nuevo
    }

    private void GirarHaciaObjetivo(Vector3 objetivo)
    {
        // Calcula la diferencia en la posición en el eje X entre el enemigo y el objetivo
        float direccion = objetivo.x - transform.position.x;

        // Si el objetivo está a la derecha y el enemigo está mirando a la izquierda, o viceversa, cambiar la escala
        if ((direccion > 0 && !mirandoDerecha) || (direccion < 0 && mirandoDerecha))
        {
            // Cambiar de dirección
            Girar();
        }
    }

    // Método que invierte la escala en X para simular un giro
    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha; // Cambiar el estado de dirección

        // Obtener la escala actual del objeto
        Vector3 escala = transform.localScale;

        // Invertir la escala en X
        escala.x *= -1;

        // Aplicar la nueva escala al objeto
        transform.localScale = escala;

    }

    // Función para recibir daño y restar vida al enemigo
    public void TakeDamage(int damage)
    {
        vida -= damage;
        if (vida <= 0)
        {
            Destroy(gameObject, 2f);
        }
    }
}
