using System.Collections;
using UnityEngine;

public class CoreComponent : Components
{

    //TODO as core takes damage it will slow down robot

    private bool coreShown;
    public bool showCore;
    [SerializeField] private float openCoreTime;
    private int numberOfAliveComponents;

    private TurretComponent turretComponent;
    private MortarComponent mortarComponent;
    private FlamethrowerComponent flamethrowerComponent;

    void Start()
    {
        base.Start();
        coreShown = false;
        numberOfAliveComponents = 3;
        Invincible(true);

        turretComponent = GameObject.FindGameObjectWithTag("Turret").GetComponent<TurretComponent>();
        mortarComponent = GameObject.FindGameObjectWithTag("Mortar").GetComponent<MortarComponent>();
        flamethrowerComponent = GameObject.FindGameObjectWithTag("Flamethrower").GetComponent<FlamethrowerComponent>();
    }

    void Update()
    {
        base.Update();

        if (showCore && numberOfAliveComponents > 0)
        {
            Debug.Log("showcore");
            StartCoroutine(ShowCore());
            numberOfAliveComponents--;
            showCore = false;
        }
    }

    IEnumerator ShowCore()
    {
        coreShown = true;
        Invincible(false);
        SupportComponentsInvincible(true);
        yield return new WaitForSeconds(openCoreTime);
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

}
