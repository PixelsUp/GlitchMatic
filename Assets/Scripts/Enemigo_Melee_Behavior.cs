using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Melee_Behavior : MonoBehaviour
{
    // Enlace al protagonista
    [SerializeField] _CharacterManager protagonista;

    // Definimos los posibles estados del enemigo
    private enum TEstado { BUSCANDO, AVANZANDO, ATACANDO }
    private TEstado estado = TEstado.BUSCANDO;

    // Variables relacionadas con el comportamiento del enemigo
    private Vector3 posicionProtagonista = Vector3.zero;
    [SerializeField] private float velocidadMovimiento = 2f; // Velocidad del enemigo
    private float danoAtaque = 20f;

    // Distancias relevantes
    [SerializeField] private float distanciaDeteccion = 25f; // Distancia para detectar al protagonista
    [SerializeField] private float distanciaDeteccionAmpliada = 50f; // Distancia ampliada cuando detecta

    [SerializeField] private float distanciaAtaque = 2f; // Distancia para atacar
    [SerializeField] private float tiempoEntreAtaques = 1f; // Tiempo de espera entre ataques
    [SerializeField] private int vida = 50; 
    private bool puedeAtacar = true; // Controla si el enemigo puede atacar
    private bool mirandoDerecha = true;

    private bool protagonistaDetectado = false; // Para controlar el cambio de rango

    // Método Start: Inicialización del script
    void Start()
{
    protagonista = _CharacterManager.Instance;

    if (protagonista == null)
    {
        Debug.LogError("No se encontró al protagonista en la escena.");
    }
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
                    protagonistaDetectado = true; // Marcar que el protagonista ha sido detectado
                    distanciaDeteccion = distanciaDeteccionAmpliada; // Ampliar rango de detección

                }
                break;

            case TEstado.AVANZANDO:
                // El enemigo continúa avanzando hacia el protagonista

                if (distanciaAlProtagonista <= distanciaAtaque)
                {
                    estado = TEstado.ATACANDO;
                    Atacar();
                }
                else if (distanciaAlProtagonista > distanciaDeteccionAmpliada)
                {
                    // Si el protagonista se aleja más allá del rango ampliado, vuelve a BUSCANDO
                    estado = TEstado.BUSCANDO;
                    protagonistaDetectado = false; // Protagonista ya no está en rango
                    distanciaDeteccion = 25f; // Reducir el rango de nuevo
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
