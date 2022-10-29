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
    private Vector3Int gridPosition;

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
            /*if(clickedTile == null)
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
            print(securityLevel);*/
        }
    }

    public float GetTileWalkingSpeed(Vector2 worldPosition)
    {
        gridPosition = map.WorldToCell(worldPosition);
        TileBase tile = map.GetTile(gridPosition);
        walkingSpeed = dataFromTiles[tile].walkingSpeed;
        return walkingSpeed;
    }

    public void SetColorToTheCells()
    {
        Vector3Int LEFT = new Vector3Int(gridPosition.x - 1, gridPosition.y, 0);
        Vector3Int RIGHT = new Vector3Int(gridPosition.x + 1, gridPosition.y, 0);
        Vector3Int DOWN = new Vector3Int(gridPosition.x, gridPosition.y - 1, 0);
        Vector3Int DOWNLEFT = new Vector3Int(gridPosition.x - 1, gridPosition.y - 1, 0);
        Vector3Int DOWNRIGHT = new Vector3Int(gridPosition.x + 1, gridPosition.y - 1, 0);
        Vector3Int UP = new Vector3Int(gridPosition.x, gridPosition.y + 1, 0);
        Vector3Int UPLEFT = new Vector3Int(gridPosition.x - 1, gridPosition.y + 1, 0);
        Vector3Int UPRIGHT = new Vector3Int(gridPosition.x + 1, gridPosition.y + 1, 0);
        Debug.Log("Left is" + LEFT);
        map.SetTileFlags(LEFT, TileFlags.None);
        map.SetColor(LEFT, Color.green);
    }
}
