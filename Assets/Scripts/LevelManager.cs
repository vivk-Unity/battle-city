using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    int smallTanksInThisLevel, fastTanksInThisLevel, bigTanksInThisLevel, armoredTanksInThisLevel, stageNumber;

    [SerializeField]
    float spawnRateInThisLevel = 5;

    public static float spawnRate { get; private set; }

    public static int smallTanks, fastTanks, bigTanks, armoredTanks;

    private void Awake()
    {
        MasterTracker.stageNumber = stageNumber;
        smallTanks = smallTanksInThisLevel;
        fastTanks = fastTanksInThisLevel;
        bigTanks = bigTanksInThisLevel;
        armoredTanks = armoredTanksInThisLevel;
        spawnRate = spawnRateInThisLevel;
    }

}
