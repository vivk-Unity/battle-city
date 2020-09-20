using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterTracker : MonoBehaviour {

    public static int smallTankDestroyed, fastTankDestroyed, bigTankDestroyed, armoredTankDestroyed;
    public static int stageNumber;

    public static int _playerLives = 3;

    [SerializeField]
    public static int playerLives = 3;
    public static int playerScore = 0;

    public static bool stageCleared = false;

    [SerializeField]
    int smallTankPoints = 100, fastTankPoints = 200, bigTankPoints = 300, armoredTankPoints = 400;
    public int SmallTankPoints { get { return smallTankPoints; } }
    public int FastTankPoints { get { return fastTankPoints; } }
    public int BigTankPoints { get { return bigTankPoints; } }
    public int ArmoredTankPoints { get { return armoredTankPoints; } }

    static MasterTracker instance = null;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        else if (SceneManager.GetActiveScene().name == "Menu")
        {
            Destroy(gameObject);
        }
    }

}


