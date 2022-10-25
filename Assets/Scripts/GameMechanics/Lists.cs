using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStringNames 
{
    public static List<string> UnitNames = new List<string> { "Soilders", "Tanks", "Bucephalus", "Bayraktars", "Javelins", "Machine Guns" };
}

public class Supporter
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
}



public class EnemyWave
{
    // 0 - soilderEnemy, 1 - kamazUnit, 2 - tigerUnit, 3 - btrUnit, 4 - tankUnit,

    // list of lists of different enemys on the start wave
    public static List<List<int>> EnemyListStart = new List<List<int>>
    {
        new List<int> { 0, 3, 0, 2, 1 },
        new List<int> { 0, 2, 0, 2, 2 },
        new List<int> { 0, 1, 0, 1, 3 },
    };

    ////////////////////////////////////////////////////////////////////////////////////

    // list of lists of different enemys on the first stage
    public static List<List<int>> EnemyListFirstStage = new List<List<int>>
    {
        new List<int> { 0, 2, 3, 0, 1 },
        new List<int> { 0, 1, 1, 1, 2 },
        new List<int> { 0, 0, 3, 2, 1 },
        new List<int> { 0, 1, 2, 2, 1 },
    };

    ////////////////////////////////////////////////////////////////////////////////////

    // list of lists of different enemys on the second stage part 1
    public static List<List<int>> EnemyListSecondStagePart1 = new List<List<int>>
    {
        new List<int> { 0, 3, 1, 1, 2 },
        new List<int> { 0, 2, 2, 2, 1 },
        new List<int> { 0, 1, 3, 2, 1 },
        new List<int> { 0, 0, 2, 2, 3 },
    };

    // list of lists of different enemys on the second stage part 2
    public static List<List<int>> EnemyListSecondStagePart2 = new List<List<int>>
    {
        new List<int> { 0, 4, 1, 2, 1 },
        new List<int> { 0, 3, 1, 1, 3 },
        new List<int> { 0, 2, 3, 2, 2 },
        new List<int> { 0, 1, 2, 2, 2 },
    };

    // list of lists of different enemys on the second stage part 3
    public static List<List<int>> EnemyListSecondStagePart3 = new List<List<int>>
    {
        new List<int> { 0, 2, 3, 3, 3 },
        new List<int> { 0, 3, 1, 2, 4 },
        new List<int> { 0, 3, 2, 2, 2 },
        new List<int> { 0, 2, 1, 3, 3 },
    };

    ////////////////////////////////////////////////////////////////////////////////////

    // list of lists of different enemys on the third stage part 1
    public static List<List<int>> EnemyListThirdStagePart1 = new List<List<int>>
    {
        new List<int> { 3, 0, 2, 3, 4 },
        new List<int> { 4, 0, 3, 4, 2 },
        new List<int> { 4, 0, 2, 3, 3 },
        new List<int> { 3, 0, 4, 2, 3 },
    };

    // list of lists of different enemys on the third stage part 2
    public static List<List<int>> EnemyListThirdStagePart2 = new List<List<int>>
    {
        new List<int> { 4, 0, 3, 3, 4 },
        new List<int> { 5, 0, 4, 4, 2 },
        new List<int> { 5, 0, 3, 3, 3 },
        new List<int> { 4, 0, 5, 2, 3 },
    };

    // list of lists of different enemys on the third stage part 3
    public static List<List<int>> EnemyListThirdStagePart3 = new List<List<int>>
    {
        new List<int> { 4, 0, 2, 4, 4 },
        new List<int> { 6, 0, 3, 5, 2 },
        new List<int> { 6, 0, 2, 4, 3 },
        new List<int> { 5, 0, 4, 3, 3 },
    };

    ////////////////////////////////////////////////////////////////////////////////////

    // list of lists of different enemys on the forth stage
    public static List<List<int>> EnemyListForthStage = new List<List<int>>
    {
        new List<int> { 5, 0, 2, 2, 1 },
        new List<int> { 4, 0, 1, 3, 2 },
        new List<int> { 5, 0, 1, 2, 2 },
        new List<int> { 6, 0, 1, 1, 1 },
    };
}