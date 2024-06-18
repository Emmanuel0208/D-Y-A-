using UnityEngine;
using UnityEngine.SceneManagement; // Importa el namespace necesario para la gestión de escenas

public class GameControl : MonoBehaviour
{
    // Método para cargar la escena 1
    public void LoadScene1()
    {
        // SceneManager.LoadScene carga la escena con el índice especificado.
        // En este caso, carga la escena con el índice 1.
        SceneManager.LoadScene(1);
    }

    // Método para salir del juego
    public void QuitGame()
    {
        // Muestra un mensaje en la consola indicando que el juego se está cerrando.
        Debug.Log("Saliendo del juego...");

        // Application.Quit cierra la aplicación. 
        // Esto funciona solo en la versión compilada del juego, no en el editor.
        Application.Quit();
    }
}
