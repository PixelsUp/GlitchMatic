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
            Debug.LogError("No se ha encontrado el objeto 'Aim'. Aseg�rate de que el nombre sea correcto.");
        }

        // Obtener el SpriteRenderer del objeto "Aim" si tiene uno
        aimSpriteRenderer = aimTransform.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!characterManager.isPaused)
        {
        // Obtener la posici�n del rat�n en el plano Z=0
        Vector3 mousePosition = GetMouseWorldPositionWithZ(0f);

        // Calcular la direcci�n y el �ngulo hacia el rat�n
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

            // Voltear el arma en el eje X cuando el rat�n est� a la izquierda del personaje
            if (mousePosition.x < transform.position.x)
            {
                // Voltear horizontalmente
                aimTransform.localScale = new Vector3(-1, 1, 1);

                // Ajustar el �ngulo para que no quede al rev�s
                aimTransform.eulerAngles = new Vector3(0, 0, angle + 180); // Rotamos 180 grados para corregir
            }
            else
            {
                // Mantener la escala normal
                aimTransform.localScale = new Vector3(1, 1, 1);

                // Rotaci�n normal
                aimTransform.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }

    // M�todo para obtener la posici�n del rat�n en el mundo, en un plano con Z fijo
    public Vector3 GetMouseWorldPositionWithZ(float zPlane)
    {
        // Crear un rayo desde la c�mara en la direcci�n del rat�n
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Crear un plano en el cual interceptar el rayo (en Z=0 o el valor que definas)
        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, zPlane));

        float distance;
        // Determinar el punto de intersecci�n del rayo con el plano
        if (plane.Raycast(ray, out distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);
            return worldPosition;
        }

        return Vector3.zero; // Retorna algo por defecto si no se calcula la posici�n
    }
}
