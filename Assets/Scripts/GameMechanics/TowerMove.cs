using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerMove : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    [SerializeField] Tilemap map;

    private MouseInput mouseInput;

    private Vector3 destination;

    private void Awake()
    {
        mouseInput = new MouseInput();
    }

    private void OnEnable()
    {
        mouseInput.Enable();
    }

    private void OnDisable()
    {
        mouseInput.Disable();
    }

    void Start()
    {
        map = GameObject.Find("SpawnPoint").GetComponent<Tilemap>();

        destination = transform.position;
        mouseInput.Mouse.MouseClick.performed += _ => MouseClick();
    }

    private void MouseClick()
    {
        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3Int gridPosition = map.WorldToCell(mousePosition);

        if(map.HasTile(gridPosition))
        {
            destination = mousePosition;
        }
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, destination) > 0.1f)
        transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
    }
}
