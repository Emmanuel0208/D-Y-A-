using UnityEngine;
using UnityEngine.SceneManagement; // Importa el namespace necesario para la gesti�n de escenas

public class GameControl : MonoBehaviour
{
    // M�todo para cargar la escena 1
    public void LoadScene1()
    {
        // SceneManager.LoadScene carga la escena con el �ndice especificado.
        // En este caso, carga la escena con el �ndice 1.
        SceneManager.LoadScene(1);
    }

    // M�todo para salir del juego
    public void QuitGame()
    {
        // Muestra un mensaje en la consola indicando que el juego se est� cerrando.
        Debug.Log("Saliendo del juego...");

        // Application.Quit cierra la aplicaci�n. 
        // Esto funciona solo en la versi�n compilada del juego, no en el editor.
        Application.Quit();
    }
}
