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
    public int maxAmmo = 10;  // Número máximo de balas
    private int currentAmmo;  // Número actual de balas
    public bool canShoot = false;  // Flag para controlar si se puede disparar

    private float cooldownTimer = 0f;  // Temporizador para el cooldown
    private bool isCooldownActive = false;  // Indica si el cooldown está activo

    void Start()
    {
        currentAmmo = maxAmmo;  // Inicializar la cantidad de balas
        UpdateAmmoUI();  // Actualizar la UI de balas al inicio
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0f;
            cooldownImage.gameObject.SetActive(false);  // Ocultar la imagen al inicio
        }
    }

    void Update()
    {
        if (isCooldownActive)
        {
            cooldownTimer += Time.deltaTime;  // Aumentar el temporizador

            // Actualizar la cantidad de llenado de la imagen
            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = cooldownTimer / cooldownTime;
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

        if (Input.GetButtonDown("Fire1") && canShoot && !isCooldownActive && currentAmmo > 0)  // Asume que el botón de disparo está configurado como "Fire1" en Input
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
        currentAmmo--;  // Reducir la cantidad de balas
        UpdateAmmoUI();  // Actualizar la UI de balas
        isCooldownActive = true;  // Iniciar el cooldown
        if (cooldownImage != null)
        {
            cooldownImage.gameObject.SetActive(true);  // Mostrar la imagen cuando se inicie el cooldown
        }
    }

    // Función para actualizar la UI de balas
    private void UpdateAmmoUI()
    {
        // Eliminar las balas existentes
        foreach (Transform child in ammoContainer)
        {
            Destroy(child.gameObject);
        }

        // Instanciar una nueva bala por cada bala actual
        for (int i = 0; i < currentAmmo; i++)
        {
            GameObject newAmmo = Instantiate(ammoPrefab, ammoContainer);
            Animator ammoAnimator = newAmmo.GetComponent<Animator>();

            if (ammoAnimator != null)
            {
                // Verificar que el Animator Controller esté asignado y que la animación exista
                RuntimeAnimatorController animatorController = ammoAnimator.runtimeAnimatorController;
                if (animatorController != null)
                {
                    bool hasAmmoAnimation = false;

                    // Verificar si la animación está en el Animator Controller
                    foreach (AnimationClip clip in animatorController.animationClips)
                    {
                        if (clip.name == "AmmoAnimation")  // Reemplaza "AmmoAnimation" con el nombre de tu animación
                        {
                            hasAmmoAnimation = true;
                            break;
                        }
                    }

                    if (hasAmmoAnimation)
                    {
                        ammoAnimator.Play("AmmoAnimation");  // Reemplaza "AmmoAnimation" con el nombre de tu animación
                    }
                    else
                    {
                        Debug.LogWarning("No se encontró la animación 'AmmoAnimation' en el Animator Controller.");
                    }
                }
                else
                {
                    Debug.LogWarning("El Animator Controller no está asignado en el prefab de la bala.");
                }
            }
            else
            {
                Debug.LogWarning("No se encontró el componente Animator en el prefab de la bala.");
            }
        }
    }

    // Función para recargar balas (puede ser llamada por un objeto de recarga o una acción)
    public void Reload()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }
}
