    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerMovement : MonoBehaviour
{
    [SerializeField] Tilemap map;
    [SerializeField] private static List<TowerMovement> moveToMice = new List<TowerMovement>();
    [SerializeField] private float _speed;
    [SerializeField] private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private Vector3 _target;
    public Vector3Int gridPosition;
    public Vector3 tileCenter;
    private int tileId;

    private bool _selected;

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

    void Start()
    {
        map = GameObject.Find("SpawnPoint").GetComponent<Tilemap>();
        moveToMice.Add(this);
        _target = transform.position;
    }

    private void OnMouseDown()
    {
        _selected = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        foreach (TowerMovement obj in moveToMice)
        {
            if(obj != this)
            {
                obj._selected = false;
                obj.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && _selected)
        {
            CheckTile();
        }
        transform.position = Vector3.MoveTowards(transform.position, tileCenter, _speed * Time.deltaTime);
    }

    private void CheckTile()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePosition);
        gridPosition = map.WorldToCell(mousePosition);
        TileBase clickedTile = map.GetTile(gridPosition);
        //float walkingSpeed = dataFromTiles[clickedTile].walkingSpeed;
        //print(clickedTile);
        //print(walkingSpeed);
        //Debug.Log(gridPosition);
        if (map.HasTile(gridPosition))
        {
            tileCenter = map.GetCellCenterWorld(gridPosition);
        }
    }
}
