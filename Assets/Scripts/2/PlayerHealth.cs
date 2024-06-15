using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public GameObject heartPrefab;  // Prefab del coraz�n
    public Transform heartsContainer;  // Contenedor para los corazones
    public GameObject gameOverPanel;  // Panel de Game Over
    public PlayerAttack playerAttack;

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
        Cursor.lockState = CursorLockMode.None;  // Desbloquear el cursor
        Cursor.visible = true;  // Hacer el cursor visible
        Time.timeScale = 0f;  // Pausar el tiempo del juego
        // deshabilitar la capacidad de disparar en el Player
        playerAttack.canShoot = false;



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
            GameObject newHeart = Instantiate(heartPrefab, heartsContainer);
            Animator heartAnimator = newHeart.GetComponent<Animator>();

            if (heartAnimator != null)
            {
                // Reproducir la animaci�n predeterminada del coraz�n
                heartAnimator.Play("HeartAnimation", 0, 0f);  // Reemplaza "HeartAnimation" con el nombre de tu animaci�n
            }
        }
    }
}
