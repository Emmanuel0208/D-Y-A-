using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;  // Prefab del proyectil
    public Transform launchPoint;  // Punto de lanzamiento del proyectil
    public float cooldownTime = 2f;  // Tiempo de cooldown en segundos
    public Image cooldownImage;  // Referencia a la imagen de cooldown
    public GameObject ammoPrefab;  // Prefab de la bala para la UI
    public Transform ammoContainer;  // Contenedor para las balas en la UI
    public int maxAmmo = 10;  // N�mero m�ximo de balas
    private int currentAmmo;  // N�mero actual de balas
    public bool canShoot = false;  // Flag para controlar si se puede disparar

    private float cooldownTimer = 0f;  // Temporizador para el cooldown
    private bool isCooldownActive = false;  // Indica si el cooldown est� activo

    void Start()
    {
        currentAmmo = 5;  // Iniciar con 5 municiones
        UpdateAmmoUI();  // Actualizar la UI de balas al inicio
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0f;
            cooldownImage.gameObject.SetActive(false);  // Ocultar la imagen al inicio
        }
    }

    void Update()
    {
        // Controlar el temporizador de cooldown
        if (isCooldownActive)
        {
            cooldownTimer += Time.deltaTime;  // Incrementar el temporizador

            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = cooldownTimer / cooldownTime;  // Actualizar el llenado de la imagen
            }

            if (cooldownTimer >= cooldownTime)
            {
                isCooldownActive = false;  // Finalizar el cooldown
                cooldownTimer = 0f;  // Reiniciar el temporizador
                if (cooldownImage != null)
                {
                    cooldownImage.fillAmount = 0f;  // Restablecer el llenado de la imagen
                    cooldownImage.gameObject.SetActive(false);  // Ocultar la imagen cuando el cooldown termine
                }
            }
        }

        // Disparar si se cumple la condici�n
        if (Input.GetButtonDown("Fire1") && canShoot && !isCooldownActive && currentAmmo > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);  // Crear el proyectil
        currentAmmo--;  // Reducir la cantidad de balas
        UpdateAmmoUI();  // Actualizar la UI de balas
        isCooldownActive = true;  // Iniciar el cooldown
        if (cooldownImage != null)
        {
            cooldownImage.gameObject.SetActive(true);  // Mostrar la imagen cuando se inicie el cooldown
        }
    }

    // Funci�n para a�adir munici�n
    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        UpdateAmmoUI();
    }

    // Funci�n para actualizar la UI de balas
    private void UpdateAmmoUI()
    {
        // Eliminar las balas existentes en la UI
        foreach (Transform child in ammoContainer)
        {
            Destroy(child.gameObject);
        }

        // Instanciar una nueva bala por cada bala actual
        for (int i = 0; i < currentAmmo; i++)
        {
            GameObject newAmmo = Instantiate(ammoPrefab, ammoContainer);

            // Asumimos que la animaci�n est� correctamente configurada en el prefab
            Animator ammoAnimator = newAmmo.GetComponent<Animator>();
            if (ammoAnimator != null)
            {
                ammoAnimator.Play("AmmoAnimation", 0, 0f);  // Reproducir la animaci�n predeterminada
            }
        }
    }

    // Funci�n para recargar balas
    public void Reload()
    {
        currentAmmo = maxAmmo;  // Restablecer la cantidad de balas al m�ximo
        UpdateAmmoUI();  // Actualizar la UI de balas
    }
}
