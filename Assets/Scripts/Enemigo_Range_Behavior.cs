using System;
using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;

public class Enemigo_Range_Behavior : Enemy
{
    // Enlace al protagonista
    private _CharacterManager protagonista;

    // Prefab del proyectil que disparará el enemigo
    [SerializeField] private GameObject proyectilPrefab;

    // Punto desde donde se disparan los proyectiles
    [SerializeField] private Transform puntoDisparo;

    // Definimos los posibles estados del enemigo
    private enum TEstado { BUSCANDO, AVANZANDO, ATACANDO, RETROCEDIENDO }
    private TEstado estado = TEstado.BUSCANDO;

    // Variables relacionadas con el comportamiento del enemigo
    private Vector3 posicionProtagonista = Vector3.zero;
    [SerializeField] private float velocidadMovimiento = 2f; // Velocidad del enemigo
    private float tiempoEntreDisparos = 2f; // Tiempo de espera entre disparos
    [SerializeField] private int vida = 50;
    private bool puedeDisparar = true;
    private bool protagonistaDetectado = false; // Para controlar el cambio de rango
    private bool disparando = false; // Bandera para evitar múltiples disparos

    // Variables para el sistema de gruñidos
    private float gruntTimer;       // Temporizador de gruñido
    private float gruntInterval;    // Intervalo aleatorio entre gruñidos
    private bool firstGrunt = true;

    // Distancias relevantes
    [SerializeField] private float distanciaDeteccion = 50f; // Distancia para detectar al protagonista
    [SerializeField] private float distanciaAtaque = 20f; // Distancia para disparar
    [SerializeField] private float distanciaMinima = 30f; // Distancia mínima de seguridad

    void Start()
    {

        animator = GetComponent<Animator>(); // Inicialización del animator

        protagonista = FindObjectOfType<_CharacterManager>();

        if (protagonista == null)
        {
            Debug.LogError("No se encontró al protagonista en la escena.");
        }
        ResetGruntTimer();
    }

    void Update()
    {
        if (protagonistaDetectado)
        {
            if (firstGrunt)
            {
                SfxScript.TriggerSfx("SfxGrunt1");
                firstGrunt = false;
            }
            GirarHaciaObjetivo(posicionProtagonista);

            // Manejo del temporizador de gruñidos
            gruntTimer -= Time.deltaTime;
            if (gruntTimer <= 0f)
            {
                // Ejecuta un gruñido y reinicia el temporizador
                Debug.Log("GRUNT1");
                SfxScript.TriggerSfx("SfxGrunt1");
                ResetGruntTimer();
            }
        }

        FSMRangedEnemy(); // Lógica de la máquina de estados
    }

    private void ResetGruntTimer()
    {
        // Configura un intervalo aleatorio entre 3 y 8 segundos
        gruntInterval = UnityEngine.Random.Range(3f, 10f);
        gruntTimer = gruntInterval;
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
                if (distanciaAlProtagonista <= distanciaDeteccion)
                {
                    Avanzar(posicionProtagonista);
                    estado = TEstado.AVANZANDO;
                    protagonistaDetectado = true;
                }
                break;

            case TEstado.AVANZANDO:
                animator.SetBool("IsRunning", true);

                if (distanciaAlProtagonista <= distanciaMinima)
                {
                    estado = TEstado.RETROCEDIENDO;
                }
                else if (distanciaAlProtagonista <= distanciaAtaque)
                {
                    estado = TEstado.ATACANDO;
                }
                else if (distanciaAlProtagonista > distanciaDeteccion)
                {
                    estado = TEstado.BUSCANDO;
                    protagonistaDetectado = false;
                }
                else
                {
                    Avanzar(posicionProtagonista);
                }
                break;

            case TEstado.ATACANDO:
                animator.SetBool("IsRunning", false);

                if (distanciaAlProtagonista <= distanciaMinima)
                {
                    estado = TEstado.RETROCEDIENDO;
                }
                else if (distanciaAlProtagonista > distanciaAtaque)
                {
                    estado = TEstado.AVANZANDO;
                }
                else if (!disparando && puedeDisparar)
                {
                    disparando = true; // Activar la bandera antes de disparar
                    StartCoroutine(DispararProyectilCoroutine());
                    SfxScript.TriggerSfx("SfxBowShot");
                }
                break;

            case TEstado.RETROCEDIENDO:
                animator.SetBool("IsRunning", true);

                if (distanciaAlProtagonista > distanciaMinima)
                {
                    estado = TEstado.ATACANDO;
                }
                else
                {
                    Retroceder(posicionProtagonista);
                }
                break;
        }
    }

    // Método para retroceder
    void Retroceder(Vector3 objetivo)
    {
        // Calculamos la dirección opuesta al objetivo (protagonista)
        Vector3 direccionEscape = (transform.position - objetivo).normalized;

        // Movemos al enemigo en la dirección opuesta al protagonista
        transform.position += direccionEscape * velocidadMovimiento * Time.deltaTime;

        // Girar hacia el objetivo para mantener el enfrentamiento visual
        GirarHaciaObjetivo(objetivo);
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

        puedeDisparar = true;  // Activar disparo nuevamente después del cooldown
        disparando = false;    // Liberar la bandera para permitir nuevos disparos
    }
}