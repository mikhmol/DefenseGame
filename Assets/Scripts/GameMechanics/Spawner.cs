using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

/*public class Supporter
{
    // 0 - soilderUnit, 1 - tankUnit, 2 - bucephalusUnit, 3 - bayraktarUnit,
    // 4 - javelinItem, 5 - machinegunItem

    // list of lists of different help on the start wave
    public static List<List<int>> SupportListStart = new List<List<int>>
    { 
        new List<int> { 2, 1, 1, 0, 0, 0 }, 
        new List<int> { 1, 0, 2, 0, 1, 0 }, 
        new List<int> { 2, 2, 0, 0, 0, 0 },  
    };

    ////////////////////////////////////////////////////////////////////////////////////

    // list of lists of different help on the first stage
    public static List<List<int>> SupportListFirstStage = new List<List<int>>
    {
        new List<int> { 2, 0, 0, 0, 0, 0 },
        new List<int> { 0, 1, 0, 0, 0, 0 },
        new List<int> { 0, 0, 1, 0, 0, 0 },
        new List<int> { 0, 0, 0, 0, 1, 0 },
    };

    ////////////////////////////////////////////////////////////////////////////////////

    // list of lists of different help on the second stage part 1
    public static List<List<int>> SupportListSecondStagePart1 = new List<List<int>>
    {
        new List<int> { 2, 0, 0, 0, 0, 0 },
        new List<int> { 1, 0, 0, 0, 0, 1 },
        new List<int> { 0, 0, 0, 0, 1, 1 },
    };

    // list of lists of different help on the second stage part 2
    public static List<List<int>> SupportListSecondStagePart2 = new List<List<int>>
    {
        new List<int> { 1, 0, 0, 1, 0, 0 },
        new List<int> { 1, 1, 0, 0, 0, 0 },
        new List<int> { 0, 0, 1, 0, 1, 0 },
    };

    // list of lists of different help on the second stage part 3
    public static List<List<int>> SupportListSecondStagePart3 = new List<List<int>>
    {
        new List<int> { 3, 0, 0, 0, 0, 0 },
        new List<int> { 0, 1, 1, 0, 0, 0 },
        new List<int> { 0, 2, 0, 0, 0, 0 },
    };

    ////////////////////////////////////////////////////////////////////////////////////
    
    // list of lists of different help on the third stage part 1
    public static List<List<int>> SupportListThirdStagePart1 = new List<List<int>>
    {
        // new List<int> { 0, 0, 0, 0, 0, 0 } + artillery
        new List<int> { 2, 0, 0, 0, 0, 0 },
        new List<int> { 0, 0, 0, 1, 0, 0 },
        new List<int> { 0, 1, 0, 0, 0, 0 }, 
    };

    // list of lists of different help on the third stage part 2
    public static List<List<int>> SupportListThirdStagePart2 = new List<List<int>>
    {
        // new List<int> { 0, 0, 0, 0, 0, 0 } + aviation
        new List<int> { 2, 0, 0, 0, 0, 0 },
        new List<int> { 0, 0, 0, 0, 0, 1 },
        new List<int> { 0, 2, 0, 0, 0, 0 },
    };

    // list of lists of different help on the third stage part 3
    public static List<List<int>> SupportListThirdStagePart3 = new List<List<int>>
    {
        new List<int> { 0, 0, 0, 0, 0, 1 },
        new List<int> { 0, 0, 0, 0, 1, 0 },
        new List<int> { 1, 0, 0, 0, 0, 0 },
    };
}*/  



public class Spawner : MonoBehaviour
{
    [SerializeField]bool allowToSpawnUnits = true;

    [SerializeField] private int TotalTowerCount;

    //list of towers (prefabs) that will instatiate
    public List<GameObject> towersPrefabs;

    //Towercount for each type
    public List<int> TowerCounts;
    public List<int> CurrentTowerCounts;

    //list of towers buttons (bg images)
    public List<Image> towersBgImageUI;

    //Transform of the spawning towers (Root Object)
    public Transform spawnTowerRoot;

    //SpawnPoints Tilemap
    public Tilemap spawnTilemap;
     
    public SpriteRenderer testSprite;

    //id of tower to spawn
    int spawnID = -1;

    // support info text
    public Text SupportClarificationText;

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

        UpdateUnitButtonsUI.UpdateUI?.Invoke();
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
        List<int> randomSupportList = new List<int>();

        int currentWave = GameManager.wave;

        if (currentWave == 0)
        {
            randomSupportList = Supporter.SupportListStart[Random.Range(0, Supporter.SupportListStart.Count)];
        }
        else if (currentWave >= 1 && currentWave <= 5) 
        {
            randomSupportList = Supporter.SupportListFirstStage[Random.Range(0, Supporter.SupportListFirstStage.Count)];
        }
        else if (currentWave >= 6 && currentWave <= 10)
        {
            randomSupportList = Supporter.SupportListSecondStagePart1[Random.Range(0, Supporter.SupportListSecondStagePart1.Count)];
        }
        else if (currentWave >= 11 && currentWave <= 15)
        {
            randomSupportList = Supporter.SupportListSecondStagePart2[Random.Range(0, Supporter.SupportListSecondStagePart2.Count)];
        }
        else if (currentWave >= 16 && currentWave <= 20)
        {
            randomSupportList = Supporter.SupportListSecondStagePart3[Random.Range(0, Supporter.SupportListSecondStagePart3.Count)];
        }
        else if (currentWave >= 21 && currentWave <= 25)
        {
            randomSupportList = Supporter.SupportListThirdStagePart1[Random.Range(0, Supporter.SupportListThirdStagePart1.Count)];
        }
        else if (currentWave >= 26 && currentWave <= 30)
        {
            randomSupportList = Supporter.SupportListThirdStagePart2[Random.Range(0, Supporter.SupportListThirdStagePart2.Count)];
        }
        else if (currentWave >= 31 && currentWave <= 35)
        {
            randomSupportList = Supporter.SupportListThirdStagePart3[Random.Range(0, Supporter.SupportListThirdStagePart3.Count)];
        }

        ///////////////////////////////////////////////////

        SupportClarificationText.text = string.Format(($"You have received:"));

        for (int i = 0; i < randomSupportList.Count; i++)
        {
            TowerCounts[i] += randomSupportList[i];

            if (randomSupportList[i] > 0)
            {
                SupportClarificationText.text += string.Format($"\n{UnitStringNames.UnitNames[i]} - {randomSupportList[i]};");
            }

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
                GameObject bayraktar = Instantiate(towersPrefabs[spawnID],new Vector3(-77f, position.y + 10f, 0f), Quaternion.identity, spawnTowerRoot);
                bayraktar.GetComponent<Bayraktar>().PositionToReach = new Vector3(position.x, position.y + 10f, 0f);
            }
            else
            {
                GameObject tower = Instantiate(towersPrefabs[spawnID], spawnTowerRoot);
                tower.transform.position = position;
            }

            DeselectTowers();

            UpdateUnitButtonsUI.UpdateUI?.Invoke();
        }
    }
    

    public void SelectTower(int id)
    {
        DeselectTowers();
        //set the spawn ID
        spawnID = id;
        //Highlight the tower
        if (CurrentTowerCounts[spawnID] < TowerCounts[spawnID])
            towersBgImageUI[spawnID].color = Color.grey;
    }

    public void DeselectTowers()
    {
        spawnID = -1;
        foreach (var t in towersBgImageUI)
        {
            t.color = new Color(1f, 1f, 1f);
        }
    }
    
    void AllowChange(bool allow)
    {
        allowToSpawnUnits = allow;
    }
}
