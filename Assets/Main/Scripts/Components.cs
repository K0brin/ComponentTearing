using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class Components : MonoBehaviour
{

    [SerializeField] protected float currentHealth = 0f;
    [SerializeField] protected float maxHealth = 100f;

    [SerializeField] protected Slider healthSlider;

    [SerializeField] GameObject sightArea;

    protected GameObject player;
    protected PlayerController playerController;


    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        //set max health
        TakeDamage(-maxHealth);
    }

    protected virtual void Update()
    {

    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    protected virtual void UpdateHealthBar()
    {
        healthSlider.value = currentHealth / maxHealth;
    }

    protected virtual bool SeePlayer()
    {
        Collider[] objectsHit = Physics.OverlapBox(sightArea.transform.position, sightArea.transform.localScale / 2, Quaternion.identity);
        for (int i = 0; i < objectsHit.Length; i++)
        {
            if (objectsHit[i].CompareTag("Player"))
            {
                //use raycast to see if can see player; wall detection
                //raycast to player position; if hit player 
                RaycastHit hit;
                Vector3 playerDir = (player.transform.position - transform.position).normalized;
                if (Physics.Raycast(transform.position, playerDir, out hit, 6))
                {
                    return true;
                }
            }
        }
        return false;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(sightArea.transform.position, sightArea.transform.localScale);
    }

}
