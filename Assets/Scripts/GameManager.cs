using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{

    [Header("Player")]
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
    }
}
