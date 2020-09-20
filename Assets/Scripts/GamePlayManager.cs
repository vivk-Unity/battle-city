using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour {

    [SerializeField]
    Transform tankReservePanel;

    [SerializeField]
    Text playerLivesText;

    GameObject tankImage;

    [SerializeField]
    Image topCurtain, bottomCurtain, blackCurtain;

    [SerializeField]
    Text stageNumberText, gameOverText;

    Animator anime;
    SpriteRenderer sprite;

    GameObject[] spawnPoints, spawnPlayerPoints;


    private bool _stageStart = false;
    private bool _tankReserveEmpty = false;

    void Start()
    {
        _stageStart = true;

        stageNumberText.text = "STAGE " + MasterTracker.stageNumber.ToString();

        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        spawnPlayerPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");

        UpdateTankReserve();

        StartCoroutine(StartStage());
    }

    private void Update()
    {
        if (_tankReserveEmpty && GameObject.FindWithTag("Small") == null && GameObject.FindWithTag("Fast") == null && GameObject.FindWithTag("Big") == null && GameObject.FindWithTag("Armored") == null)
        {
            MasterTracker.stageCleared = true;

            int nextStage = MasterTracker.stageNumber + 1;
            LevelCompleted("Stage" + nextStage);
        }
    }

    IEnumerator StartStage()
    {
        StartCoroutine(RevealStageNumber());
        yield return new WaitForSeconds(5);
        StartCoroutine(RevealTopStage());
        StartCoroutine(RevealBottomStage());
        yield return null;
        InvokeRepeating("SpawnEnemy", LevelManager.spawnRate, LevelManager.spawnRate);
        SpawnPlayer();
    }

    IEnumerator RevealStageNumber()
    {
        while (blackCurtain.rectTransform.localScale.y > 0)
        {
            blackCurtain.rectTransform.localScale = new Vector3(1, Mathf.Clamp(blackCurtain.rectTransform.localScale.y - Time.deltaTime, 0, 1), 1);
            yield return null;
        }
    }

    IEnumerator RevealTopStage()
    {
        stageNumberText.enabled = false;
        while (topCurtain.rectTransform.position.y < 1250)
        {
            topCurtain.rectTransform.Translate(new Vector3(0, 500 * Time.deltaTime, 0));
            yield return null;
        }
    }

    IEnumerator RevealBottomStage()
    {
        while (bottomCurtain.rectTransform.position.y > -400)
        {
            bottomCurtain.rectTransform.Translate(new Vector3(0, -500 * Time.deltaTime, 0));
            yield return null;
        }
    }

    public IEnumerator GameOver()
    {
        while (gameOverText.rectTransform.localPosition.y < 0)
        {
            gameOverText.rectTransform.localPosition = new Vector3(gameOverText.rectTransform.localPosition.x, gameOverText.rectTransform.localPosition.y + 120f * Time.deltaTime, gameOverText.rectTransform.localPosition.z);
            yield return null;
        }

        MasterTracker.stageCleared = false;
        LevelCompleted("GameOver");
    }

    public void SpawnEnemy()
    {
        if (LevelManager.smallTanks + LevelManager.fastTanks + LevelManager.bigTanks + LevelManager.armoredTanks > 0)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            anime = spawnPoints[spawnPointIndex].GetComponent<Animator>();
            sprite = spawnPoints[spawnPointIndex].GetComponentInChildren<SpriteRenderer>();
            sprite.color = new Color(1f, 1f, 1f, 1f);
            anime.Play("SpawnPoint");
        }
        else
        {
            _tankReserveEmpty = true;
            CancelInvoke();
        }
    }

    public void SpawnPlayer()
    {
        if (MasterTracker.playerLives > 1)
        {
            if (!_stageStart)
            {
                MasterTracker.playerLives--;
            }
            _stageStart = false;
            anime = spawnPlayerPoints[0].GetComponent<Animator>();
            sprite = spawnPlayerPoints[0].GetComponentInChildren<SpriteRenderer>();
            sprite.color = new Color(1f, 1f, 1f, 1f);
            anime.Play("SpawnPoint");
            UpdatePlayerLives();
        }
        else
        {
            StartCoroutine(GameOver());
        }
    }

    private void LevelCompleted(string sceneName)
    {
        _tankReserveEmpty = false;

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void UpdateTankReserve()
    {
        int j;
        int numberOfTanks = LevelManager.smallTanks + LevelManager.fastTanks + LevelManager.bigTanks + LevelManager.armoredTanks;

        for (j = 0; j < numberOfTanks; j++)
        {
            tankImage = tankReservePanel.transform.GetChild(j).gameObject;
            tankImage.SetActive(true);
        }
    }
    public void RemoveTankReserve()
    {
        int numberOfTanks = LevelManager.smallTanks + LevelManager.fastTanks + LevelManager.bigTanks + LevelManager.armoredTanks;

        tankImage = tankReservePanel.transform.GetChild(numberOfTanks).gameObject;
        tankImage.SetActive(false);
    }

    public void UpdatePlayerLives()
    {
        playerLivesText.text = MasterTracker.playerLives.ToString();
    }



}
