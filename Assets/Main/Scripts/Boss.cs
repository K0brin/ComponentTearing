using System;
using System.Collections;
using UnityEditor.Build;
using UnityEngine;

public class Boss : MonoBehaviour
{

    [SerializeField] private GameObject core, turret, flamethrower, mortar;
    [SerializeField] private Transform coreSpawn, turretSpawn, flamethrowerSpawn, mortarSpawn;
    [SerializeField] private GameObject sightArea;
    private PlayerController player;
    private bool seePlayer;
    private CoreComponent coreComponent;

    [SerializeField] private float bossSpeed;

    //instantiate components at set positions

    private void Start()
    {
        Instantiate(core, coreSpawn);
        Instantiate(turret, turretSpawn);
        Instantiate(flamethrower, flamethrowerSpawn);
        Instantiate(mortar, mortarSpawn);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        coreComponent = GameObject.FindGameObjectWithTag("Core").GetComponent<CoreComponent>();
    }

    void Update()
    {
        if (SeePlayer())
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, bossSpeed * Time.deltaTime);
        }
    }

    private bool SeePlayer()
    {
        if (!coreComponent.coreShown)
        { 
        Collider[] objectsHit = Physics.OverlapBox(sightArea.transform.position, sightArea.transform.localScale / 2, Quaternion.identity);
        for (int i = 0; i < objectsHit.Length; i++)
        {
            if (objectsHit[i].CompareTag("Player"))
            {
                return true;
            }
        }
        }
        return false;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(sightArea.transform.position, sightArea.transform.localScale);
    }

}
