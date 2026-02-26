using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
  public TMP_Text scoreText;
  PlayerController player;

  void OnEnable()
  {
    player = !!GameObject.FindGameObjectWithTag("Player") ? GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>() : null;
  }

  void Update()
  {
    if (!player) return;
    scoreText.text = "Score: " + player.Score;
  }
}