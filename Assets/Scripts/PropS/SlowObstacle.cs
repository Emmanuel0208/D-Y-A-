using UnityEngine;

public class SlowObstacle : MonoBehaviour
{
    public float slowMultiplier = 0.5f;  // Multiplicador de la velocidad (por ejemplo, 0.5 para reducir la velocidad a la mitad)
    public string playerTag = "Player";  // Etiqueta del jugador

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))  // Verificar si el objeto que entra en el trigger tiene la etiqueta "Player"
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.AdjustSpeed(slowMultiplier);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))  // Verificar si el objeto que sale del trigger tiene la etiqueta "Player"
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.RestoreOriginalSpeed();
            }
        }
    }
}
