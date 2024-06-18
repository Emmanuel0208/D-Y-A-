using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    // Etiqueta del jugador que debe recoger el objeto.
    public string playerTag = "Player";
    // Etiqueta para identificar el objeto VictoryManager en la escena.
    public string victoryManagerTag = "GraphManager";
    // Velocidad a la que el objeto rotará en grados por segundo.
    public float rotationSpeed = 50f;
    // Amplitud del movimiento de flotación en el eje Y.
    public float floatAmplitude = 0.5f;
    // Frecuencia del movimiento de flotación, controlando cuántas veces sube y baja por segundo.
    public float floatFrequency = 1f;
    // Posición inicial del objeto, para mantener una referencia.
    private Vector3 startPosition;
    // Referencia al componente VictoryManager que gestionará la recogida del objeto.
    private VictoryManager victoryManager;

    void Start()
    {
        // Busca el objeto en la escena que tiene la etiqueta especificada para el VictoryManager.
        GameObject victoryManagerObject = GameObject.FindGameObjectWithTag(victoryManagerTag);
        // Almacena la posición inicial del objeto para el movimiento de flotación.
        startPosition = transform.position;
        // Verifica si se encontró un objeto con la etiqueta del VictoryManager.
        if (victoryManagerObject != null)
        {
            // Intenta obtener el componente VictoryManager del objeto encontrado.
            victoryManager = victoryManagerObject.GetComponent<VictoryManager>();
            // Verifica si se encontró el componente VictoryManager en el objeto.
            if (victoryManager == null)
            {
                // Imprime una advertencia en la consola si no se encontró el componente.
                Debug.LogWarning("No se encontró el componente VictoryManager en el objeto con la etiqueta " + victoryManagerTag);
            }
        }
        else
        {
            // Imprime una advertencia en la consola si no se encontró ningún objeto con la etiqueta.
            Debug.LogWarning("No se encontró ningún objeto con la etiqueta " + victoryManagerTag);
        }
    }

    void Update()
    {
        // Rota el objeto alrededor del eje Z a la velocidad especificada.
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // Calcula la nueva posición Y para el efecto de flotación.
        float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        // Actualiza la posición del objeto para aplicar el movimiento de flotación.
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el trigger tiene la etiqueta del jugador.
        if (other.CompareTag(playerTag))
        {
            // Si se encontró el VictoryManager, llama a la función para registrar la recogida del objeto.
            if (victoryManager != null)
            {
                victoryManager.CollectKey();
                // Destruye el objeto para simular su recogida.
                Destroy(gameObject);
            }
            else
            {
                // Imprime una advertencia en la consola si el VictoryManager no está asignado.
                Debug.LogWarning("VictoryManager no está asignado.");
            }
        }
    }
}
