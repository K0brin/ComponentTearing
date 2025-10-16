using System;
using System.Collections;
using UnityEditor.Build;
using UnityEngine;

public class Boss : MonoBehaviour
{

    //TODO install git ignore for library, etc so git add works

    [SerializeField] private GameObject core, turret, flamethrower, mortar;
    [SerializeField] private Transform coreSpawn, turretSpawn, flamethrowerSpawn, mortarSpawn;

    private PlayerController player;

    //instantiate components at set positions

    private void Start()
    {
        Instantiate(core, coreSpawn);
        Instantiate(turret, turretSpawn);
        Instantiate(flamethrower, flamethrowerSpawn);
        Instantiate(mortar, mortarSpawn);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

}
