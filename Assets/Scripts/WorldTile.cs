using UnityEngine;
using UnityEngine.Tilemaps;
//This class contains the information saved in each tile we lay down on the tilemap

public class WorldTile
{
    public Vector3Int LocalPlace { get; set; }

    public Vector3 WorldLocation { get; set; }

    public TileBase TileBase { get; set; }

    public Tilemap TilemapMember { get; set; }

    public string Name { get; set; }
    //Used to detect if a tower is placed on this tile.
    public bool Blocked { get; set; }
}