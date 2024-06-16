using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para la gesti�n de escenas

public class GameControl : MonoBehaviour
{
    // M�todo para cambiar a la escena 1
    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }

    // M�todo para salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

  
    
}
