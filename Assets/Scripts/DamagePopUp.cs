using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

// This class manages shown damage values and there behaviour
public class DamagePopUp : MonoBehaviour {

    // Creating a Pop-Up with damage values
    public static DamagePopUp Create(Vector3 position, int damageAmount, bool critHit)
    {
        Transform damagePopUptransform = Instantiate(CodeMonkey.Assets.i.pfDamagePopUp, position, Quaternion.identity);
        
        DamagePopUp damagePopUp = damagePopUptransform.GetComponent<DamagePopUp>();
        damagePopUp.SetUp(damageAmount, critHit);

        return damagePopUp;
    }

    public static DamagePopUp CreateMoney(Vector3 position, int amount)
    {
        Transform damagePopUptransform = Instantiate(CodeMonkey.Assets.i.moneyPopup, position, Quaternion.identity);

        DamagePopUp damagePopUp = damagePopUptransform.GetComponent<DamagePopUp>();
        damagePopUp.SetUp(-amount, false);
        //Debug.Log("MoneyPopUp");
        return damagePopUp;
    }


    private static int sortingOrder;

    private const float DISAPPEAR_TIMER_MAX = .5f;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private int critDamageAmount;
    private Vector3 moveVector;

    // Loads the text-component from the object
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    // Manages the appearance of the shown damage/heal values
    public void SetUp(int damageAmount, bool critHit)
    {
        if (!critHit)
        {
            textMesh.fontSize = 10;
            textColor = UtilsClass.GetColorFromString("E5DB15");
            damageAmount = damageAmount;
        }
        else
        {
            textMesh.fontSize = 15;
            textColor = UtilsClass.GetColorFromString("FF2B00");
            damageAmount = damageAmount * 2;
        }
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        moveVector = new Vector3(2f, 1) * 2f;

        textMesh.SetText(damageAmount.ToString());
    }

    // Manages the behaviour of show damage/heal values
    private void Update()
    {
        if(disappearTimer > DISAPPEAR_TIMER_MAX *.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;

        }else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        float moveYspeed = 5f;
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 2f * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            //Start Disappearing
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
                if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}