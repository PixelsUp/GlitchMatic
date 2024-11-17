using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ApuntarConArma : MonoBehaviour
{
    private Transform aimTransform;
    private SpriteRenderer aimSpriteRenderer; // Para cambiar la escala del sprite
    public _CharacterManager characterManager;


    private void Awake()
    {
        characterManager = FindObjectOfType<_CharacterManager>();
        // Encontrar el objeto "Aim"
        aimTransform = transform.Find("Aim");

        if (aimTransform == null)
        {
            Debug.LogError("No se ha encontrado el objeto 'Aim'. Asegúrate de que el nombre sea correcto.");
        }

        // Obtener el SpriteRenderer del objeto "Aim" si tiene uno
        aimSpriteRenderer = aimTransform.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!characterManager.isPaused)
        {
        // Obtener la posición del ratón en el plano Z=0
        Vector3 mousePosition = GetMouseWorldPositionWithZ(0f);

        // Calcular la dirección y el ángulo hacia el ratón
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

            // Voltear el arma en el eje X cuando el ratón está a la izquierda del personaje
            if (mousePosition.x < transform.position.x)
            {
                // Voltear horizontalmente
                aimTransform.localScale = new Vector3(-1, 1, 1);

                // Ajustar el ángulo para que no quede al revés
                aimTransform.eulerAngles = new Vector3(0, 0, angle + 180); // Rotamos 180 grados para corregir
            }
            else
            {
                // Mantener la escala normal
                aimTransform.localScale = new Vector3(1, 1, 1);

                // Rotación normal
                aimTransform.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }

    // Método para obtener la posición del ratón en el mundo, en un plano con Z fijo
    public Vector3 GetMouseWorldPositionWithZ(float zPlane)
    {
        // Crear un rayo desde la cámara en la dirección del ratón
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Crear un plano en el cual interceptar el rayo (en Z=0 o el valor que definas)
        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, zPlane));

        float distance;
        // Determinar el punto de intersección del rayo con el plano
        if (plane.Raycast(ray, out distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);
            return worldPosition;
        }

        return Vector3.zero; // Retorna algo por defecto si no se calcula la posición
    }
}
