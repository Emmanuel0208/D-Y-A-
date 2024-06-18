using UnityEngine;

public class SlowObstacle : MonoBehaviour
{
    // Multiplicador de la velocidad para ralentizar al jugador.
    // Un valor de 0.5 reduce la velocidad a la mitad.
    public float slowMultiplier = 0.5f;
    // Etiqueta que identifica al jugador en la escena.
    public string playerTag = "Player";

    // Método que se ejecuta cuando otro objeto entra en el collider de este objeto.
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el trigger tiene la etiqueta "Player".
        if (other.CompareTag(playerTag))
        {
            // Intenta obtener el componente PlayerMovement del objeto que colisiona.
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            // Verifica si se encontró el componente PlayerMovement.
            if (playerMovement != null)
            {
                // Llama al método AdjustSpeed del PlayerMovement para reducir la velocidad.
                playerMovement.AdjustSpeed(slowMultiplier);
            }
        }
    }

    // Método que se ejecuta cuando otro objeto sale del collider de este objeto.
    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale del trigger tiene la etiqueta "Player".
        if (other.CompareTag(playerTag))
        {
            // Intenta obtener el componente PlayerMovement del objeto que colisiona.
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            // Verifica si se encontró el componente PlayerMovement.
            if (playerMovement != null)
            {
                // Llama al método RestoreOriginalSpeed del PlayerMovement para restaurar la velocidad original.
                playerMovement.RestoreOriginalSpeed();
            }
        }
    }
}
