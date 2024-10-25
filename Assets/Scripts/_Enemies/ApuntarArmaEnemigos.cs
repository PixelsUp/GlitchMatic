using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApuntarConArmaEnemigos : MonoBehaviour
{
    private Transform aimTransform;
    private SpriteRenderer aimSpriteRenderer; // Para cambiar la escala del sprite
    private Character_Functioning protagonista;

    private void Awake()
    {
        // Encontrar el objeto "Aim"
        aimTransform = transform.Find("Aim");

        if (aimTransform == null)
        {
            Debug.LogError("No se ha encontrado el objeto 'Aim'. Aseg�rate de que el nombre sea correcto.");
        }

        // Obtener el SpriteRenderer del objeto "Aim" si tiene uno
        aimSpriteRenderer = aimTransform.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        protagonista = FindObjectOfType<Character_Functioning>();

        if (protagonista == null)
        {
            Debug.LogError("No se encontr� al protagonista en la escena.");
        }
    }

    private void Update()
    {
        // Aseg�rate de que el protagonista est� en el rango de detecci�n antes de apuntar
        if (protagonista != null)
        {
            Vector3 protagonistaPosition = protagonista.transform.position;

            // Calcular la direcci�n y el �ngulo hacia el protagonista
            Vector3 aimDirection = (protagonistaPosition - transform.position).normalized; // Cambiado a aimTransform.position
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

            if (!(transform.localScale.x > 0)) // Asumiendo que mirandoDerecha significa escala positiva
            {
                angle += 180; // Rota 180 grados si est� mirando a la izquierda
            }
            // Ajustar la rotaci�n del objeto "Aim"
            aimTransform.eulerAngles = new Vector3(0, 0, angle);

        }
    }
}