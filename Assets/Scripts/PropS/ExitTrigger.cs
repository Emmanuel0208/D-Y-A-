using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public string playerTag = "Player";  // Etiqueta del jugador
    public GameObject noKeyPanel;  // Panel que se muestra si el jugador no tiene la llave
    public GameObject hasKeyPanel;  // Panel que se muestra si el jugador tiene la llave
    public VictoryManager victoryManager;  // Referencia al VictoryManager
    public PlayerAttack PlayerAttack;
    

    private bool playerAtExit = false;  // Flag para controlar si el jugador está en la salida

    void Start()
    {
       
        if (noKeyPanel != null)
        {
            noKeyPanel.SetActive(false);  // Asegurarse de que el panel esté desactivado al inicio
        }
        if (hasKeyPanel != null)
        {
            hasKeyPanel.SetActive(false);  // Asegurarse de que el panel esté desactivado al inicio
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (victoryManager.HasKey())  // Verificar si el jugador tiene la llave
            {
                ShowPanel(hasKeyPanel);
                playerAtExit = true;  // El jugador está en la salida
            }
            else
            {
                ShowPanel(noKeyPanel);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerAtExit = false;  // El jugador ya no está en la salida
            HideAllPanels();
        }
    }

    void Update()
    {
        if (playerAtExit && Input.GetKeyDown(KeyCode.F))
        {
            victoryManager.OnVictoryObjectCollected();
            Time.timeScale = 0f;  // Pausar el tiempo del juego
            Cursor.lockState = CursorLockMode.None;  // Desbloquear el cursor
            Cursor.visible = true;  // Hacer el cursor visible// Llamar a la función de victoria en el VictoryManager
            PlayerAttack.canShoot = false;
            hasKeyPanel.SetActive(false); 
            DestroyObject(hasKeyPanel);
        }
    }

    public void DestroyObject(GameObject target)
    {
        if (target != null)
        {
            Destroy(target);
           
        }
    }
    void ShowPanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    void HideAllPanels()
    {
        if (noKeyPanel != null)
        {
            noKeyPanel.SetActive(false);
        }
        if (hasKeyPanel != null)
        {
            hasKeyPanel.SetActive(false);
        }
    }
}
