using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestOurTile : MonoBehaviour
{
	public static TestOurTile instance;

	private WorldTile _tile;
	// TESTING
	public GameObject firstTower;
	public GameObject secondTower;
	public GameObject thirdTower;
	private GameObject selectedTower;
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
		if (Input.GetMouseButtonDown(0))
		{
			if (selectedTower != null)
			{
				Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);

				var tiles = GameTiles.instance.tiles; // This is our Dictionary of tiles
													  // Playing Tower and Color ground

				if (tiles.TryGetValue(worldPoint, out _tile))
				{
					print("Tile " + _tile.Name + " costs: " + _tile.Cost);
					_tile.TilemapMember.SetTileFlags(_tile.LocalPlace, TileFlags.None);
					_tile.TilemapMember.SetColor(_tile.LocalPlace, Color.green);
					Vector3 towerPos = _tile.WorldLocation;
					towerPos.z = -1;
					towerPos.x = towerPos.x + 0.5f;
					towerPos.y = towerPos.y + 0.5f;
					if (_tile.Blocked)
					{
						print("Tile already used");
					}
					else
						Instantiate(selectedTower, towerPos, Quaternion.identity);
					_tile.Blocked = true;

				}
			}
		}
		else
		{
			Debug.Log("No Turret Selected");
		}
	}
	public void SetTurretToBuild(GameObject _turret)
	{
		selectedTower = _turret;
	}

	public void GetTurretToBuild(GameObject _turret)
	{
		_turret = selectedTower;
	}
}