using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using CodeMonkey;

// This class manages the visual changes of the implemented healthsystem
public class HealthbarEffect : MonoBehaviour {

    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = .6f;

    private Image barImage;
    private Image damagedBarImage;
    public HealthbarSystem healthSystem;
    private float damagedHealthShrinkTimer;
    public Unit unit;

    // This will load the custom healthbars
    private void Awake()
    {
        barImage = transform.Find("healthbar").GetComponent<Image>();
        damagedBarImage = transform.Find("healthbarshrink").GetComponent<Image>();


    }

    // Manages the fillamount of the healthbar
    private void Start()
    {
        healthSystem = new HealthbarSystem(unit.life);
        SetHealth(healthSystem.GetHealthNormalized());
        damagedBarImage.fillAmount = barImage.fillAmount;

        healthSystem.OnDamage += HealthSystem_OnDamage;
        healthSystem.OnHeal += HealthSystem_OnHeal;


    }

    // Manages the appearance of the healthbar after taking damage or recieving a heal
    private void Update()
    {
        damagedHealthShrinkTimer -= Time.deltaTime;
        if(damagedHealthShrinkTimer < 0)
        {
           if(barImage.fillAmount < damagedBarImage.fillAmount)
            {
                float shrinkspeed = 1f;
                damagedBarImage.fillAmount -= shrinkspeed * Time.deltaTime;
            }
        }
    }

    // Manages the Damage
    public void HealthSystem_OnDamage(object sender, System.EventArgs e)
    {
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        SetHealth(healthSystem.GetHealthNormalized());
    }

    // Manages the Heal
    public void HealthSystem_OnHeal(object sender, System.EventArgs e)
    {
        SetHealth(healthSystem.GetHealthNormalized());
        damagedBarImage.fillAmount = barImage.fillAmount;
    }

 public void SetHealth(float healthNormalized)
    {
        barImage.fillAmount = healthNormalized;
    }

    // This function transfers the damage into the Unit Class
    public void hitDamage(int damage) 
    {
        healthSystem.damageHealth(damage);
    }

}
