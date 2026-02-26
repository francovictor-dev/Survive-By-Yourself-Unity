using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  public List<GameObject> enemies;
  public float offset = 2f; // distância fora da tela

  float cdSpwan = 0f;
  float timeGame = 0f;

  int level = 1;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void OnEnable()
  {
    SpawnEnemy();
  }

  void OnDisable()
  {
    level = 1;
    timeGame = 0f;
    cdSpwan = 0f;
  }

  // Update is called once per frame
  void Update()
  {
    cdSpwan += Time.deltaTime;
    timeGame += Time.deltaTime;

    if (timeGame >= 180f)
    {
      level = 10;
    }
    else if (timeGame >= 140f)
    {
      level = 6;
    }
    else if (timeGame >= 100f)
    {
      level = 5;
    }
    else if (timeGame >= 75f)
    {
      level = 4;
    }
    else if (timeGame >= 50f)
    {
      level = 3;
    }
    else if (timeGame >= 25f)
    {
      level = 2;
    }

    if (cdSpwan >= 3f)
    {
      SpawnEnemy();
      cdSpwan = 0f;
    }
  }

  void SpawnEnemy()
  {
    Camera cam = Camera.main;

    // limites da câmera em coordenadas de mundo
    Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
    Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

    float xMin = bottomLeft.x;
    float xMax = topRight.x;
    float yMin = bottomLeft.y;
    float yMax = topRight.y;

    // escolher lado aleatório
    int side = UnityEngine.Random.Range(0, 4);
    Vector3 spawnPos = Vector3.zero;

    switch (side)
    {
      case 0: // esquerda
        spawnPos = new Vector3(xMin - offset, UnityEngine.Random.Range(yMin, yMax), 0);
        break;
      case 1: // direita
        spawnPos = new Vector3(xMax + offset, UnityEngine.Random.Range(yMin, yMax), 0);
        break;
      case 2: // cima
        spawnPos = new Vector3(UnityEngine.Random.Range(xMin, xMax), yMax + offset, 0);
        break;
      case 3: // baixo
        spawnPos = new Vector3(UnityEngine.Random.Range(xMin, xMax), yMin - offset, 0);
        break;
    }

    if (level == 1)
    {
      int index = UnityEngine.Random.Range(0, enemies.Count - 6 + level);
      GameObject enemyPrefab = enemies[index];
      Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
      return;
    }

    int maxSpawn = (int)Math.Floor((double)level / 2);

    int[] spawnSize = new int[maxSpawn];

    foreach (int spawn in spawnSize)
    {
      int index = UnityEngine.Random.Range(0, enemies.Count - 6 + level);
      GameObject enemyPrefab = enemies[index];
      Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
  }

}
