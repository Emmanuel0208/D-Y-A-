using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para la gestión de escenas

public class GameControl : MonoBehaviour
{
    // Método para cambiar a la escena 1
    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

  
    
}
