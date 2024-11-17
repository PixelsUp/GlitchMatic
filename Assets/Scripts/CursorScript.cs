using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public Texture2D defaultCursorTexture; // Cursor normal
    public Texture2D enemyCursorTexture;  // Cursor cuando está sobre un enemigo
    public Vector2 hotSpot = Vector2.zero; // Punto de anclaje del cursor
    public CursorMode cursorMode = CursorMode.Auto; // Modo del cursor

    private void Start()
    {
        // Configurar el cursor inicial como el predeterminado
        SetCustomCursor(defaultCursorTexture);
    }

    private void Update()
    {
        // Detectar automáticamente si el cursor está sobre un enemigo
        DetectHover();
    }

    private void SetCustomCursor(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void DetectHover()
    {
        // Convertir la posición del mouse a coordenadas del mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePosition.x, mousePosition.y);

        // Hacer un Raycast2D desde la posición del mouse
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null)
        {
            // Verificar si el objeto tiene el tag "Enemy"
            if (hit.collider.CompareTag("Enemy"))
            {
                SetCustomCursor(enemyCursorTexture);
                return; // Salir para evitar cambios innecesarios
            }
        }

        // Si no está sobre un enemigo, restaurar el cursor predeterminado
        SetCustomCursor(defaultCursorTexture);
    }

    // Métodos que manejan los eventos individuales (OnMouseEnter y OnMouseExit)
    private void OnMouseEnter()
    {
        // Cambiar al cursor de enemigo al entrar en el área de este objeto
        SetCustomCursor(enemyCursorTexture);
    }

    private void OnMouseExit()
    {
        // Restaurar el cursor normal al salir del área de este objeto
        SetCustomCursor(defaultCursorTexture);
    }
}
