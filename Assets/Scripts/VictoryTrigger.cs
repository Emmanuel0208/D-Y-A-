using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    public string playerTag = "Player";  // Etiqueta del jugador
    public string victoryManagerTag = "GraphManager";  // Etiqueta del VictoryManager

    private VictoryManager victoryManager;  // Referencia al VictoryManager

    void Start()
    {
        // Buscar el VictoryManager por su etiqueta
        GameObject victoryManagerObject = GameObject.FindGameObjectWithTag(victoryManagerTag);

        // Verificar si se encontró el VictoryManager
        if (victoryManagerObject != null)
        {
            victoryManager = victoryManagerObject.GetComponent<VictoryManager>();
            if (victoryManager == null)
            {
                Debug.LogWarning("No se encontró el componente VictoryManager en el objeto con la etiqueta " + victoryManagerTag);
            }
        }
        else
        {
            Debug.LogWarning("No se encontró ningún objeto con la etiqueta " + victoryManagerTag);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisiona tiene la etiqueta del jugador
        if (other.CompareTag(playerTag))
        {
            // Llamar a la función para manejar la victoria en VictoryManager
            if (victoryManager != null)
            {
                victoryManager.OnVictoryObjectCollected();

            }
            else
            {
                Debug.LogWarning("VictoryManager no está asignado.");
            }
        }
    }
}
