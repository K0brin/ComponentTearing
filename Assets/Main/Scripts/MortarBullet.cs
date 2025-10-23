using UnityEngine;

public class MortarBullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MortarHitArea"))
        {
            Destroy(other.gameObject);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
