using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    TestOurTile gm;

    public void Start()
    {
        gm = TestOurTile.instance;
    }

    public void PurchaseFirstTower()
    {
        Debug.Log("First");
        gm.SetTurretToBuild(gm.firstTower);
    }

    public void PurchaseSecondTower()
    {
        Debug.Log("Second");
        gm.SetTurretToBuild(gm.secondTower);
    }

    public void PurchaseThirdTower()
    {
        Debug.Log("Third");
        gm.SetTurretToBuild(gm.thirdTower);
    }


}
