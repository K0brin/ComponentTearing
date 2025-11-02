using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using StarterAssets;
using Unity.VisualScripting;
using UnityEditor;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float currentHealth = 0;
    private float maxHealth = 100;
    [SerializeField] private Slider healthBar;
    [SerializeField] private float bowDamage;
    public PlayerInputs playerControls;
    private InputAction attack;

    private Animator playerAnimator;


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
        playerAnimator = GetComponent<Animator>();

        currentHealth = maxHealth;
        Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    void Update()
    {
        PlayerLookForward();
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
        playerAnimator.SetTrigger("Attack");
    }

    private void PlayerLookForward()
    {
        Vector3 cameraForward = GameObject.FindGameObjectWithTag("MainCamera").transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        transform.rotation = Quaternion.LookRotation(cameraForward);
    }

    public void Launch()
    {
        //Make raycast from camera center forward
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.layer == 3)
            {

                hit.collider.GetComponent<Components>().TakeDamage(bowDamage);

            }
            else
            {
                Debug.Log("Missed");
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward);
    }
}
