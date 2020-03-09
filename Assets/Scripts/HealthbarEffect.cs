using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using CodeMonkey;

public class HealthbarEffect : MonoBehaviour {

    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = .6f;

    private Image barImage;
    private Image damagedBarImage;
    public HealthbarSystem healthSystem;
    private float damagedHealthShrinkTimer;
    public Unit unit;

    private void Awake()
    {
        barImage = transform.Find("healthbar").GetComponent<Image>();
        damagedBarImage = transform.Find("healthbarshrink").GetComponent<Image>();


    }

    private void Start()
    {
        healthSystem = new HealthbarSystem(unit.life);
        SetHealth(healthSystem.GetHealthNormalized());
        damagedBarImage.fillAmount = barImage.fillAmount;

        healthSystem.OnDamage += HealthSystem_OnDamage;
        healthSystem.OnHeal += HealthSystem_OnHeal;
        // CMDebug.ButtonUI(new Vector2(-100, -50), "Damage", () => healthSystem.damageHealth(10));
        // CMDebug.ButtonUI(new Vector2(+100, -50), "Heal", () => healthSystem.healHealth(10));


    }

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

    public void HealthSystem_OnDamage(object sender, System.EventArgs e)
    {
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        SetHealth(healthSystem.GetHealthNormalized());
    }

    public void HealthSystem_OnHeal(object sender, System.EventArgs e)
    {
        SetHealth(healthSystem.GetHealthNormalized());
        damagedBarImage.fillAmount = barImage.fillAmount;
    }

 public void SetHealth(float healthNormalized)
    {
        barImage.fillAmount = healthNormalized;
    }

    public void hitDamage(int damage) 
    {
        healthSystem.damageHealth(damage);
    }

}
