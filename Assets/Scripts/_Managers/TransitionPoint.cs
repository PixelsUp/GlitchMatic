using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    private bool isActive = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            Debug.Log("Punto de Transicion");

            // Destruir instancia del CameraManager si existe
            //if (_CameraManager.Instance != null)
            //{
            //    Destroy(_CameraManager.Instance.gameObject);
            //}

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
        GameObject character = GameObject.Find("Character");
        character.GetComponent<_CharacterManager>().Health(false);
        PointToDoor point = character.GetComponent<PointToDoor>();
        point.Activate();
        // igual podemos hacer una animacion para habilitar el transition point
    }
}
