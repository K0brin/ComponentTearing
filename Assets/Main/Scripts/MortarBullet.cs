using UnityEngine;

public class MortarBullet : MonoBehaviour
{

    [SerializeField] private float mortarDamage;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>()
                .TakeDamage(mortarDamage);
        }
        
        if (other.CompareTag("MortarHitArea"))
        {
            Destroy(other.gameObject);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
