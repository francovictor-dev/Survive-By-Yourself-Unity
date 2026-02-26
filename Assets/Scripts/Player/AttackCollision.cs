using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class AttackCollision : MonoBehaviour
{
  [SerializeField] PlayerController player;
  CameraShake cameraShake;
  AudioSource audioSource;
  [SerializeField] AudioClip swordSound;

  [SerializeField] BoxCollider2D boxCollider;


  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
  }

  void Update()
  {
    transform.position = player.transform.position;


  }


  void OnTriggerEnter2D(Collider2D collision)
  {

    if (collision.CompareTag("Enemy"))
    {
      TakeDamage(collision);
    }
  }

  void TakeDamage(Collider2D collision)
  {
    Flash flash = collision.GetComponent<Flash>();
    Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
    Transform collisionTransform = collision.GetComponent<Transform>();
    Combat combat = collision.GetComponent<Combat>();

    audioSource.PlayOneShot(swordSound, 0.3f);

    if (flash != null)
    {
      flash.FlashRed(0.2f);
    }

    if (cameraShake)
    {
      cameraShake.Shake(.15f, .1f);
    }

    combat.TakeDamage(player.GetComponent<Combat>().Stats.AttackValue);
    Vector2 dir = (collisionTransform.position - transform.position).normalized;
    rb.AddForce(dir * 10f, ForceMode2D.Impulse);
    if (combat.Stats.LifePoints <= 0)
    {
      EnemyBase enemyBase = collision.GetComponent<EnemyBase>();

      player.Score += enemyBase.score;
    }
  }


}
