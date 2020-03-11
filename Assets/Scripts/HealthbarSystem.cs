using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the healthbars current state and damage/heal changes
public class HealthbarSystem : MonoBehaviour
{
    public event EventHandler OnDamage;
    public event EventHandler OnHeal;
    

    private int healthAmount;
    private int healthAmountMax;

    public HealthbarSystem(int healthAmount)
    {
        healthAmountMax = healthAmount;
        this.healthAmount = healthAmount;
    }

    public void damageHealth(int amount)
    {
        healthAmount -= amount;
        if(healthAmount < 0){
            healthAmount = 0;
        }
        if (OnDamage != null) OnDamage(this, EventArgs.Empty);
    }

    public void healHealth(int amount)
    {
        healthAmount += amount;
        if(healthAmount > healthAmountMax){
           healthAmount = healthAmountMax;
        }
        if (OnHeal != null) OnHeal(this, EventArgs.Empty);
    }

    public float GetHealthNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }
}
