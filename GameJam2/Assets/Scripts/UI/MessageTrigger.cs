using UnityEngine;
using TMPro;

public class MessageTrigger : MonoBehaviour
{
    public GameObject messagePanel; // Panel de la UI que se activa/desactiva
    private TextMeshProUGUI messageText; // Referencia al texto del panel

    private void Start()
    {
        if (messagePanel != null)
        {
            messageText = messagePanel.GetComponentInChildren<TextMeshProUGUI>(); // Busca el TMP dentro del panel
            messagePanel.SetActive(false); // Asegurar que inicie oculto
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Detecta al jugador
        {
            messagePanel.SetActive(true); // Activa el panel (el texto ya está configurado en la UI)
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Cuando el jugador se aleja
        {
            messagePanel.SetActive(false); // Oculta el panel
        }
    }
}




