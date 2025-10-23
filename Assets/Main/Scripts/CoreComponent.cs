using System.Collections;
using UnityEngine;

public class CoreComponent : Components
{

    //TODO as core takes damage it will slow down robot
    //incentive to shoot core after each component rather than others
    //other components are invincible and can't attack when core is shown

    private bool coreShown;
    [SerializeField] private float openCoreTime;

    void Start()
    {
        base.Start();
        coreShown = false;
    }

    void Update()
    {
        base.Update();

        /*  if(other component health == 0)
      {
          StartCoroutine(ShowCore());
      }*/
    }

    IEnumerator ShowCore()
    {
        coreShown = true;
        //move protective mesh
        yield return new WaitForSeconds(openCoreTime);
        //move mesh back
        coreShown = false;
    }

}
