using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public GameObject heartPrefab;  // Prefab del coraz�n
    public Transform heartsContainer;  // Contenedor para los corazones
    public GameObject gameOverPanel;  // Panel de Game Over

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartsUI();
        gameOverPanel.SetActive(false);  // Asegurarse de que el panel de Game Over est� inactivo al inicio
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHeartsUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Mostrar el panel de Game Over
        gameOverPanel.SetActive(true);
        Debug.Log("Jugador ha muerto");
        // Aqu� puedes a�adir l�gica adicional como reiniciar el nivel o salir al men� principal
    }

    private void UpdateHeartsUI()
    {
        // Eliminar los corazones existentes
        foreach (Transform child in heartsContainer)
        {
            Destroy(child.gameObject);
        }

        // Instanciar un nuevo coraz�n por cada vida actual
        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(heartPrefab, heartsContainer);
        }
    }
}
