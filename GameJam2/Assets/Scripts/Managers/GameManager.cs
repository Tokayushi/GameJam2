using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance { get; private set; }

    // Game States
    public enum GameState { Menu, Playing, Paused, GameOver, Win }
    public GameState EstadoActual { get; private set; }

    // Victory and Defeat Flags
    public bool gano { get; private set; }
    public bool perdio { get; private set; }

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

        // Reset victory and defeat states
        gano = false;
        perdio = false;

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
                perdio = true;
                break;

            case GameState.Win:
                gano = true;
                break;
        }
    }

    public bool EsEstado(GameState estado)
    {
        return EstadoActual == estado;
    }
}
