using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdateUnitButtonsUI : MonoBehaviour
{
    // unit classes buttons text (amount of units into class)
    public Text PersonelUCText, 
                VehiclesUCText, 
                FacilitiesUCText;

    // units from personel class
    [Space(15)] 
    public Text Soilders;

    // units from vehicles class
    [Space(5)] 
    public Text Tanks;
    public Text Bucephalus, 
                Bayraktars;

    // units from facilities class

    [SerializeField]
    private Spawner spawner;

    public static Action UpdateUI;

    private void Awake()
    {
        UpdateUI += UpdateUICounters;
    }

    public void UpdateUICounters() 
    {
        UnitClassesCounters();
        UnitPanelsCounters();
    }

    private void UnitClassesCounters() 
    {
        PersonelUCText.text = (spawner.TowerCounts[0] - spawner.CurrentTowerCounts[0]).ToString();
        VehiclesUCText.text = (spawner.TowerCounts[1] - spawner.CurrentTowerCounts[1] +
                               spawner.TowerCounts[2] - spawner.CurrentTowerCounts[2] +
                               spawner.TowerCounts[3] - spawner.CurrentTowerCounts[3]).ToString();
    }

    private void UnitPanelsCounters() 
    {
        Soilders.text = (spawner.TowerCounts[0] - spawner.CurrentTowerCounts[0]).ToString();

        Tanks.text = (spawner.TowerCounts[1] - spawner.CurrentTowerCounts[1]).ToString();
        Bucephalus.text = (spawner.TowerCounts[2] - spawner.CurrentTowerCounts[2]).ToString();
        Bayraktars.text = (spawner.TowerCounts[3] - spawner.CurrentTowerCounts[3]).ToString();
    }
}