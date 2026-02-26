using UnityEngine;
using TMPro;
public class FinalScore : MonoBehaviour
{
  public TMP_Text scoreText;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void OnEnable()
  {
    // ler pontuação
    scoreText.text = "Maior ponto" + ": " + PlayerPrefs.GetInt("score", 0);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
