using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private GameObject[] pastMode; //array of past objects
    private GameObject[] futMode; //array of future objects
    private bool mostrandoPasado = true; //condition to change between past/future mode
    public GameObject panelNegro; // dark panel (to show corrupted effect)
    public float tiempoMaximo = 2f; // time to start corrupted effect
    public float velocidadOscurecimiento = 0.5f; // Velocidad del efecto

    private float contadorTiempo = 0f; //timecounter to control corrupted mode
    private Image imagenPanel; //canva panel propertie to dark screen



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pastMode = GameObject.FindGameObjectsWithTag("present");
        futMode = GameObject.FindGameObjectsWithTag("past");

        ActivarObjetos(pastMode, true);
        ActivarObjetos(futMode, false);

        if (panelNegro != null)
        {
            imagenPanel = panelNegro.GetComponent<Image>();
            imagenPanel.color = new Color(0, 0, 0, 0); //panel color, transparent at init
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) //change mode with T key
        {
            mostrandoPasado = !mostrandoPasado; // switch between past and future

            ActivarObjetos(pastMode, mostrandoPasado); //call activeObject method to past
            ActivarObjetos(futMode, !mostrandoPasado); //call activeObject method to future
        }

        CorruptedMode();
    }



    void ActivarObjetos(GameObject[] objetos, bool estado) //let active and desactive objects in function of mode.
    {
        foreach (GameObject obj in objetos)
        {
            obj.SetActive(estado);
        }
    }

    void CorruptedMode() {
        if (futMode[1].activeSelf)
        {
            contadorTiempo += Time.deltaTime;

            if (contadorTiempo >= tiempoMaximo)
            {
                // Oscurecer progresivamente el panel
                float alpha = Mathf.Clamp01((contadorTiempo - tiempoMaximo) * velocidadOscurecimiento);
                imagenPanel.color = new Color(0, 0, 0, alpha);

                if (alpha >= 1){
                    Debug.Log("GAME OVER");
                }
                
            }

        }
        else
            {
                // Si volvemos al "past mode", reiniciamos el contador y el panel
                contadorTiempo = 0f;
                imagenPanel.color = new Color(0, 0, 0, 0);
            }
    }
}


