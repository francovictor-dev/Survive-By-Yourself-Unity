using UnityEngine;

public class Fireball : MonoBehaviour
{
  public Vector3 direction;
  public int attackValue;
  [SerializeField] AudioClip fireballSound;
  [SerializeField] AudioClip explosionSound;
  [SerializeField] GameObject explosionPrefab;
  CameraShake cameraShake;
  AudioSource audioSource;
  float speed = 5f;
  float lifeTime = 3f;

  void Awake()
  {
    audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
  }
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    audioSource.PlayOneShot(fireballSound);
  }

  // Update is called once per frame
  void Update()
  {
    transform.position += direction * speed * Time.deltaTime;
    lifeTime -= Time.deltaTime;
    if (lifeTime <= 0)
      Destroy(gameObject);
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Enemy"))
    {
      Flash flash = collision.GetComponent<Flash>();
      Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
      Transform collisionTransform = collision.GetComponent<Transform>();
      Combat combat = collision.GetComponent<Combat>();
      Instantiate(explosionPrefab, transform.position, Quaternion.identity);
      audioSource.PlayOneShot(explosionSound);

      if (flash != null)
      {
        flash.FlashRed(0.2f);
      }

      if (cameraShake)
      {
        cameraShake.Shake(.1f, .05f);
      }
      combat.TakeDamage(attackValue);
      Vector2 dir = (collisionTransform.position - transform.position).normalized;
      rb.AddForce(dir * 10f, ForceMode2D.Impulse);
      Destroy(gameObject);
    }
  }
}
