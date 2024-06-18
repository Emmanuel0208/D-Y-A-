using UnityEngine;
using UnityEngine.UI;  // Necesario para trabajar con UI en Unity
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento del Player
    public float mouseSensitivity = 100f;  // Sensibilidad del movimiento del ratón
    public Transform playerBody;  // Referencia al cuerpo del Player
    public Transform cameraTransform; // Referencia a la cámara del Player
    public Slider sensitivitySlider;  // Slider de sensibilidad del mouse

    private CharacterController controller; // Referencia al CharacterController
    private float xRotation = 0f;
    private bool isLookingBack = false; // Indica si el jugador está mirando hacia atrás

    private float originalMoveSpeed;  // Velocidad original del jugador
    private Coroutine slowDownCoroutine;  // Corrutina activa para ralentizar
    private Coroutine speedBoostCoroutine;  // Corrutina activa para aumento de velocidad

    void Start()
    {
        // Guardar la velocidad original
        originalMoveSpeed = moveSpeed;

        // Obtener el CharacterController del GameObject
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogError("CharacterController no encontrado. Asegúrate de que el Player tiene un CharacterController.");
        }

        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform no asignado. Asigna la cámara principal al campo cameraTransform.");
        }

        // Configurar el slider
        if (sensitivitySlider != null)
        {
            sensitivitySlider.minValue = 0;
            sensitivitySlider.maxValue = 100;
            sensitivitySlider.value = mouseSensitivity;
            sensitivitySlider.onValueChanged.AddListener(delegate { AdjustSensitivity(); });
        }
        else
        {
            Debug.LogWarning("Slider de sensibilidad no asignado.");
        }
    }

    void Update()
    {
        // Movimiento de la cámara con el ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (!isLookingBack)
        {
            // Rotar la cámara en el eje vertical
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        // Movimiento del Player con las flechas
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            LookBack();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            LookForward();
        }
    }

    private void LookBack()
    {
        isLookingBack = true;
        cameraTransform.localRotation = Quaternion.Euler(0f, 180f, 0f);
    }

    private void LookForward()
    {
        isLookingBack = false;
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    // Función para ajustar la sensibilidad del mouse
    public void AdjustSensitivity()
    {
        if (sensitivitySlider != null)
        {
            mouseSensitivity = sensitivitySlider.value;
        }
    }

    // Función para ralentizar al jugador
    public void AdjustSpeed(float multiplier)
    {
        if (slowDownCoroutine != null)
        {
            StopCoroutine(slowDownCoroutine);  // Detener la ralentización activa si existe
        }

        slowDownCoroutine = StartCoroutine(SlowDownCoroutine(multiplier));
    }

    // Corrutina de ralentización
    private IEnumerator SlowDownCoroutine(float multiplier)
    {
        moveSpeed = originalMoveSpeed * multiplier;  // Aplicar el multiplicador para ralentizar la velocidad
        yield return null;
    }

    // Restaurar la velocidad original del jugador
    public void RestoreOriginalSpeed()
    {
        if (slowDownCoroutine != null)
        {
            StopCoroutine(slowDownCoroutine);
            slowDownCoroutine = null;
        }
        moveSpeed = originalMoveSpeed;
    }

    // Función para aumentar la velocidad
    public IEnumerator IncreaseSpeed(float multiplier, float duration)
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);  // Detener el aumento de velocidad activo si existe
        }

        speedBoostCoroutine = StartCoroutine(IncreaseSpeedCoroutine(multiplier, duration));
        yield return speedBoostCoroutine;
    }

    // Corrutina para el aumento de velocidad
    private IEnumerator IncreaseSpeedCoroutine(float multiplier, float duration)
    {
        moveSpeed = originalMoveSpeed * multiplier;  // Aumentar la velocidad
        yield return new WaitForSeconds(duration);  // Esperar la duración del boost
        moveSpeed = originalMoveSpeed;  // Restaurar la velocidad original
    }
}
