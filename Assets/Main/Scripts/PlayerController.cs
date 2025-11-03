using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using StarterAssets;
using Unity.VisualScripting;
using UnityEditor;
using System.Collections;
using System.ComponentModel;
using UnityEditor.SearchService;
public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float currentHealth = 0;
    private float maxHealth = 100;
    [SerializeField] private Slider healthBar;
    [SerializeField] private float bowDamage;
    public PlayerInputs playerControls;
    private InputAction attack;
    private InputAction aim;

    private Animator playerAnimator;
    private bool canAttack;
    private ThirdPersonController playerMovementController;
    private bool aiming;
    private GameObject crosshair;
    private ScreenManager sceneManager;


    void Awake()
    {
        playerControls = new PlayerInputs();
    }

    void OnEnable()
    {
        attack = playerControls.Player.Attack;
        attack.Enable();
        attack.performed += Attack;

        aim = playerControls.Player.Aim;
        aim.Enable();
        aim.performed += Aim;
    }

    void OnDisable()
    {
        attack.Disable();
        aim.Disable();
    }

    void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ScreenManager>();
        playerAnimator = GetComponent<Animator>();
        playerMovementController = GetComponent<ThirdPersonController>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        crosshair.SetActive(false);

        currentHealth = maxHealth;
        Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
        canAttack = false;
        aiming = false;
    }

    void Update()
    {
        if (aiming)
        {
            crosshair.SetActive(true);
            PlayerLookForward();
        }

        if (IsDead())
        {
            sceneManager.LoseMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Destroy(this.gameObject);
        }

    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        Mathf.Clamp(currentHealth, 0f, maxHealth);
        StartCoroutine(RegenHealth());
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;
    }

    private void Attack(InputAction.CallbackContext context)
    {
        //attack function
        if (canAttack)
        {
            playerAnimator.SetTrigger("Attack");
            canAttack = false;
            aiming = false;
            crosshair.SetActive(false);
            playerMovementController.MoveSpeed = 4f;
            playerMovementController.SprintSpeed = 4f;
        }
    }

    private void Aim(InputAction.CallbackContext context)
    {
        playerAnimator.SetTrigger("Aim");
        //disable movement
        playerMovementController.MoveSpeed = 0f;
        playerMovementController.SprintSpeed = 0f;
        aiming = true;
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

    public void CanAttack()
    {
        canAttack = true;
    }

    IEnumerator RegenHealth()
    {
        float storedHealth = currentHealth;
        yield return new WaitForSeconds(5);
        if (storedHealth == currentHealth)
        {
            currentHealth = maxHealth;
            UpdateHealthBar();
        }
    }
    
    private bool IsDead()
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward);
    }
}
