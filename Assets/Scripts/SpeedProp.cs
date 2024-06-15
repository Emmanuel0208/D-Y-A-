using UnityEngine;

public class SpeedProp : MonoBehaviour
{
    public float speedMultiplier = 1.5f;  // Multiplicador de velocidad
    public float duration = 5f;  // Duración del efecto

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                StartCoroutine(playerMovement.IncreaseSpeed(speedMultiplier, duration));
                Debug.Log("Velocidad aumentada.");
            }
            Destroy(gameObject);
        }
    }
}
