using System.Collections;
using UnityEngine;

public class CoreComponent : Components
{

    //TODO as core takes damage it will slow down robot

    public bool coreShown;
    public bool showCore;
    [SerializeField] private float openCoreTime;
    private int numberOfAliveComponents;

    private TurretComponent turretComponent;
    private MortarComponent mortarComponent;
    private FlamethrowerComponent flamethrowerComponent;
    private ScreenManager screenManager;

    void Start()
    {
        base.Start();
        coreShown = false;
        numberOfAliveComponents = 3;
        Invincible(true);

        turretComponent = GameObject.FindGameObjectWithTag("Turret").GetComponent<TurretComponent>();
        mortarComponent = GameObject.FindGameObjectWithTag("Mortar").GetComponent<MortarComponent>();
        flamethrowerComponent = GameObject.FindGameObjectWithTag("Flamethrower").GetComponent<FlamethrowerComponent>();
        screenManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ScreenManager>();
    }

    void Update()
    {
        base.Update();

        if (showCore && numberOfAliveComponents > 0)
        {
            StartCoroutine(ShowCore());
            numberOfAliveComponents--;
            showCore = false;


        }
        else if(numberOfAliveComponents <= 0)
        {
            coreShown = true;
            Invincible(false);
        }

        if (CoreIsDead())
        {
            screenManager.WinMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    IEnumerator ShowCore()
    {
        coreShown = true;
        Invincible(false);
        SupportComponentsInvincible(true);
        yield return new WaitForSeconds(openCoreTime);
        Debug.Log(openCoreTime);
        Invincible(true);
        SupportComponentsInvincible(false);
        coreShown = false;

    }

    private void SupportComponentsInvincible(bool input)
    {
        turretComponent.Invincible(input);
        mortarComponent.Invincible(input);
        flamethrowerComponent.Invincible(input);
    }

    private bool CoreIsDead()
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        return false;
    }

}
