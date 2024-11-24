using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public Texture2D defaultCursorTexture; // Cursor normal
    public Texture2D enemyCursorTexture;  // Cursor cuando está sobre un enemigo
    public Vector2 hotSpot = Vector2.zero; // Punto de anclaje del cursor
    public Vector2 enemyHotSpot = new Vector2(540, 540); // Punto de anclaje para el cursor enemigo
    public CursorMode cursorMode = CursorMode.Auto; // Modo del cursor

    private void Start()
    {
        // Configurar el cursor inicial como el predeterminado
        SetCustomCursor(defaultCursorTexture, hotSpot);
    }

    private void Update()
    {
        // Detectar automáticamente si el cursor está sobre un enemigo
        DetectHover();
    }

    private void SetCustomCursor(Texture2D cursorTexture, Vector2 hotspot)
    {
        // Ajustar escala si es WebGL
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Vector2 scaledHotspot = hotspot * (32f / cursorTexture.width); // Escalar el punto de anclaje
            Texture2D resizedTexture = ResizeCursorTexture(cursorTexture, 32, 32); // Ajustar el tamaño
            Cursor.SetCursor(resizedTexture, scaledHotspot, cursorMode);
        }
        else
        {
            // Mantener comportamiento normal en otras plataformas
            Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
        }
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
                SetCustomCursor(enemyCursorTexture, hotSpot);
                return; // Salir para evitar cambios innecesarios
            }
        }
        else
        {
            // Si no está sobre un enemigo, restaurar el cursor predeterminado
            SetCustomCursor(defaultCursorTexture, hotSpot);
        }
    }

    private Texture2D ResizeCursorTexture(Texture2D originalTexture, int width, int height)
    {
        // Crear una nueva textura con las dimensiones especificadas
        Texture2D resizedTexture = new Texture2D(width, height);
        Color[] resizedPixels = resizedTexture.GetPixels();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Calcular la proporción para tomar el color del píxel correspondiente
                float u = (float)x / width;
                float v = (float)y / height;
                resizedPixels[y * width + x] = originalTexture.GetPixelBilinear(u, v);
            }
        }

        resizedTexture.SetPixels(resizedPixels);
        resizedTexture.Apply();

        return resizedTexture;
    }
}
