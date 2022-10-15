using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private float walkingSpeed;
    private float securityLevel;

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = map.WorldToCell(mousePosition);
            TileBase clickedTile = map.GetTile(gridPosition);
            if(clickedTile == null)
            {
                walkingSpeed = 1;
                securityLevel = 1;
                print("2");
            }
            //walkingSpeed = dataFromTiles[clickedTile].walkingSpeed;
            //securityLevel = dataFromTiles[clickedTile].securityLevel;
            //if (mousePosition == null) walkingSpeed = 1;
            //if (mousePosition == null) securityLevel = 1;
            print(walkingSpeed);
            print(securityLevel);
        }
    }
}
