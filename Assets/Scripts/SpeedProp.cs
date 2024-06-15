using UnityEngine;

public class SpeedProp : MonoBehaviour
{
    public float speedMultiplier = 1.5f;  // Multiplicador de velocidad
    public float duration = 5f;  // Duración del boost en segundos

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // Iniciar la coroutine de aumento de velocidad en el jugador
                playerMovement.StartCoroutine(playerMovement.IncreaseSpeed(speedMultiplier, duration));
                Debug.Log($"Velocidad aumentada por {duration} segundos.");

                Destroy(gameObject);  // Destruir el prop de velocidad
            }
        }
    }
}
