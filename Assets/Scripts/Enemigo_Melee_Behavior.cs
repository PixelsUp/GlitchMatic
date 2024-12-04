using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemigo_Melee_Behavior : Enemy
{
    // Enlace al protagonista
    private _CharacterManager protagonista;

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

    private bool protagonistaDetectado = false; // Para controlar el cambio de rango

    // Variables para el sistema de gru�idos
    private float gruntTimer;       // Temporizador de gru�ido
    private float gruntInterval;    // Intervalo aleatorio entre gru�idos
    private bool firstGrunt = true;
    public bool rolling = false;
    private Transform pos;

    // M�todo Start: Inicializaci�n del script
    void Start()
    {
        animator = GetComponent<Animator>(); // Inicializaci�n del animator

        protagonista = FindObjectOfType<_CharacterManager>();

    if (protagonista == null)
    {
        Debug.LogError("No se encontr� al protagonista en la escena.");
    }
}

    // M�todo Update: Llamamos a la m�quina de estados cada frame
    void Update()
    {
        if (protagonistaDetectado == true)
        {
            if (firstGrunt)
            {
                SfxScript.TriggerSfx("SfxGrunt2");
                firstGrunt = false;
            }
            gruntTimer -= Time.deltaTime;
            if (gruntTimer <= 0f)
            {
                Debug.Log("GRUNT2");
                SfxScript.TriggerSfx("SfxGrunt2");
                ResetGruntTimer();
            }
        }
        FSMMeleeEnemy(); // L�gica de la m�quina de estados
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(rollBool());
        }
    }

    IEnumerator rollBool()
    {
        rolling = true;
        yield return new WaitForSeconds(2.5f);
        rolling = false;
    }

    private void ResetGruntTimer()
    {
        // Configura un intervalo aleatorio entre 3 y 8 segundos
        gruntInterval = UnityEngine.Random.Range(3f, 10f);
        gruntTimer = gruntInterval;
    }
    // M�quina de estados finita (FSM) para el enemigo
    void FSMMeleeEnemy()
    {
        // Obtenemos la posici�n del protagonista
        posicionProtagonista = protagonista.transform.position;
        float distanciaAlProtagonista = Vector3.Distance(transform.position, posicionProtagonista);

        // Cambia la l�gica en funci�n del estado actual
        switch (estado)
        {
            case TEstado.BUSCANDO:
                // El enemigo mira y avanza hacia el protagonista si est� dentro del rango de detecci�n
                if (distanciaAlProtagonista <= distanciaDeteccion)
                {
                    // El enemigo mira hacia el protagonista
                    //MirarHacia(posicionProtagonista);

                    // Si el enemigo ya est� mirando al protagonista, comienza a avanzar
                    Avanzar(posicionProtagonista);
                    estado = TEstado.AVANZANDO;
                    protagonistaDetectado = true; // Marcar que el protagonista ha sido detectado
                    distanciaDeteccion = distanciaDeteccionAmpliada; // Ampliar rango de detecci�n

                }
                else
                {
                    animator.SetBool("IsRunning", false);
                }
                break;

            case TEstado.AVANZANDO:
                // El enemigo contin�a avanzando hacia el protagonista
                animator.SetBool("IsRunning", true); // Activar animaci�n de correr

                if (distanciaAlProtagonista <= distanciaAtaque)
                {
                    estado = TEstado.ATACANDO;
                    Atacar();
                }
                else if (distanciaAlProtagonista > distanciaDeteccionAmpliada)
                {
                    // Si el protagonista se aleja m�s all� del rango ampliado, vuelve a BUSCANDO
                    estado = TEstado.BUSCANDO;
                    protagonistaDetectado = false; // Protagonista ya no est� en rango
                    distanciaDeteccion = 25f; // Reducir el rango de nuevo
                }
                else
                {
                    Avanzar(posicionProtagonista);
                }
                break;


            case TEstado.ATACANDO:
                animator.SetBool("IsRunning", false); // Detener animaci�n de correr al atacar

                // Verificar la distancia actual entre el enemigo y el protagonista
                if ((distanciaAlProtagonista <= distanciaAtaque) && puedeAtacar)
                {
                    // Si el enemigo est� dentro del rango de ataque, realiza el ataque
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
        // Calculamos la direcci�n hacia el protagonista (objetivo)
        animator.SetBool("IsRunning", true);
        Vector3 direccion = (objetivo - transform.position).normalized;

        GirarHaciaObjetivo(objetivo);
        // Movemos al enemigo en la direcci�n del objetivo a una velocidad constante
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
        protagonista.TakeDamage(danoAtaque); // Aplicar da�o
        Debug.Log("Salud restante del protagonista: " + protagonista.hp);

        yield return new WaitForSeconds(tiempoEntreAtaques); // Esperar un segundo

        puedeAtacar = true; // Activar el ataque de nuevo
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            if (rolling)
            {
                StartCoroutine(stopVelocity());
            }
            else
            {
                transform.position = collision.transform.position;
            }
        }
    }
    IEnumerator stopVelocity()
    {
        yield return new WaitForSeconds(1f);
        pos = transform;
        yield return new WaitForSeconds(0.1f);
        this.posicionProtagonista = pos.position;
    }
}
