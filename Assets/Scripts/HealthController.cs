using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    [SerializeField]
    private int _actualHealth = 1;
    private int _currentHealth;

    void Start()
    {
        SetHealth();
    }

    public void TakeDamage()
    {
        _currentHealth--;
        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    public void SetHealth()
    {
        _currentHealth = _actualHealth;
    }

    public void SetInvincible()
    {
        _currentHealth = 1000;
    }

    void Death()
    {
        GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
        if (gameObject.CompareTag("Player"))
        {
            GPM.SpawnPlayer();
        }
        else
        {
            if (gameObject.CompareTag("Small")) MasterTracker.smallTankDestroyed++;
            else if (gameObject.CompareTag("Fast")) MasterTracker.fastTankDestroyed++;
            else if (gameObject.CompareTag("Big")) MasterTracker.bigTankDestroyed++;
            else if (gameObject.CompareTag("Armored")) MasterTracker.armoredTankDestroyed++;
        }
        Destroy(gameObject);
    }
}
