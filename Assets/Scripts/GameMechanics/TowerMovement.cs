using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerMovement : MonoBehaviour
{
    //[SerializeField] private float movementSpeed;
    //[SerializeField] Tilemap map;
    //int spawnID = -1;
    public Tilemap map;
    [SerializeField] private static List<TowerMovement> moveToMice = new List<TowerMovement>();
    [SerializeField] private float _speed;
    private Vector3 _target;
    private Spawner spawner;
    private Tilemap spawnTilemap;

    private bool _selected;

    void Start()
    {
        map = GameObject.Find("SpawnPoint").GetComponent<Tilemap>();
        moveToMice.Add(this);
        _target = transform.position;
        //map = GameObject.Find("SpawnPoint").GetComponent<Tilemap>();

        //destination = transform.position;
        //mouseInput.Mouse.MouseClick.performed += _ => MouseClick();
        spawner = GetComponent<Spawner>();
    }

    private void OnMouseDown()
    {
        _selected = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;

        foreach(TowerMovement obj in moveToMice)
        {
            if(obj != this)
            {
                obj._selected = false;
                obj.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private void MouseClick()
    {
        //Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //Vector3Int gridPosition = map.WorldToCell(mousePosition);

        //if (map.HasTile(gridPosition))
        //{
            //destination = mousePosition;
        //}
    }

    void Update()
    {
        if(Input.GetMouseButton(1) && _selected)
        {
            _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //var cellPosDefault = spawnTilemap.WorldToCell(mousePos);
            //var cellPosCentered = spawnTilemap.GetCellCenterWorld(cellPosDefault);
            //if (spawnTilemap.GetColliderType(cellPosDefault) == Tile.ColliderType.Sprite)
            //{
                //Spawn the tower
                //SpawnTower(cellPosCentered);
                //Disable the collider
                //spawnTilemap.SetColliderType(cellPosDefault, Tile.ColliderType.None);
            //}
            _target.z = transform.position.z;
        }
        //if (Vector3.Distance(transform.position, destination) > 0.1f)
        //transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
        //if (CanMove())
        //{
        //    SelectTower();
        //}
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }

    public void SelectTower(/*int id*/)
    {
        //spawnID = id;
    }

    public void DeselectTower()
    {
        //spawnID = -1;
    }

    bool CanMove()
    {
        return true;
    }
}
