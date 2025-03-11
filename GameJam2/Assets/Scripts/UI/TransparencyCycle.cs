using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransparencyCycle : MonoBehaviour
{
    //Objeto padre menu pasado para desactivarlo
    public GameObject menuScreen;

    // Array de elementos UI (Imágenes, Botones, Textos)
    public Graphic[] uiElements;

    [Range(0f, 1f)] public float minAlpha = 0f;
    [Range(0f, 1f)] public float maxAlpha = 1f;

    //Tiempo de transición de un estado a otro
    public float transitionDuration = 2f;

    //Tiempo que permanece en cada estado (opaco o transparente)
    public float waitTime = 5f;

    void Start()
    {
        // Iniciamos la corrutina que alterna la transparencia de los elementos
        StartCoroutine(ToggleTransparencyLoop());
    }

    IEnumerator ToggleTransparencyLoop()
    {
        while (true) // Bucle infinito
        {
            // Mantener transparencia máxima por un tiempo
            yield return new WaitForSeconds(waitTime);

            // Transición a transparencia mínima
            yield return StartCoroutine(ChangeTransparency(minAlpha, transitionDuration));

            //Cuando la transparencia llegue a 0, desactivamos el objeto padre
            menuScreen.SetActive(false);

            //Mantener el objeto desactivado por el tiempo de espera
            yield return new WaitForSeconds(waitTime);

            //Antes de volver a la transparencia máxima, reactivamos el objeto
            menuScreen.SetActive(true);

            //Transición a transparencia máxima
            yield return StartCoroutine(ChangeTransparency(maxAlpha, transitionDuration));
        }
    }

    IEnumerator ChangeTransparency(float targetAlpha, float duration)
    {
        // Variable para llevar el tiempo transcurrido en la interpolación
        float elapsedTime = 0f;

        // Array para almacenar los colores originales de cada elemento UI
        Color[] startColors = new Color[uiElements.Length];

        // Recorremos el array de elementos UI y guardamos sus colores iniciales
        for (int i = 0; i < uiElements.Length; i++)
        {
            // Verificamos que el elemento no sea nulo antes de acceder a su color
            if (uiElements[i] != null)
            {
                startColors[i] = uiElements[i].color;
            }
        }

        // Iniciamos un bucle que durará hasta que la transición se complete
        while (elapsedTime < duration)
        {
            // Calculamos el nuevo valor de transparencia interpolando entre la transparencia inicial y la final
            float alpha = Mathf.Lerp(startColors[0].a, targetAlpha, elapsedTime / duration);

            // Aplicamos el nuevo valor de transparencia a cada elemento UI
            for (int i = 0; i < uiElements.Length; i++)
            {
                if (uiElements[i] != null)
                {
                    // Creamos un nuevo color basado en el color original, pero con la nueva transparencia
                    Color newColor = startColors[i];
                    newColor.a = alpha;
                    uiElements[i].color = newColor;
                }
            }

            // Aumentamos el tiempo transcurrido con el tiempo de cada frame
            elapsedTime += Time.deltaTime;

            // Esperamos hasta el siguiente frame antes de continuar la interpolación
            yield return null;
        }

        // Aseguramos que el alpha final se establezca exactamente en el valor objetivo
        for (int i = 0; i < uiElements.Length; i++)
        {
            if (uiElements[i] != null)
            {
                Color finalColor = uiElements[i].color;
                finalColor.a = targetAlpha;
                uiElements[i].color = finalColor;
            }
        }
    }


}
