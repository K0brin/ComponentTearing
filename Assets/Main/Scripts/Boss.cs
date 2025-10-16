using System;
using System.Collections;
using UnityEditor.Build;
using UnityEngine;

public class Boss : MonoBehaviour
{

    [SerializeField] private GameObject core, turret, flamethrower, mortar;
    [SerializeField] private Transform coreSpawn, turretSpawn, flamethrowerSpawn, mortarSpawn;

    private PlayerController player;
    [SerializeField] private LayerMask playerLayer;

    [Header("Sight")]
    [SerializeField] private Transform sightTransform;
    [SerializeField] private Vector3 sightArea;
    public bool seePlayer;

    //instantiate components at set positions

    private void Start()
    {
        Instantiate(core, coreSpawn);
        Instantiate(turret, turretSpawn);
        Instantiate(flamethrower, flamethrowerSpawn);
        Instantiate(mortar, mortarSpawn);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        seePlayer = SeePlayer();
    }


    private bool SeePlayer()
    {
        Collider[] objectsHit = Physics.OverlapBox(sightTransform.position, sightArea/2, Quaternion.identity);
        for(int i = 0; i < objectsHit.Length; i++)
        {
            if (objectsHit[i].CompareTag("Player"))
            {
                //use raycast to see if can see player; wall detection
                //raycast to player position; if hit player 
                RaycastHit hit;
                Vector3 playerDir = (player.transform.position - transform.position).normalized;
                if (Physics.Raycast(transform.position, playerDir, out hit, playerLayer))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(sightTransform.position, sightArea);
    }

}
