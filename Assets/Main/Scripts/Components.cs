using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;
using UnityEngine.InputSystem;

public class Components : MonoBehaviour
{

    [SerializeField] protected float currentHealth = 0f;
    [SerializeField] protected float maxHealth = 100f;

    [SerializeField] protected Slider healthSlider;

    
    protected virtual void Start()
    {
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

   
}
