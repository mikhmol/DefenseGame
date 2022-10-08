using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerMovement : MonoBehaviour
{
    [SerializeField] Tilemap map;
    [SerializeField] private static List<TowerMovement> moveToMice = new List<TowerMovement>();
    [SerializeField] private float _speed;
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
        if(Input.GetMouseButton(1) && _selected)
        {
            CheckTile();
        }
        transform.position = Vector3.MoveTowards(transform.position, tileCenter, _speed * Time.deltaTime);
    }

    private void CheckTile()
    {
        //if( map.HasTile(mousePosition))
        //{
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePosition);
            gridPosition = map.WorldToCell(mousePosition);
            Debug.Log(gridPosition);
            tileCenter = map.GetCellCenterWorld(gridPosition);
        //}
        //if (map.HasTile(gridPosition))
        //{
        //    Debug.Log("It has tile");
        //    transform.position = Vector3.MoveTowards(transform.position, tileCenter, _speed * Time.deltaTime);
        //    tileId = 1;
        //}
        //else
        //{
        //   Debug.Log("No");
        //    tileId = 0;
        //}
    }
}
