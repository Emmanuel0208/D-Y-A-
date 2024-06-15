using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public GameObject heartPrefab;  // Prefab del corazón
    public Transform heartsContainer;  // Contenedor para los corazones
    public GameObject gameOverPanel;  // Panel de Game Over
    public PlayerAttack playerAttack;  // Referencia al script de ataque del jugador
    public PlayerMovement playerMovement;  // Referencia al script de movimiento del jugador

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartsUI();
        gameOverPanel.SetActive(false);  // Asegurarse de que el panel de Game Over esté inactivo al inicio
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

    public void AddHealth(int amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
            UpdateHeartsUI();
        }
        else
        {
            // Si la vida está al máximo, otorga un beneficio aleatorio
            GiveRandomBenefit();
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
        playerAttack.canShoot = false;  // Deshabilitar la capacidad de disparar
    }

    private void UpdateHeartsUI()
    {
        // Eliminar los corazones existentes
        foreach (Transform child in heartsContainer)
        {
            Destroy(child.gameObject);
        }

        // Instanciar un nuevo corazón por cada vida actual
        for (int i = 0; i < currentHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartsContainer);
            Animator heartAnimator = newHeart.GetComponent<Animator>();

            if (heartAnimator != null)
            {
                // Reproducir la animación predeterminada del corazón
                heartAnimator.Play("HeartAnimation", 0, 0f);  // Reemplaza "HeartAnimation" con el nombre de tu animación
            }
        }
    }

    private void GiveRandomBenefit()
    {
        int randomBenefit = Random.Range(0, 2);  // Genera un número aleatorio entre 0 y 1

        if (randomBenefit == 0)
        {
            // Recuperar 1 de munición
            playerAttack.AddAmmo(1);
            Debug.Log("Munición añadida en lugar de vida.");
        }
        else
        {
            // Aumentar la velocidad por 5 segundos
            StartCoroutine(playerMovement.IncreaseSpeed(1.5f, 5f));
            Debug.Log("Velocidad aumentada en lugar de vida.");
        }
    }
}
