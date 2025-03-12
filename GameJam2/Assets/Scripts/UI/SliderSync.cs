using UnityEngine;
using UnityEngine.UI;

public class SliderSync : MonoBehaviour
{
    // Sliders que queremos sincronizar
    [SerializeField] public Slider sliderPastMusic, sliderPastSFX, sliderFutureMusic, sliderFutureSFX;

    // Bandera para evitar actualizaciones infinitas entre sliders
    private bool isUpdating = false;

    void Start()
    {
        // Verificamos que todos los sliders estén asignados en el Inspector antes de continuar
        if (sliderPastMusic != null && sliderPastSFX != null &&
            sliderFutureMusic != null && sliderFutureSFX != null)
        {
            // Inicialmente, igualamos los valores de los sliders del pasado con los del futuro
            sliderPastMusic.value = sliderFutureMusic.value;
            sliderPastSFX.value = sliderFutureSFX.value;

            // Nos suscribimos a los eventos onValueChanged de cada slider para que se actualicen entre sí
            sliderPastMusic.onValueChanged.AddListener(UpdateFutureMusic);
            sliderFutureMusic.onValueChanged.AddListener(UpdatePastMusic);
            sliderPastSFX.onValueChanged.AddListener(UpdateFutureSFX);
            sliderFutureSFX.onValueChanged.AddListener(UpdatePastSFX);
        }
        else
        {
            // Si falta asignar algún slider, mostramos un mensaje de error en la consola
            Debug.LogError("Uno o más sliders no están asignados en el inspector.");
        }
    }

    // Función que actualiza sliderFutureMusic cuando sliderPastMusic cambia
    public void UpdateFutureMusic(float value)
    {
        // Si ya estamos actualizando otro slider, evitamos una actualización en bucle
        if (!isUpdating && sliderFutureMusic != null)
        {
            isUpdating = true; // Activamos la bandera para evitar loops
            sliderFutureMusic.value = value; // Asignamos el nuevo valor
            AudioManager.Instance.MusicVolumeControl(value); // Actualizamos el volumen de la música
            isUpdating = false; // Desactivamos la bandera para permitir futuras actualizaciones
        }
    }
    public void UpdateMusicVolume()
    {
        if (sliderPastMusic != null)
        {
            AudioManager.Instance.MusicVolumeControl(sliderPastMusic.value);
        }
    }

    public void UpdateSfxVolume()
    {
        if (sliderPastSFX != null)
        {
            AudioManager.Instance.SFXVolumeControl(sliderPastSFX.value);
        }
    }
    // Función que actualiza sliderPastMusic cuando sliderFutureMusic cambia
    public void UpdatePastMusic(float value)
    {
        if (!isUpdating && sliderPastMusic != null)
        {
            isUpdating = true;
            sliderPastMusic.value = value;
            AudioManager.Instance.MusicVolumeControl(value);
            isUpdating = false;
        }
    }

    // Función que actualiza sliderFutureSFX cuando sliderPastSFX cambia
    void UpdateFutureSFX(float value)
    {
        if (!isUpdating && sliderFutureSFX != null)
        {
            isUpdating = true;
            sliderFutureSFX.value = value;
            AudioManager.Instance.SFXVolumeControl(value);
            isUpdating = false;
        }
    }

    // Función que actualiza sliderPastSFX cuando sliderFutureSFX cambia
    void UpdatePastSFX(float value)
    {
        if (!isUpdating && sliderPastSFX != null)
        {
            isUpdating = true;
            sliderPastSFX.value = value;
            AudioManager.Instance.SFXVolumeControl(value);
            isUpdating = false;
        }
    }

    void OnDestroy()
    {
        // Eliminamos las suscripciones para evitar errores cuando el objeto se destruya
        if (sliderPastMusic != null) sliderPastMusic.onValueChanged.RemoveListener(UpdateFutureMusic);
        if (sliderFutureMusic != null) sliderFutureMusic.onValueChanged.RemoveListener(UpdatePastMusic);
        if (sliderPastSFX != null) sliderPastSFX.onValueChanged.RemoveListener(UpdateFutureSFX);
        if (sliderFutureSFX != null) sliderFutureSFX.onValueChanged.RemoveListener(UpdatePastSFX);
    }
}
