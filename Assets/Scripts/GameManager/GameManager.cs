using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Necesario para la gesti�n de escenas

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Referencia al men� de pausa
    public EnemySpawner enemySpawner;  // Referencia al spawner de enemigos
    private bool isPaused = true;  // Estado inicial del juego (comienza en pausa)
    public PlayerAttack playerAttack;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;  // Comenzar con el cursor libre
        Cursor.visible = true;  // Hacer el cursor visible
        Time.timeScale = 0f;  // Pausar el tiempo del juego
    }

    void Update()
    {
        // Detectar si se presiona la tecla 'Q' para pausar o reanudar el juego
        if (Input.GetKeyDown(KeyCode.Q))
        {
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

    // Funci�n para pausar el juego
    public void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);  // Mostrar el men� de pausa
        Time.timeScale = 0f;  // Pausar el tiempo del juego
        Cursor.lockState = CursorLockMode.None;  // Desbloquear el cursor
        Cursor.visible = true;  // Hacer el cursor visible
        playerAttack.canShoot = false;
    }

    // Funci�n para reanudar el juego
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;  // Reanudar el tiempo del juego
        pauseMenuUI.SetActive(false);  // Ocultar el men� de pausa
        Cursor.lockState = CursorLockMode.Locked;  // Bloquear el cursor
        Cursor.visible = false;  // Ocultar el cursor
        playerAttack.canShoot = true;  // Permitir disparar de nuevo
    }

    // Funci�n para establecer la dificultad y reanudar el juego
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
        ResumeGame();  // Reanudar el juego despu�s de seleccionar la dificultad
    }

    // Funci�n para cerrar el juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");  // Mensaje para el editor
        Application.Quit();
    }

    // Funci�n para reiniciar la escena actual
    public void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Debug.Log("Reiniciando la escena...");
    }

    // Funci�n para salir al men� inicial (escena 0)
    public void ExitToMainMenu()
    {
        Debug.Log("Saliendo al men� principal...");
        SceneManager.LoadScene(0);  // Cargar la escena con el �ndice 0
    }
}
