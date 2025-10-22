using System.Collections;
using UnityEngine;

public class CoreComponent : Components
{

    //TODO as core takes damage it will slow down robot
    //incentive to shoot core after each component rather than others
    //if core sustains enough damage other components will stop shooting for set time
    //if timer goes up before damage is met nothing happens

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
