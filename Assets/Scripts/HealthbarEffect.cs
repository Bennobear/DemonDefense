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
    private HealthbarSystem healthSystem;
    private float damagedHealthShrinkTimer;

    private void Awake()
    {
        barImage = transform.Find("healthbar").GetComponent<Image>();
        damagedBarImage = transform.Find("healthbarshrink").GetComponent<Image>();

    }

    private void Start()
    {
        healthSystem = new HealthbarSystem(100);
        SetHealth(healthSystem.GetHealthNormalized());
        damagedBarImage.fillAmount = barImage.fillAmount;

        healthSystem.OnDamage += HealthSystem_OnDamage;
        healthSystem.OnHeal += HealthSystem_OnHeal;

        CMDebug.ButtonUI(new Vector2(-100, -50), "Damage", () => healthSystem.damageHealth(10));
        CMDebug.ButtonUI(new Vector2(+100, -50), "Heal", () => healthSystem.healHealth(10));


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

    private void HealthSystem_OnDamage(object sender, System.EventArgs e)
    {
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        SetHealth(healthSystem.GetHealthNormalized());
    }

    private void HealthSystem_OnHeal(object sender, System.EventArgs e)
    {
        SetHealth(healthSystem.GetHealthNormalized());
        damagedBarImage.fillAmount = barImage.fillAmount;
    }

 private void SetHealth(float healthNormalized)
    {
        barImage.fillAmount = healthNormalized;
    }

}
