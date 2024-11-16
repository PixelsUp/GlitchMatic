using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public Texture2D cursorTexture; // Arrastra la textura del cursor aquí desde el inspector
    public Vector2 hotSpot = Vector2.zero; // Define el punto de anclaje del cursor
    public CursorMode cursorMode = CursorMode.Auto; // Modo del cursor

    void Start()
    {
        SetCustomCursor();
    }

    void SetCustomCursor()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode); // Restablece el cursor al predeterminado
    }
}