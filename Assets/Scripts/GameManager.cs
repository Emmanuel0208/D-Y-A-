using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Referencia al menú de pausa
    public EnemySpawner enemySpawner;  // Referencia al spawner de enemigos
    private bool isPaused = true;  // Estado inicial del juego (comienza en pausa)

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;  // Comenzar con el cursor libre
        Cursor.visible = true;  // Hacer el cursor visible
        Time.timeScale = 0f;  // Pausar el tiempo del juego
       
    }

    void Update()
    {
        // Detectar si se presiona la tecla 'P' para pausar o reanudar el juego
        if (Input.GetKeyDown(KeyCode.P))
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

    // Función para pausar el juego
    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;  // Pausar el tiempo del juego
        pauseMenuUI.SetActive(true);  // Mostrar el menú de pausa
        Cursor.lockState = CursorLockMode.None;  // Desbloquear el cursor
        Cursor.visible = true;  // Hacer el cursor visible
    }

    // Función para reanudar el juego
    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;  // Reanudar el tiempo del juego
        pauseMenuUI.SetActive(false);  // Ocultar el menú de pausa
        Cursor.lockState = CursorLockMode.Locked;  // Bloquear el cursor
        Cursor.visible = false;  // Ocultar el cursor
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
}
