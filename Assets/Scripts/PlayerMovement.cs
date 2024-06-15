using UnityEngine;
using System.Collections;  // Aseg�rate de que este espacio de nombres est� presente

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento del Player
    public float mouseSensitivity = 100f;  // Sensibilidad del movimiento del rat�n
    public Transform playerBody;  // Referencia al cuerpo del Player
    public Transform cameraTransform; // Referencia a la c�mara del Player

    private CharacterController controller; // Referencia al CharacterController
    private float xRotation = 0f;
    private bool isLookingBack = false; // Indica si el jugador est� mirando hacia atr�s
    private float originalMoveSpeed; // Para almacenar la velocidad original del jugador

    private Quaternion originalCameraRotation; // Para almacenar la rotaci�n original de la c�mara

    void Start()
    {
        // No desactivar ni bloquear el cursor aqu�, mantenerlo libre y visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Obtener el CharacterController del GameObject
        controller = GetComponent<CharacterController>();

        // Verificar si el CharacterController est� presente
        if (controller == null)
        {
            Debug.LogError("CharacterController no encontrado. Aseg�rate de que el Player tiene un CharacterController.");
        }

        // Verificar si la c�mara est� asignada
        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform no asignado. Asigna la c�mara principal al campo cameraTransform.");
        }

        // Guardar la rotaci�n original de la c�mara
        originalCameraRotation = cameraTransform.localRotation;

        // Guardar la velocidad original del jugador
        originalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        // Movimiento de la c�mara con el rat�n
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (!isLookingBack)
        {
            // Rotar la c�mara en el eje vertical
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar la rotaci�n vertical

            // Aplicar la rotaci�n de la c�mara
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Rotar el cuerpo del Player en el eje horizontal
            playerBody.Rotate(Vector3.up * mouseX);
        }

        // Movimiento del Player con las flechas
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Girar la c�mara 180 grados cuando se presiona la tecla E
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
        cameraTransform.localRotation = originalCameraRotation;
    }

    public IEnumerator IncreaseSpeed(float multiplier, float duration)
    {
        moveSpeed *= multiplier;  // Aumentar la velocidad
        yield return new WaitForSeconds(duration);  // Esperar el tiempo de duraci�n
        moveSpeed = originalMoveSpeed;  // Restablecer la velocidad original
    }
}
