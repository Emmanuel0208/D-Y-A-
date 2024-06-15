using UnityEngine;

public class AmmoProp : MonoBehaviour
{
    public int ammoAmount = 3;  // Cantidad de municiones que añade

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAttack playerAttack = other.GetComponent<PlayerAttack>();
            if (playerAttack != null && ammoAmount > 0)  // Validar que ammoAmount sea mayor a cero
            {
                playerAttack.AddAmmo(ammoAmount);
                Debug.Log($"Munición añadida: {ammoAmount}.");
            }
            Destroy(gameObject);  // Destruir el prop de munición
        }
    }
}
