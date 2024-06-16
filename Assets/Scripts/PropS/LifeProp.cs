using UnityEngine;

public class LifeProp : MonoBehaviour
{
    public int lifeAmount = 1;  // Cantidad de vida que añade

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.AddHealth(lifeAmount);
                Debug.Log("Vida añadida.");
            }
            Destroy(gameObject);
        }
    }
}
