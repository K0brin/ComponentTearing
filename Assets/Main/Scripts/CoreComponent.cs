using System.Collections;
using UnityEngine;

public class CoreComponent : Components
{

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
