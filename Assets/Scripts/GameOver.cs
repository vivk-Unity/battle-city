using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    private float _timer = 0.0F;
    private float _timerMax = 5.0F;

    void Update () {

        _timer += Time.deltaTime;


        if (_timer >= _timerMax)
        {
            SceneManager.LoadScene("Menu");
            
        }
    }
}
