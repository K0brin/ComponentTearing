using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float currentHealth = 0;
    private float maxHealth = 100;
    [SerializeField] private Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;
    }
}
