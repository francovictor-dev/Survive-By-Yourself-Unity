using UnityEngine;
using System.Collections;
public class BuffParticleEffect : MonoBehaviour
{
  public GameObject target;
  public Color targetColor;

  Transform targetTransform;
  SpriteRenderer targetSprite;
  ParticleSystem particle;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    targetTransform = target.GetComponent<Transform>();
    targetSprite = target.GetComponent<SpriteRenderer>();
    particle = GetComponent<ParticleSystem>();
    var main = particle.main;
    main.startColor = targetColor;
    StartCoroutine(LightEffectRoutine());
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y - .2f, targetTransform.position.y - 1);
  }

  private IEnumerator LightEffectRoutine()
  {
    Color defaultColor = Color.white;
    float multTime = 6f;
    int times = 0;

    float colorValue;
    while (times < 5)
    {
      colorValue = 0f;
      while (colorValue <= 1f)
      {
        colorValue += Time.deltaTime * multTime;
        targetSprite.color = Color.Lerp(defaultColor, targetColor, colorValue);
        yield return null;
      }
      colorValue = 0f;

      while (colorValue <= 1f)
      {
        colorValue += Time.deltaTime * multTime;
        targetSprite.color = Color.Lerp(targetColor, defaultColor, colorValue);
        yield return null;
      }
      times++;
    }
    Destroy(gameObject);
  }
}
