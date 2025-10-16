using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float health = 0;
    private float maxHealth = 100;

    void Start()
    {
        health = maxHealth;
        Mathf.Clamp(health, 0f, maxHealth);
    }

    private void TakeDamage(float damageToTake)
    {
        health -= damageToTake;
        Mathf.Clamp(health, 0f, maxHealth);
    }
}
