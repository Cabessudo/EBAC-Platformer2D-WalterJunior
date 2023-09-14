using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager>
{

    [Header("References")]
    public GameObject gameOverText;
    private GameObject _player;
    

    [Header("Variables")]
    public bool gameOver;
    public int coin;

    void Update()
    {
        FindPlayer();
        
        if(gameOver)
        ShowGameOver();
    }

    void FindPlayer()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if(_player == null)
        gameOver = true;
    }

    void ShowGameOver()
    {
        gameOverText.SetActive(true);
        gameObject.transform.DOScale(new Vector2(1.1f, 1.1f), 1).SetLoops(-1, LoopType.Yoyo);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /*[Header("Player")]
    public GameObject playerPrefab;
    private GameObject _currentPlayer;

    [Header("References")]
    public Transform spawnPoint;
    
    [Header("Enemies")]
    public GameObject[] enemies;

    [Header("Animation")]
    public Ease ease;
    public float duration;
    public float delay;

    void Awake()
    {
        if(Instance == null)
           Instance = this;
        else
           Destroy(gameObject);
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        _currentPlayer = Instantiate(playerPrefab);
        _currentPlayer.SetActive(true);
        _currentPlayer.transform.position = spawnPoint.position;
        _currentPlayer.transform.DOScale(0, duration).SetEase(ease).SetDelay(delay).From();
    }*/
}
