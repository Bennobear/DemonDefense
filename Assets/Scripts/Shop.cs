using CodeMonkey.Utils;
using UnityEngine;
//This class handles updates of towers as well as the ui providing the buttons to upgrade
//Singleton pattern, get all buttons attached to the overlay and hide it
public class Shop : MonoBehaviour
{
    private static Shop Instance { get; set; }
    private Vector3 Position;
    TestOurTile TileManager;

    //Singleton pattern, get all buttons attached to the overlay and hide it
    private void Awake()
    {
        Instance = this;

        transform.Find("ShopItem").GetComponent<Button_Sprite>().ClickFunc = PurchaseFirstTower;
        transform.Find("ShopItem(1)").GetComponent<Button_Sprite>().ClickFunc = PurchaseSecondTower;
        transform.Find("ShopItem(2)").GetComponent<Button_Sprite>().ClickFunc = PurchaseThirdTower;
        //transform.Find("Shop Item(3)").GetComponent<Button_Sprite>().ClickFunc = PurchaseFourthTower;
        Hide();
    }
    //Static method to open the overlay from another script
    public static void Show_Static(Vector3 position)
    {
        Instance.Show(position);
    }
    //Static method to close the overlay from another script
    public static void Hide_Static()
    {
        Instance.Hide();
    }
    //Activates the shop at the position of the tile
    private void Show(Vector3 position)
    {
        this.Position = position;
        gameObject.SetActive(true);
        transform.localPosition = Position;
    }
    //Hides the shop again 
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    //Get the TileManager (BuildManager) singleton 
    public void Start()
    {
        TileManager = TestOurTile.instance;
    }
    //Linked to button - call BuildManager method
    public void PurchaseFirstTower()
    {
        TileManager.SetTurretToBuild(TileManager.firstTower);
    }
    //Linked to button - call BuildManager method
    public void PurchaseSecondTower()
    {
        TileManager.SetTurretToBuild(TileManager.secondTower);
    }
    //Linked to button - call BuildManager method
    public void PurchaseThirdTower()
    {
        TileManager.SetTurretToBuild(TileManager.thirdTower);
    }
    //Returns the active status of the overlay (true/false)
    public static bool isActive()
    {
        return Instance.gameObject.activeSelf;
    }
}
