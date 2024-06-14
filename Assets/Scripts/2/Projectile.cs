using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;  // Velocidad del proyectil
    public float lifetime = 5f;  // Duración de vida del proyectil

    private void Start()
    {
        // Destruir el proyectil después de que pase su tiempo de vida
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Mover el proyectil hacia adelante
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el proyectil colisiona con un enemigo etiquetado como "Astar" o "Dijkstra"
        if (collision.gameObject.CompareTag("Astar") || collision.gameObject.CompareTag("Dijkstra"))
        {
            // Destruir al enemigo y al proyectil
            Destroy(collision.gameObject);
            Destroy(gameObject);  // Destruir el proyectil

            
        }
        else
        {
            // Destruir el proyectil al colisionar con cualquier otro objeto
            Destroy(gameObject);
        }
    }
}
