using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.EventSystems;

public class UpgradeOverlay : MonoBehaviour
{
    private static UpgradeOverlay Instance { get;  set; }

    private Tower tower;

    private void Awake()
    {
        Instance = this;

        transform.Find("UpgradeRange").GetComponent<Button_Sprite>().ClickFunc = UpgradeRange;
        transform.Find("UpgradeDamage").GetComponent<Button_Sprite>().ClickFunc = UpgradeDamge;
        transform.Find("CloseOverlay").GetComponent<Button_Sprite>().ClickFunc = Hide;
        transform.Find("SellTower").GetComponent<Button_Sprite>().ClickFunc = SellTower;

        Hide();
    }

    public static void Show_Static(Tower tower)
    {
        Instance.Show(tower);
    }

    public static void Hide_Static()
    {
        Instance.Hide();
    }
   private void Show(Tower tower)
    {
        this.tower = tower;
        gameObject.SetActive(true);
        transform.localPosition = tower.transform.localPosition;
        RefreshRangeVisual();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpgradeRange()
    {
        tower.UpgradeRange();
        RefreshRangeVisual();
    }

    private void UpgradeDamge()
    {
        tower.UpgradeDamage();
    }

    private void SellTower()
    {
        tower.SellTower();
    }

    private void RefreshRangeVisual()
    {
        transform.Find("Range").localScale = Vector3.one * tower.GetRange() * 2f;
    }
}
