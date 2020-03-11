using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TestOurTile : MonoBehaviour
{
	public static TestOurTile instance;
	public PlayerStats playerStats;
	public Vector3 towerPos;

	private WorldTile _tile;
	// TESTING
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

	// Update is called once per frame
	private void Update()
	{
		var tiles = GameTiles.instance.tiles;
		
		if (Input.GetMouseButtonDown(0))
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				Debug.Log("Clicked on the UI");
			}
			else
			{
				Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);
				// This is our Dictionary of tiles
				if (tiles.TryGetValue(worldPoint, out _tile))
				{
					print("Tile " + _tile.Name);
					_tile.TilemapMember.SetTileFlags(_tile.LocalPlace, TileFlags.None);
					if (_tile.Blocked)
					{
						Shop.Hide_Static();
						print("Tile already used");
					}
					else if (Shop.isActive())
					{
						Shop.Hide_Static();
						Debug.Log("Shop was already active");
					}
					else if (UpgradeOverlay.isActive())
					{
						Shop.Hide_Static();
						UpgradeOverlay.Hide_Static();
						Debug.Log("Upgrade Overlay is active");
					}
					else
					{
						towerPos = _tile.WorldLocation;
						towerPos.z = -1;
						towerPos.x = towerPos.x + 0.5f;
						towerPos.y = towerPos.y + 0.5f;
						Shop.Show_Static(towerPos);
						Debug.Log("Show Shop");
					}
				}
				else
				{
					Shop.Hide_Static();
					UpgradeOverlay.Hide_Static();
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

	public void BuildTower()
	{
		Instantiate(selectedTower, towerPos, Quaternion.identity);
		Tower t = selectedTower.GetComponent<Tower>();
		PlayerStats.money -= t.GetPrice();
		DamagePopUp.CreateMoney(playerStats.moneyPos.position,t.GetPrice());
		_tile.Blocked = true;
		StartCoroutine(Wait());
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(.1f);
		Shop.Hide_Static();
	}
}