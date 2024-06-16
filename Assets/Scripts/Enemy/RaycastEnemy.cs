using UnityEngine;
using System.Collections;

public class RaycastEnemy : MonoBehaviour
{
    public float detectionRange = 10f;  // Distancia máxima del raycast
    public float moveSpeed = 3f;  // Velocidad de movimiento del enemigo
    public float chaseDuration = 10f;  // Duración de la persecución
    public string playerTag = "Player";  // Etiqueta del jugador
    public string projectileTag = "Projectile";  // Etiqueta del proyectil
    public float rotationSpeed = 5f;  // Velocidad de rotación del enemigo

    public float obstacleAvoidanceDistance = 1f;  // Distancia para evitar obstáculos
    public float sideRayOffset = 0.5f;  // Desplazamiento lateral de los rayos para detectar obstáculos
    public float obstacleAvoidanceStrength = 1.0f;  // Fuerza de evitación de obstáculos

    public Transform spawnPoint;  // Referencia al punto de spawn

    private bool isChasing = false;  // Indica si el enemigo está persiguiendo al jugador
    private Transform playerTransform;  // Referencia al transform del jugador
    private Coroutine chaseCoroutine;  // Referencia a la corrutina de persecución
    private RaycastEnemySpawner raycastEnemySpawner;  // Referencia al spawner de raycast enemies

    void Start()
    {
        // Buscar el spawner de raycast enemies en la escena
        raycastEnemySpawner = FindObjectOfType<RaycastEnemySpawner>();

        // Verificar que el spawnPoint esté asignado
        if (spawnPoint == null)
        {
            Debug.LogError("RaycastEnemy no tiene un punto de spawn asignado.");
        }
    }

    void Update()
    {
        if (!isChasing)
        {
            // Realizar un raycast desde la posición del enemigo hacia adelante
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, detectionRange))
            {
                if (hit.collider.CompareTag(playerTag))
                {
                    // Si el raycast detecta al jugador, iniciar la persecución
                    playerTransform = hit.collider.transform;
                    chaseCoroutine = StartCoroutine(ChasePlayer());
                }
            }
        }
        else
        {
            // Perseguir al jugador
            if (playerTransform != null)
            {
                Vector3 direction = (playerTransform.position - transform.position).normalized;
                Vector3 avoidanceDirection = AvoidObstacles(direction);

                // Mover el enemigo hacia el jugador
                transform.position += avoidanceDirection * moveSpeed * Time.deltaTime;

                // Rotar el enemigo hacia el jugador
                Quaternion targetRotation = Quaternion.LookRotation(avoidanceDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    Vector3 AvoidObstacles(Vector3 moveDirection)
    {
        // Raycast hacia adelante
        if (Physics.Raycast(transform.position, moveDirection, obstacleAvoidanceDistance))
        {
            // Raycast hacia la izquierda
            Vector3 leftRayOrigin = transform.position - transform.right * sideRayOffset;
            if (!Physics.Raycast(leftRayOrigin, transform.forward, obstacleAvoidanceDistance))
            {
                moveDirection += -transform.right * obstacleAvoidanceStrength;
            }
            // Raycast hacia la derecha
            else
            {
                Vector3 rightRayOrigin = transform.position + transform.right * sideRayOffset;
                if (!Physics.Raycast(rightRayOrigin, transform.forward, obstacleAvoidanceDistance))
                {
                    moveDirection += transform.right * obstacleAvoidanceStrength;
                }
            }
        }
        return moveDirection.normalized;
    }

    IEnumerator ChasePlayer()
    {
        isChasing = true;
        yield return new WaitForSeconds(chaseDuration);

        // Si no se colisiona con el jugador, destruir el enemigo y respawnearlo
        if (playerTransform == null || !GetComponent<Collider>().bounds.Intersects(playerTransform.GetComponent<Collider>().bounds))
        {
            DestroyEnemy();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Destruir el enemigo y restar vida al jugador
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            DestroyEnemy();
        }
        else if (collision.gameObject.CompareTag(projectileTag))
        {
            // Destruir el enemigo al colisionar con el proyectil
            Destroy(collision.gameObject);  // Destruir el proyectil
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        if (raycastEnemySpawner != null)
        {
            raycastEnemySpawner.RespawnEnemyWithDelay(spawnPoint);
        }
        Destroy(gameObject);
    }
}
