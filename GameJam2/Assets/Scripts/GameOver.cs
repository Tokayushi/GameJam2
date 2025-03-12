using UnityEngine;

public class GameOver : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)  
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CambiarEstado(GameManager.GameState.GameOver);
            Debug.Log("El jugador perdió");
        }
    }
}
