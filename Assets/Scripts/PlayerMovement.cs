using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento del Player
    public float mouseSensitivity = 100f;  // Sensibilidad del movimiento del ratón
    public Transform playerBody;  // Referencia al cuerpo del Player (objeto con el script)
    public Transform cameraTransform; // Referencia a la cámara del Player

    private CharacterController controller; // Referencia al CharacterController
    private float xRotation = 0f;
    private bool isLookingBack = false; // Indica si el jugador está mirando hacia atrás

    private Quaternion originalCameraRotation; // Para almacenar la rotación original de la cámara

    void Start()
    {
        // Bloquear el cursor en el centro de la pantalla y ocultarlo
        Cursor.lockState = CursorLockMode.Locked;

        // Obtener el CharacterController del GameObject
        controller = GetComponent<CharacterController>();

        // Verificar si el CharacterController está presente
        if (controller == null)
        {
            Debug.LogError("CharacterController no encontrado. Asegúrate de que el Player tiene un CharacterController.");
        }

        // Verificar si la cámara está asignada
        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform no asignado. Asigna la cámara principal al campo cameraTransform.");
        }

        // Guardar la rotación original de la cámara
        originalCameraRotation = cameraTransform.localRotation;
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
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar la rotación vertical para evitar que la cámara gire completamente

            // Aplicar la rotación de la cámara
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Rotar el cuerpo del Player en el eje horizontal
            playerBody.Rotate(Vector3.up * mouseX);
        }

        // Movimiento del Player con las flechas
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Girar la cámara 180 grados cuando se presiona la tecla E
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

}