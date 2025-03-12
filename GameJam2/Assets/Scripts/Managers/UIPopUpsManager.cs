using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPopUpsManager : MonoBehaviour
{
    [SerializeField] GameObject pantallaVictoria, pantallaDerrota, pantallaPausa;

    private bool isPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.CambiarEstado(GameManager.GameState.Playing);

        //pantallas de victoria/derrota
        pantallaDerrota.SetActive(false);
        pantallaVictoria.SetActive(false);
        pantallaPausa.SetActive(false);

        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.EstadoActual == GameManager.GameState.Win)
        {
            pantallaVictoria.SetActive(true);
        }

        if (GameManager.Instance.EstadoActual == GameManager.GameState.GameOver)
        {
            pantallaDerrota.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.P)) 
        {
            isPaused=!isPaused;

            if (isPaused == true)
            {
                pantallaPausa.SetActive(true) ;
                GameManager.Instance.CambiarEstado(GameManager.GameState.Paused);
            }
            if (isPaused == false)
            {
                pantallaPausa.SetActive(false);
                GameManager.Instance.CambiarEstado(GameManager.GameState.Playing);
            }
        }
    }

    public void ResumeGame()
    {
        isPaused = !isPaused;

        if (isPaused == true)
        {
            pantallaPausa.SetActive(true);
            GameManager.Instance.CambiarEstado(GameManager.GameState.Paused);
        }
        if (isPaused == false)
        {
            pantallaPausa.SetActive(false);
            GameManager.Instance.CambiarEstado(GameManager.GameState.Playing);
        }
    }

    public void RestarGame()
    {
        SceneManager.LoadScene(1);
        GameManager.Instance.CambiarEstado(GameManager.GameState.Playing);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
