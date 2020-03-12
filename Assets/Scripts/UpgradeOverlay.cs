using UnityEngine;
using CodeMonkey.Utils;
//This class handles upgrades of towers as well as the ui providing the buttons to upgrade
public class UpgradeOverlay : MonoBehaviour
{
    private static UpgradeOverlay Instance { get;  set; }
    private Tower tower;

    //Singleton pattern, get all buttons attached to the overlay and hide it
    private void Awake()
    {
        Instance = this;

        transform.Find("UpgradeRange").GetComponent<Button_Sprite>().ClickFunc = UpgradeRange;
        transform.Find("UpgradeDamage").GetComponent<Button_Sprite>().ClickFunc = UpgradeDamge;
        transform.Find("SellTower").GetComponent<Button_Sprite>().ClickFunc = SellTower;

        Hide();
    }
    //Static method to open the overlay from another script
    public static void Show_Static(Tower tower)
    {
        Instance.Show(tower);
    }
    //Static method to close the overlay from another script
    public static void Hide_Static()
    {
        Instance.Hide();
    }
    //Activates the UpgradeOverlay at the position of the tower
   private void Show(Tower tower)
    {
        this.tower = tower;
        gameObject.SetActive(true);
        transform.localPosition = tower.transform.localPosition;
        RefreshRangeVisual();
    }
    //Hides the UpgradeOverlay again
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    //Calls the UpgradeRange method of a specific tower
    public void UpgradeRange()
    {
        tower.UpgradeRange();
        RefreshRangeVisual();
    }
    //Calls the UpgradeDamange method of a specific tower
    public void UpgradeDamge()
    {
        tower.UpgradeDamage();
    }
    //Calls the SellTower method of a specific tower
    public void SellTower()
    {
        tower.SellTower();
    }
    //Rresizes the range display (Circle)
    private void RefreshRangeVisual()
    {
        transform.Find("Range").localScale = Vector3.one * tower.GetRange() * 2f;
    }
    //Returns the active status of the overlay (true/false)
    public static bool isActive()
    {
        return Instance.gameObject.activeSelf;
    }
}
