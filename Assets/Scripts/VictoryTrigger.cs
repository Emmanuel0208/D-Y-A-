using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    public string playerTag = "Player";  // Etiqueta del jugador
    public string victoryManagerTag = "GraphManager";  // Etiqueta del VictoryManager
    public float rotationSpeed = 50f;  // Velocidad de rotaci�n en grados por segundo
    public float floatAmplitude = 0.5f;  // Amplitud del movimiento de flotaci�n
    public float floatFrequency = 1f;  // Frecuencia del movimiento de flotaci�n
    private Vector3 startPosition;  // Posici�n inicial del objeto
    private VictoryManager victoryManager;  // Referencia al VictoryManager

    void Start()
    {
        // Buscar el VictoryManager por su etiqueta
        GameObject victoryManagerObject = GameObject.FindGameObjectWithTag(victoryManagerTag);
        startPosition = transform.position;
        // Verificar si se encontr� el VictoryManager
        if (victoryManagerObject != null)
        {
            victoryManager = victoryManagerObject.GetComponent<VictoryManager>();
            if (victoryManager == null)
            {
                Debug.LogWarning("No se encontr� el componente VictoryManager en el objeto con la etiqueta " + victoryManagerTag);
            }
        }
        else
        {
            Debug.LogWarning("No se encontr� ning�n objeto con la etiqueta " + victoryManagerTag);
        }
    }

    void Update()
    {
        // Rotar el objeto en el eje Z
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // Movimiento de flotaci�n
        float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisiona tiene la etiqueta del jugador
        if (other.CompareTag(playerTag))
        {
            // Llamar a la funci�n para manejar la victoria en VictoryManager
            if (victoryManager != null)
            {
                victoryManager.OnVictoryObjectCollected();

            }
            else
            {
                Debug.LogWarning("VictoryManager no est� asignado.");
            }
        }
    }
}
