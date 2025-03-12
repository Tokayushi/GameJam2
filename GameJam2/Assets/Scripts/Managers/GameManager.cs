using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance { get; private set; }

    // Game States
    public enum GameState { Menu, Playing, Paused, GameOver }
    public GameState EstadoActual { get; private set; }

    private void Awake()
    {
        // Singleton Implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Dont destroy when load scenes
        }
        else
        {
            Destroy(gameObject); // If already exist, destroy it.
        }
    }

    private void Start()
    {
        CambiarEstado(GameState.Menu); // Start the menu
    }

    public void CambiarEstado(GameState nuevoEstado)
    {
        EstadoActual = nuevoEstado;
        Debug.Log("Estado cambiado a: " + EstadoActual);

        
        switch (EstadoActual) //add whatever you want that happen in each state
        {
            case GameState.Menu:
                
                break;

            case GameState.Playing:
                
                Time.timeScale = 1f;
                break;

            case GameState.Paused:
                
                Time.timeScale = 0f;
                break;

            case GameState.GameOver:
                
                break;
        }
    }

    public bool EsEstado(GameState estado)
    {
        return EstadoActual == estado;
    }
}
