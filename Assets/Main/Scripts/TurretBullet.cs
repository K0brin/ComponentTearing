using System.Security.Cryptography;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{

    [SerializeField] float bulletDamage = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>()
                .TakeDamage(bulletDamage);

            Destroy(this.gameObject);
        }

    }
}
