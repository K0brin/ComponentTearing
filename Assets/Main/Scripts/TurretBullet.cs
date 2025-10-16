using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("once");
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

    }
}
