using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using StarterAssets;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float currentHealth = 0;
    private float maxHealth = 100;
    [SerializeField] private Slider healthBar;
    public PlayerInputs playerControls;
    private InputAction attack;

    void Awake()
    {
        playerControls = new PlayerInputs();
    }

    void OnEnable()
    {
        attack = playerControls.Player.Attack;
        attack.Enable();
        attack.performed += Attack;
    }

    void OnDisable()
    {
        attack.Disable();
    }

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

    private void Attack(InputAction.CallbackContext context)
    {
        //attack function
        Debug.Log("Attacked");
    }
}
