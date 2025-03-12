using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject tuto;
    [SerializeField] private GameObject[] tutorialParts;


    [SerializeField] bool inTuto;

    public int tutoPart = 0;


    public void NextTutoPart()
    {
        tutoPart++;
        tutorialParts[tutoPart].SetActive(true);
        
    }

    public void PlayTuto()
    {
        tuto.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
