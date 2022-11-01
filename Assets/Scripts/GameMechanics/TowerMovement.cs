using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerMovement : MonoBehaviour
{
    [SerializeField] Tilemap map;
    [SerializeField] private static List<TowerMovement> moveToMice = new List<TowerMovement>();
    [SerializeField] private float _speed;
    [SerializeField] private float _range;
    [SerializeField] private float _energy;
    private MapManager mapManager;
    private Vector3 _target;
    public Vector3Int gridPosition;
    public Vector3 tileCenter;
    private int tileId;
    private bool _selected;

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
        mapManager.SetColorToTheCells();
        foreach (TowerMovement obj in moveToMice)
        {
            if(obj != this)
            {
                obj._selected = false;
                obj.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private void Awake()
    {
        mapManager = FindObjectOfType<MapManager>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && _selected)
        {
            CheckTile();
        }
        float _modifiedSpeed = mapManager.GetTileWalkingSpeed(transform.position) * _speed;
        transform.position = Vector3.MoveTowards(transform.position, tileCenter, _modifiedSpeed * Time.deltaTime);
    }

    private void CheckTile()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gridPosition = map.WorldToCell(mousePosition);
        if (map.HasTile(gridPosition))
        {
            tileCenter = map.GetCellCenterWorld(gridPosition);
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        _selected = false;
        mapManager.RemoveColorFromTheCells();
    }
}
