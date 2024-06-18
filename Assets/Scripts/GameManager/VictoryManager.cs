using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    public Transform[] spawnPoints;  // Array de puntos de spawn
    public GameObject victoryObjectPrefab;  // Prefab del objeto de victoria
    public GameObject victoryPanel;  // Panel de victoria
    public PlayerAttack playerAttack;
    public GameObject keyIcon;  // Icono de la llave en la UI
    public GameManager gameManager;
    private bool hasKey = false;  // Flag para saber si el jugador tiene la llave
    private GameObject currentVictoryObject;  // Referencia al objeto de victoria actual

    void Start()
    {
        // Iniciar el juego con el panel de victoria y el icono de la llave desactivados
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
        if (keyIcon != null)
        {
            keyIcon.SetActive(false);
        }

        // Generar el objeto de victoria en un punto de spawn aleatorio
        SpawnVictoryObject();
    }

    void SpawnVictoryObject()
    {
        if (victoryObjectPrefab != null && spawnPoints.Length > 0)
        {
            // Elegir un punto de spawn aleatorio
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Establecer la rotación inicial (90 grados en el eje X)
            Quaternion initialRotation = Quaternion.Euler(90f, 0f, 0f);

            // Instanciar el objeto de victoria con la rotación inicial
            currentVictoryObject = Instantiate(victoryObjectPrefab, spawnPoint.position, initialRotation);
        }
    }

    public void CollectKey()
    {
        hasKey = true;
        if (keyIcon != null)
        {
            keyIcon.SetActive(true);  // Mostrar el icono de la llave en la UI
        }
        Debug.Log("Llave recogida");
    }

    public bool HasKey()
    {
        return hasKey;
    }

    public void OnVictoryObjectCollected()
    {
        // Destruir el objeto de victoria actual
        if (currentVictoryObject != null)
        {
            Destroy(currentVictoryObject);
        }

        // Mostrar el panel de victoria
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;  // Desbloquear el cursor
            Cursor.visible = true;  // Hacer el cursor visible
            Time.timeScale = 0f;  // Pausar el tiempo del juego      
            // deshabilitar la capacidad de disparar en el Player
            playerAttack.canShoot = false;
            gameManager.canTogglePause = false;
        }
    }
}
