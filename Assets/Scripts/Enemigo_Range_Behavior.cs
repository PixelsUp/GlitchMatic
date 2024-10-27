using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Range_Behavior : Enemy
{
    // Enlace al protagonista
    private _CharacterManager protagonista;

    // Prefab del proyectil que disparar� el enemigo
    [SerializeField] private GameObject proyectilPrefab;

    // Punto desde donde se disparan los proyectiles
    [SerializeField] private Transform puntoDisparo;

    // Definimos los posibles estados del enemigo
    private enum TEstado { BUSCANDO, AVANZANDO, ATACANDO }
    private TEstado estado = TEstado.BUSCANDO;

    // Variables relacionadas con el comportamiento del enemigo
    private Vector3 posicionProtagonista = Vector3.zero;
    [SerializeField] private float velocidadMovimiento = 2f; // Velocidad del enemigo
    private float tiempoEntreDisparos = 2f; // Tiempo de espera entre disparos
    [SerializeField] private int vida = 50;
    private bool puedeDisparar = true;
    private bool protagonistaDetectado = false; // Para controlar el cambio de rango


    // Distancias relevantes
    [SerializeField] private float distanciaDeteccion = 50f; // Distancia para detectar al protagonista
    [SerializeField] private float distanciaAtaque = 20f; // Distancia para disparar

    void Start()
    {
        protagonista = FindObjectOfType<_CharacterManager>();

        if (protagonista == null)
        {
            Debug.LogError("No se encontr� al protagonista en la escena.");
        }
    }

    void Update()
    {
        if (protagonistaDetectado == true)
        {
            GirarHaciaObjetivo(posicionProtagonista);
        }
        FSMRangedEnemy(); // L�gica de la m�quina de estados
    }

    // M�quina de estados finita (FSM) para el enemigo a distancia
    void FSMRangedEnemy()
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
                    Avanzar(posicionProtagonista);
                    estado = TEstado.AVANZANDO;
                    protagonistaDetectado = true;
                }
                break;

            case TEstado.AVANZANDO:
                // El enemigo contin�a avanzando hacia el protagonista
                if (distanciaAlProtagonista <= distanciaAtaque)
                {
                    estado = TEstado.ATACANDO;
                    StartCoroutine(DispararProyectilCoroutine());
                }
                else if (distanciaAlProtagonista > distanciaDeteccion)
                {
                    estado = TEstado.BUSCANDO;
                    protagonistaDetectado=false;
                }
                else
                {
                    Avanzar(posicionProtagonista);
                }
                break;

            case TEstado.ATACANDO:
                // Verificar si el enemigo est� dentro del rango de ataque
                if ((distanciaAlProtagonista <= distanciaAtaque) && puedeDisparar)
                {
                    
                    StartCoroutine(DispararProyectilCoroutine());
                    
                }
                else if (distanciaAlProtagonista > distanciaAtaque)
                {
                    estado = TEstado.AVANZANDO;
                    Avanzar(posicionProtagonista);
                    
                }
                break;
        }
    }

    // M�todo para avanzar hacia el protagonista
    public void Avanzar(Vector3 objetivo)
    {
        // Calculamos la direcci�n hacia el protagonista (objetivo)
        Vector3 direccion = (objetivo - transform.position).normalized;

        // Girar hacia el objetivo antes de avanzar
        GirarHaciaObjetivo(objetivo);

        // Movemos al enemigo en la direcci�n del objetivo a una velocidad constante
        transform.position += direccion * velocidadMovimiento * Time.deltaTime;
    }

    // Coroutine para manejar el disparo y el tiempo de espera entre disparos
    private IEnumerator DispararProyectilCoroutine()
    {
        puedeDisparar = false; // Desactivar disparo hasta que pase el cooldown

        // Instanciar el proyectil en el punto de disparo
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);

        // Configurar la direcci�n hacia el protagonista
        Vector3 direccion = (posicionProtagonista - transform.position).normalized;

        // Girar el proyectil para que apunte en la direcci�n de disparo
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        proyectil.transform.rotation = Quaternion.Euler(0f, 0f, angulo);

        // Asignar la direcci�n al proyectil
        proyectil.GetComponent<Proyectil>().ConfigurarDireccion(direccion);

        // Esperar el tiempo entre disparos
        yield return new WaitForSeconds(tiempoEntreDisparos);

        puedeDisparar = true; // Activar disparo nuevamente despu�s del cooldown
    }
}
