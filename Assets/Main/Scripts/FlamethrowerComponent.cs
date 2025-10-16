using UnityEngine;

public class FlamethrowerComponent : Components
{
    //use physics.overlapBox
    //maybe linger attack
    //tic damage when player is inside



    //starts flame attack when see's you
    //flame attack continues for seconds even after not seeing you
    //meaning you can still take damage

    [SerializeField] private GameObject flameRange;

    void Update()
    {
        if (SeePlayer() && InAttackRange())
        {
            StartAttack();
        }
    }

    //if SeePlayer()==true && InAttackRange
    private bool InAttackRange()
    {
        Collider[] objectsHit = Physics.OverlapBox(flameRange.transform.position, flameRange.transform.localScale/2, Quaternion.identity);
        for(int i = 0; i < objectsHit.Length; i++)
        {
            if (objectsHit[i].CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void StartAttack()
    {


        //call player damage function
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(flameRange.transform.position, flameRange.transform.localScale);
    }
}
