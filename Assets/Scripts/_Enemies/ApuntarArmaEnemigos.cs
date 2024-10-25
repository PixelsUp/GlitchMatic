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
            Debug.LogError("No se ha encontrado el objeto 'Aim'. Asegúrate de que el nombre sea correcto.");
        }

        // Obtener el SpriteRenderer del objeto "Aim" si tiene uno
        aimSpriteRenderer = aimTransform.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        protagonista = FindObjectOfType<Character_Functioning>();

        if (protagonista == null)
        {
            Debug.LogError("No se encontró al protagonista en la escena.");
        }
    }

    private void Update()
    {
        // Asegúrate de que el protagonista esté en el rango de detección antes de apuntar
        if (protagonista != null)
        {
            Vector3 protagonistaPosition = protagonista.transform.position;

            // Calcular la dirección y el ángulo hacia el protagonista
            Vector3 aimDirection = (protagonistaPosition - transform.position).normalized; // Cambiado a aimTransform.position
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

            if (!(transform.localScale.x > 0)) // Asumiendo que mirandoDerecha significa escala positiva
            {
                angle += 180; // Rota 180 grados si está mirando a la izquierda
            }
            // Ajustar la rotación del objeto "Aim"
            aimTransform.eulerAngles = new Vector3(0, 0, angle);

        }
    }
}