using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Supporter
{
    // list of lists of different help sets
    public static List<List<int>> SupportList = new List<List<int>>
    { 
        new List<int> { 1, 0, 0, 0 }, 
        new List<int> { 0, 1, 0, 0 }, 
        new List<int> { 0, 0, 1, 0 }, 
        new List<int> { 0, 0, 0, 1 }, 
    };
}

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

    //bayraktar "y" position (height)
    float heightOfFlight = 6.14f;

    private void Start()
    {
        InGameTimers.Support += GetSupport;
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

    public void GetSupport(bool param)
    {
        List<int> randomSupportList = Supporter.SupportList[Random.Range(0, Supporter.SupportList.Count)];

        for (int i = 0; i < randomSupportList.Count; i++)
        {
            TowerCounts[i] += randomSupportList[i];
        }

        AllowChange(true);
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

            
            if(spawnID == 3)
            {
                GameObject bayraktar = Instantiate(towersPrefabs[spawnID],new Vector3(-16f, heightOfFlight, 0f), Quaternion.identity, spawnTowerRoot);
                bayraktar.GetComponent<Bayraktar>().PositionToReach = new Vector3(position.x, heightOfFlight, 0f);
            }
            else
            {
                GameObject tower = Instantiate(towersPrefabs[spawnID], spawnTowerRoot);
                tower.transform.position = position;
            }

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
