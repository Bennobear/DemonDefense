using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private static Shop Instance { get; set; }
    private Vector3 Position;
    TestOurTile TileManager;

    private void Awake()
    {
        Instance = this;

        transform.Find("ShopItem").GetComponent<Button_Sprite>().ClickFunc = PurchaseFirstTower;
        transform.Find("ShopItem(1)").GetComponent<Button_Sprite>().ClickFunc = PurchaseSecondTower;
        transform.Find("ShopItem(2)").GetComponent<Button_Sprite>().ClickFunc = PurchaseThirdTower;
        //transform.Find("Shop Item(3)").GetComponent<Button_Sprite>().ClickFunc = PurchaseFourthTower;
        Hide();
    }

    public static void Show_Static(Vector3 position)
    {
        Instance.Show(position);
    }

    public static void Hide_Static()
    {
        Instance.Hide();
    }
    private void Show(Vector3 position)
    {
        this.Position = position;
        gameObject.SetActive(true);
        transform.localPosition = Position;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Start()
    {
        TileManager = TestOurTile.instance;
    }

    public void PurchaseFirstTower()
    {
        TileManager.SetTurretToBuild(TileManager.firstTower);
    }

    public void PurchaseSecondTower()
    {
        TileManager.SetTurretToBuild(TileManager.secondTower);
    }

    public void PurchaseThirdTower()
    {
        TileManager.SetTurretToBuild(TileManager.thirdTower);
    }

    public static bool isActive()
    {
        return Instance.gameObject.activeSelf;
    }


}
