using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class Testing : MonoBehaviour
{    private void Start()
    {
        //DamagePopUp.Create(Vector3.zero, 250);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool critHit = Random.Range(0, 100) < 30;
            DamagePopUp.Create(UtilsClass.GetMouseWorldPosition(), 100, critHit);
        }
    }

}