using UnityEngine;

public class Win : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CambiarEstado(GameManager.GameState.Win);
            Debug.Log("El jugador Ganó.");
        }
    }
}
