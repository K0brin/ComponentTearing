using System.Collections;
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
    [SerializeField] private float flameDamage = 10f;
    [SerializeField] private float secBetweenDamage = 0.5f;
    private bool canAttack;
    private GameObject fireImage;

    void Start()
    {
        base.Start();
        canAttack = true;
        fireImage = GameObject.FindGameObjectWithTag("FireImage");
        fireImage.SetActive(false);
    }

    void Update()
    {
        base.Update();
        if (SeePlayer(false))
        {
            Debug.Log("see's player");
            StartCoroutine(StartAttack());
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

    IEnumerator StartAttack()
    {
        if (canAttack)
        {
            canAttack = false;
            
            //call player damage function on tic rate
            if (InAttackRange())
            {
                playerController.TakeDamage(flameDamage);
                fireImage.SetActive(true);
            }
            yield return new WaitForSeconds(secBetweenDamage);
            fireImage.SetActive(false);
            canAttack = true;
        }
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(flameRange.transform.position, flameRange.transform.localScale);
    }
}
