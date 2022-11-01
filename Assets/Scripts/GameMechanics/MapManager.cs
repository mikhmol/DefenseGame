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
    private Vector3Int LEFT, RIGHT, DOWN, DOWNLEFT, DOWNRIGHT, UP, UPLEFT, UPRIGHT;

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
        LEFT = new Vector3Int(gridPosition.x - 1, gridPosition.y, 0);
        RIGHT = new Vector3Int(gridPosition.x + 1, gridPosition.y, 0);
        DOWN = new Vector3Int(gridPosition.x, gridPosition.y - 1, 0);
        DOWNLEFT = new Vector3Int(gridPosition.x - 1, gridPosition.y - 1, 0);
        DOWNRIGHT = new Vector3Int(gridPosition.x + 1, gridPosition.y - 1, 0);
        UP = new Vector3Int(gridPosition.x, gridPosition.y + 1, 0);
        UPLEFT = new Vector3Int(gridPosition.x - 1, gridPosition.y + 1, 0);
        UPRIGHT = new Vector3Int(gridPosition.x + 1, gridPosition.y + 1, 0);
        map.SetTileFlags(LEFT, TileFlags.None);
        map.SetTileFlags(RIGHT, TileFlags.None);
        map.SetTileFlags(DOWN, TileFlags.None);
        map.SetTileFlags(DOWNLEFT, TileFlags.None);
        map.SetTileFlags(DOWNRIGHT, TileFlags.None);
        map.SetTileFlags(UP, TileFlags.None);
        map.SetTileFlags(UPLEFT, TileFlags.None);
        map.SetTileFlags(UPRIGHT, TileFlags.None);
        map.SetColor(LEFT, Color.green);
        map.SetColor(RIGHT, Color.green);
        map.SetColor(DOWN, Color.green);
        map.SetColor(DOWNLEFT, Color.green);
        map.SetColor(DOWNRIGHT, Color.green);
        map.SetColor(UP, Color.green);
        map.SetColor(UPLEFT, Color.green);
        map.SetColor(UPRIGHT, Color.green);
    }

    public void RemoveColorFromTheCells()
    {
        map.SetColor(LEFT, Color.white);
        map.SetColor(RIGHT, Color.white);
        map.SetColor(DOWN, Color.white);
        map.SetColor(DOWNLEFT, Color.white);
        map.SetColor(DOWNRIGHT, Color.white);
        map.SetColor(UP, Color.white);
        map.SetColor(UPLEFT, Color.white);
        map.SetColor(UPRIGHT, Color.white);
    }
}
