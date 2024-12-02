using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathScript : MonoBehaviour
{
    public int damage = 20; // Daño que inflige la llama
    private _CharacterManager protagonista;

    // Método Start: Inicialización del script
    void Start()
    {
        protagonista = FindObjectOfType<_CharacterManager>();

        if (protagonista == null)
        {
            Debug.LogError("No se encontró al protagonista en la escena.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto que toca la llama es el jugador
        if (collision.CompareTag("Player"))
        {
           protagonista.TakeDamage(damage); // Infligir daño al jugador
        }
    }
}
