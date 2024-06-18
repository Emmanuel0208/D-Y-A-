using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Necesario para la gestión de escenas
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Referencia al menú de pausa
    public EnemySpawner enemySpawner;  // Referencia al spawner de enemigos
    private bool isPaused = true;  // Estado inicial del juego (comienza en pausa)
    public PlayerAttack playerAttack;
    public bool canTogglePause = true;  // Controla si se puede pausar/reanudar el juego

    public GameObject panelToShow;  // Referencia al panel que se mostrará

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;  // Comenzar con el cursor libre
        Cursor.visible = true;  // Hacer el cursor visible
        Time.timeScale = 0f;  // Pausar el tiempo del juego
        canTogglePause = false;
    }

    void Update()
    {
        // Detectar si se presiona la tecla 'Q' para pausar o reanudar el juego
        if (Input.GetKeyDown(KeyCode.Q) && canTogglePause)
        {
            canTogglePause = false;
            TogglePause();
        }
    }

    // Alternar entre pausar y reanudar el juego
    private void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    // Función para pausar el juego
    public void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);  // Mostrar el menú de pausa
        Time.timeScale = 0f;  // Pausar el tiempo del juego
        Cursor.lockState = CursorLockMode.None;  // Desbloquear el cursor
        Cursor.visible = true;  // Hacer el cursor visible
        playerAttack.canShoot = false;
    }

    // Función para reanudar el juego
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;  // Reanudar el tiempo del juego
        pauseMenuUI.SetActive(false);  // Ocultar el menú de pausa
        Cursor.lockState = CursorLockMode.Locked;  // Bloquear el cursor
        Cursor.visible = false;  // Ocultar el cursor
        playerAttack.canShoot = true;  // Permitir disparar de nuevo
    }

    // Función para establecer la dificultad y reanudar el juego
    public void SetDifficulty(string difficulty)
    {
        if (difficulty == "Easy")
        {
            enemySpawner.SpawnDijkstra();
        }
        else if (difficulty == "Hard")
        {
            enemySpawner.SpawnAStar();
        }
        ResumeGame();  // Reanudar el juego después de seleccionar la dificultad
    }

    // Función para cerrar el juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");  // Mensaje para el editor
        Application.Quit();
    }

    // Función para reiniciar la escena actual
    public void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Debug.Log("Reiniciando la escena...");
    }

    // Función para salir al menú inicial (escena 0)
    public void ExitToMainMenu()
    {
        Debug.Log("Saliendo al menú principal...");
        SceneManager.LoadScene(0);  // Cargar la escena con el índice 0
    }

    // Función para desactivar la capacidad de pausar/reanudar
    public void DisableTogglePause()
    {
        canTogglePause = false;
        Debug.Log("TogglePause deshabilitado.");
    }

    // Función para activar la capacidad de pausar/reanudar
    public void EnableTogglePause()
    {
        canTogglePause = true;
        Debug.Log("TogglePause habilitado.");
    }

    // Función para mostrar un panel por 5 segundos y luego destruirlo
    public void ShowPanelForSeconds()
    {
        if (panelToShow != null)
        {
            StartCoroutine(ShowAndDestroyPanel(panelToShow, 3f));
        }
        else
        {
            Debug.LogWarning("No se ha asignado ningún panel para mostrar.");
        }
    }

    // Corrutina que muestra un panel y luego lo destruye después de un tiempo
    private IEnumerator ShowAndDestroyPanel(GameObject panel, float seconds)
    {
        panel.SetActive(true);  // Mostrar el panel
        yield return new WaitForSeconds(seconds);  // Esperar el tiempo especificado
        Destroy(panel);  // Destruir el panel
        Debug.Log("Panel destruido después de " + seconds + " segundos.");
    }
}
