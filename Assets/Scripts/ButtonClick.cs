using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour {

	public void OnItemClick(string stageName)
    {
        MasterTracker.playerLives = MasterTracker._playerLives; 

        SceneManager.LoadScene(stageName);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
