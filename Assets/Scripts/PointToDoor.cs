using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointToDoor : MonoBehaviour
{
    public GameObject Flecha;
    public Transform puerta; // Asigna la puerta de salida en el inspector
    private Transform flechaTransform;
    private bool activado = false;

    void OnEnable()
    {
        // Subscribirse al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Desubscribirse del evento de cambio de escena
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Método que se llama cuando una nueva escena es cargada
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Encontrar el GameObject con la etiqueta "Transition"
        Flecha.SetActive(false);
        activado = false;
        GameObject puertaObjeto = GameObject.Find("TransitionPoint");
        if (puertaObjeto != null)
        {
            puerta = puertaObjeto.transform; // Asignar el transform de la puerta encontrada
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con la etiqueta 'Transition' en la escena.");
        }
    }

    private void Update()
    {
        if (activado)
        {
            // Calcular la dirección hacia la puerta
            Vector3 direccionHaciaPuerta = (puerta.position - flechaTransform.position).normalized;

            // Calcular el ángulo para apuntar hacia la puerta
            float angulo = Mathf.Atan2(direccionHaciaPuerta.y, direccionHaciaPuerta.x) * Mathf.Rad2Deg;

            // Aplicar la rotación a la flecha
            flechaTransform.eulerAngles = new Vector3(0, 0, angulo);
        }
    }
    public void Activate()
    {
        activado = true;
        Flecha.SetActive(true);
        flechaTransform = Flecha.transform;
    }
}
