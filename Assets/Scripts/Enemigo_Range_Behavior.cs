using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Range_Behavior : MonoBehaviour
{
    // Enlace al protagonista
    private Character_Functioning protagonista;

    // Prefab del proyectil que disparará el enemigo
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
    private bool mirandoDerecha = true;
    private bool protagonistaDetectado = false; // Para controlar el cambio de rango


    // Distancias relevantes
    [SerializeField] private float distanciaDeteccion = 50f; // Distancia para detectar al protagonista
    [SerializeField] private float distanciaAtaque = 20f; // Distancia para disparar

    void Start()
    {
        protagonista = FindObjectOfType<Character_Functioning>();

        if (protagonista == null)
        {
            Debug.LogError("No se encontró al protagonista en la escena.");
        }
    }

    void Update()
    {
        if (protagonistaDetectado == true)
        {
            GirarHaciaObjetivo(posicionProtagonista);
        }
        FSMRangedEnemy(); // Lógica de la máquina de estados
    }

    // Máquina de estados finita (FSM) para el enemigo a distancia
    void FSMRangedEnemy()
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
                    Avanzar(posicionProtagonista);
                    estado = TEstado.AVANZANDO;
                    protagonistaDetectado = true;
                }
                break;

            case TEstado.AVANZANDO:
                // El enemigo continúa avanzando hacia el protagonista
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
                // Verificar si el enemigo está dentro del rango de ataque
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

    // Método para avanzar hacia el protagonista
    public void Avanzar(Vector3 objetivo)
    {
        // Calculamos la dirección hacia el protagonista (objetivo)
        Vector3 direccion = (objetivo - transform.position).normalized;

        // Girar hacia el objetivo antes de avanzar
        GirarHaciaObjetivo(objetivo);

        // Movemos al enemigo en la dirección del objetivo a una velocidad constante
        transform.position += direccion * velocidadMovimiento * Time.deltaTime;
    }

    // Coroutine para manejar el disparo y el tiempo de espera entre disparos
    private IEnumerator DispararProyectilCoroutine()
    {
        puedeDisparar = false; // Desactivar disparo hasta que pase el cooldown

        // Instanciar el proyectil en el punto de disparo
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);

        // Configurar la dirección hacia el protagonista
        Vector3 direccion = (posicionProtagonista - transform.position).normalized;

        // Girar el proyectil para que apunte en la dirección de disparo
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        proyectil.transform.rotation = Quaternion.Euler(0f, 0f, angulo);

        // Asignar la dirección al proyectil
        proyectil.GetComponent<Proyectil>().ConfigurarDireccion(direccion);

        // Esperar el tiempo entre disparos
        yield return new WaitForSeconds(tiempoEntreDisparos);

        puedeDisparar = true; // Activar disparo nuevamente después del cooldown
    }

    // Método para girar al enemigo hacia el objetivo
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
