using UnityEngine;

public class Flash : MonoBehaviour
{
  [SerializeField] SpriteRenderer spriteRenderer;
  private Color originalColor;

  void Start()
  {
    originalColor = spriteRenderer.color;
  }

  public void FlashRed(float seconds)
  {
    StartCoroutine(FlashRoutine(seconds));
  }

  private System.Collections.IEnumerator FlashRoutine(float seconds)
  {
    spriteRenderer.color = Color.red;       // muda para vermelho
    yield return new WaitForSeconds(seconds);  // espera 0.1 segundos
    spriteRenderer.color = originalColor;   // volta para cor original
  }
}
