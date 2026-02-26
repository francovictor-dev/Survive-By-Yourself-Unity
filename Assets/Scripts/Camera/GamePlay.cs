using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

enum GameState
{
  Menu,
  GamePlay
}

public class GamePlay : MonoBehaviour
{
  public EnemySpawner enemySpawner;
  public GameObject playerPrebab;
  public GameObject canvas;

  public AudioSource audioSource;
  public AudioClip battleMusic;
  public AudioClip menuMusic;

  public TimerText timerText;

  GameObject[] canvasMenus;
  GameObject[] canvasGamePlay;
  GameObject playerInstance;
  bool isRestarted = false;

  int scoreTotal = 0;

  void Start()
  {
    canvasMenus = GameObject.FindGameObjectsWithTag("Menu");
    canvasGamePlay = GameObject.FindGameObjectsWithTag("GamePlay");

    audioSource.clip = menuMusic;
    audioSource.Play();
    GameScene(GameState.Menu);
  }

  void Update()
  {
    if (playerInstance)
    {
      scoreTotal = playerInstance.GetComponent<PlayerController>().Score;
      return;
    }

    if (isRestarted)
    {
      GameOver();
      isRestarted = false;
    }
  }

  public void StartGame()
  {
    isRestarted = true;
    audioSource.clip = battleMusic;
    audioSource.Play();
    playerInstance = Instantiate(playerPrebab, Vector3.zero, Quaternion.identity);
    enemySpawner.enabled = true;
    GameScene(GameState.GamePlay);
  }

  void GameOver()
  {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    GameObject[] animations = GameObject.FindGameObjectsWithTag("Animation");
    GameObject[] objects = GameObject.FindGameObjectsWithTag("Iten").Concat(enemies).ToArray().Concat(animations).ToArray();

    foreach (GameObject obj in objects)
    {
      Destroy(obj);
    }

    // salvar pontuação
    scoreTotal += (int)timerText.ElapsedTime * 30;
    int currentScore = PlayerPrefs.GetInt("score", 0);

    if (scoreTotal > currentScore)
    {
      PlayerPrefs.SetInt("score", scoreTotal);
      PlayerPrefs.Save(); // força salvar imediatamente
    }

    audioSource.clip = menuMusic;
    audioSource.Play();
    enemySpawner.enabled = false;
    GameScene(GameState.Menu);
  }

  void GameScene(GameState gameState)
  {

    bool isStartGame = gameState == GameState.GamePlay;
    foreach (GameObject menu in canvasMenus)
    {
      menu.SetActive(!isStartGame);
    }
    foreach (GameObject gameplay in canvasGamePlay)
    {
      gameplay.SetActive(isStartGame);
    }

  }
}