using UnityEngine;

public class SpeedProp : MonoBehaviour
{
    // Multiplicador para aumentar la velocidad del jugador.
    public float speedMultiplier = 1.5f;
    // Duración durante la cual se mantendrá el aumento de velocidad.
    public float duration = 5f;

    // Método que se ejecuta cuando otro objeto entra en el collider de este objeto.
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona tiene la etiqueta "Player".
        if (other.CompareTag("Player"))
        {
            // Intenta obtener el componente PlayerMovement del objeto que colisiona.
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            // Verifica si se encontró el componente PlayerMovement.
            if (playerMovement != null)
            {
                // Inicia la coroutine IncreaseSpeed en el componente PlayerMovement.
                playerMovement.StartCoroutine(playerMovement.IncreaseSpeed(speedMultiplier, duration));
                // Imprime un mensaje en la consola indicando que la velocidad ha sido aumentada.
                Debug.Log($"Velocidad aumentada por {duration} segundos.");

                // Destruye el objeto SpeedProp para simular que ha sido recogido.
                Destroy(gameObject);
            }
        }
    }
}
