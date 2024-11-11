using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    private bool isActive = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            Debug.Log("Punto de Transicion");
            RoomManager.Instance.LoadNextRoom();
        }
    }

    void OnCollideEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            Debug.Log("Punto de Transicion");
            RoomManager.Instance.LoadNextRoom();
        }
    }
    public void ActivateTransition()
    {
        isActive = true;
        Debug.Log("TransitionPoint activated.");
        // igual podemos hacer una animacion para habilitar el transition point
    }
}
