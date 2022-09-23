using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField]bool allowToSpawnUnits = true;

    [SerializeField] private int TotalTowerCount;

    //list of towers (prefabs) that will instatiate
    public List<GameObject> towersPrefabs;

    //Towercount for each type
    public List<int> TowerCounts;
    public List<int> CurrentTowerCounts;

    //list of towers (UI)
    public List<Image> towersUI;

    //Transform of the spawning towers (Root Object)
    public Transform spawnTowerRoot;

    //SpawnPoints Tilemap
    public Tilemap spawnTilemap;

    public SpriteRenderer testSprite;

    //id of tower to spawn
    int spawnID = -1;


    private void Start()
    {
        InGameTimers.Allow += AllowChange;
        CurrentTowerCounts = new List<int>();
        for (int c = 0; c < TowerCounts.Count; c++)
        {
            TotalTowerCount += TowerCounts[c];
            CurrentTowerCounts.Add(0);
        }
    }
    void Update()
    {
        if (CanSpawn())
            DetectSpawnPoint();
    }

    bool CanSpawn()
    {
        if(spawnID==-1)
            return false;
        else
            return true;
    }

    void DetectSpawnPoint()
    {
        //Detect when mouse is clicked (first touch clicked)
        if(Input.GetMouseButtonDown(0))
        {
            //get the world space position of the mouse
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //get the position of the cell in the tilemap
            var cellPosDefault = spawnTilemap.WorldToCell(mousePos);
            //get the center position of the cell
            var cellPosCentered = spawnTilemap.GetCellCenterWorld(cellPosDefault);
            //check if we can spawn in that cell (collider)
            if (spawnTilemap.GetColliderType(cellPosDefault) == Tile.ColliderType.Sprite)
            {
                //Spawn the tower
                SpawnTower(cellPosCentered);
                //Disable the collider
                spawnTilemap.SetColliderType(cellPosDefault,Tile.ColliderType.None);
            }
        }
    }

    void SpawnTower(Vector3 position)
    {
        if (allowToSpawnUnits && CurrentTowerCounts[spawnID] < TowerCounts[spawnID])
        {
            CurrentTowerCounts[spawnID]++;
            GameObject tower = Instantiate(towersPrefabs[spawnID], spawnTowerRoot);
            tower.transform.position = position;

            DeselectTowers();
        }
    }
    

    public void SelectTower(int id)
    {
        DeselectTowers();
        //set the spawn ID
        spawnID = id;
        //Highlight the tower
        if (CurrentTowerCounts[spawnID] < TowerCounts[spawnID])
            towersUI[spawnID].color = Color.grey;

    }

    public void DeselectTowers()
    {
        spawnID = -1;
        foreach (var t in towersUI)
        {
            t.color = new Color(1f, 1f, 1f);
        }
    }
    
    void AllowChange(bool allow)
    {
        allowToSpawnUnits = allow;
    }
}
