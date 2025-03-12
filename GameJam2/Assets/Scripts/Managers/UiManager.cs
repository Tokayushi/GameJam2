using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject tuto;
    [SerializeField] private GameObject[] tutorialParts;


    [SerializeField] bool inTuto;

    public int tutoPart = 0;

    private void Start()
    {
        //tuto 
        tuto.SetActive(false);
        tutorialParts[0].SetActive(true);
        tutorialParts[1].SetActive(false);
        tutorialParts[2].SetActive(false);
        tutorialParts[3].SetActive(false);

      
    }

    private void Update()
    {
       

    }

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
