using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
  public TMP_Text timerText;
  private float elapsedTime;

  public float ElapsedTime { get => elapsedTime; set => elapsedTime = value; }

  void OnEnable()
  {
    elapsedTime = 0f;
  }

  void Update()
  {
    // acumula tempo
    elapsedTime += Time.deltaTime;

    // converte para minutos e segundos
    int minutes = Mathf.FloorToInt(elapsedTime / 60f);
    int seconds = Mathf.FloorToInt(elapsedTime % 60f);

    // formata no estilo 00:00
    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
  }

}
