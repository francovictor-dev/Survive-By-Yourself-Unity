using UnityEngine;

public class Explosion : MonoBehaviour
{
  AudioSource audioSource;
  [SerializeField] AudioClip explosionSound;

  void Start()
  {
    audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    audioSource.PlayOneShot(explosionSound, 0.05f);
  }
  public void FinishAnimation()
  {
    Destroy(gameObject);
  }

}
