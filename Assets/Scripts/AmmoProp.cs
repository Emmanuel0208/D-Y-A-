using UnityEngine;

public class AmmoProp : MonoBehaviour
{
    public int ammoAmount = 3;  // Cantidad de municiones que a�ade

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAttack playerAttack = other.GetComponent<PlayerAttack>();
            if (playerAttack != null && ammoAmount > 0)  // Validar que ammoAmount sea mayor a cero
            {
                playerAttack.AddAmmo(ammoAmount);
                Debug.Log($"Munici�n a�adida: {ammoAmount}.");
            }
            Destroy(gameObject);  // Destruir el prop de munici�n
        }
    }
}
