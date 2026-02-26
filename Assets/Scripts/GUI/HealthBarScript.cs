using System;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
  [SerializeField] Image healthBar;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void OnEnable()
  {
    SetHealthBarValue(200);
  }
  public void SetHealthBarValue(float value)
  {
    healthBar.fillAmount = value / 200;
  }
}
