using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TestOurTile : MonoBehaviour
{
    public Animator money;

    public static TestOurTile instance;
    public PlayerStats playerStats;
    public Vector3 towerPos;

    private WorldTile _tile;
    public GameObject firstTower;
    public GameObject secondTower;
    public GameObject thirdTower;
    private GameObject selectedTower;
    private bool buildMode;
    Shop shop;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in Scene!");
            return;
        }
        instance = this;
    }

    //This is our input logic - it handles our Mouse Input and checks wheter any UI Element is active and acts accordingly
    private void Update()
    {
        var tiles = GameTiles.instance.tiles;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);
            // This is our Dictionary of tiles
            if (tiles.TryGetValue(worldPoint, out _tile))
            {
               // print("Tile " + _tile.Name);
                _tile.TilemapMember.SetTileFlags(_tile.LocalPlace, TileFlags.None);
                if (UpgradeOverlay.isActive())
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                   // Debug.Log("Check Overlay Tilemap");
                    if (Physics.Raycast(ray, out hit))
                    {
                       // Debug.Log("Raycast Hit");
                        if (hit.collider.tag == "Tower")
                        {
                            //Debug.Log("Hit Tower");
                        }
                        else if (hit.collider.tag == "Button")
                        {
                           // Debug.Log("Hit Button");
                        }
                        
                    }
                    else
                    {
                       // Debug.Log("No Hit");
                        Shop.Hide_Static();
                        UpgradeOverlay.Hide_Static();
                        Tooltip.Hide_Static();
                    }
                }
                else if (_tile.Blocked)
                {
                    Shop.Hide_Static();
                   // print("Tile already used");
                }
                else if (Shop.isActive())
                {
                    Shop.Hide_Static();
                    //Debug.Log("Shop was already active");
                }
                else
                {
                    towerPos = _tile.WorldLocation;
                    towerPos.z = -1;
                    towerPos.x = towerPos.x + 0.5f;
                    towerPos.y = towerPos.y + 0.5f;
                    Shop.Show_Static(towerPos);
                }
            }
            else
            {
                if (UpgradeOverlay.isActive())
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Debug.Log("Check Overlay Path");
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log("Raycast Hit");
                        if (hit.collider.tag == "Tower")
                        {
                           // Debug.Log("Hit Tower");
                        }
                        else if (hit.collider.tag == "Button")
                        {
                           // Debug.Log("Hit Button");
                        }
                    }
                    else
                    {
                       // Debug.Log("No Hit");
                        Shop.Hide_Static();
                        UpgradeOverlay.Hide_Static();
                        Tooltip.Hide_Static();
                    }
                }
                else
                {
                    Shop.Hide_Static();
                    UpgradeOverlay.Hide_Static();
                    Tooltip.Hide_Static();
                }     
            }
        }
    }
    public void SetTurretToBuild(GameObject _turret)
    {
        selectedTower = _turret;
        BuildTower();
    }

    public void GetTurretToBuild(GameObject _turret)
    {
        _turret = selectedTower;
    }
    //Unlocks the Tile again = set Blocked false
    public void DeleteTower(Tower tower)
    {
        var tiles = GameTiles.instance.tiles;
        if (tiles.TryGetValue(new Vector3Int(Mathf.FloorToInt(tower.transform.position.x), Mathf.FloorToInt(tower.transform.position.y), 0), out _tile))
        {
            print("Tower " + _tile.Name + " sold!");
            _tile.Blocked = false;
            StartCoroutine(Wait());
            //CALL POPUP to show it
        }
    }
    //Instantiates the Tower prefab of the Tower we selected
    public void BuildTower()
    {
        Tower t = selectedTower.GetComponent<Tower>();
        if (PlayerStats.money >= t.GetPrice())
        {
            Instantiate(selectedTower, towerPos, Quaternion.identity);
            PlayerStats.money -= t.GetPrice();
            DamagePopUp.CreateMoney(new Vector3(playerStats.moneyPos.position.x - 1, playerStats.moneyPos.position.y - 1, playerStats.moneyPos.position.z), t.GetPrice());
            _tile.Blocked = true;
            StartCoroutine(Wait());
        }
        else
        {
            money.SetTrigger("itHappened");
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.1f);
        Shop.Hide_Static();
    }
}