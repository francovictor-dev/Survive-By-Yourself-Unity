using Unity.VisualScripting;
using UnityEngine;

public class Iten : MonoBehaviour
{
  public enum Type
  {
    HealthCure, ImproveSpeed, ImproveAttack, GetMoney
  }

  [SerializeField] int value;
  [SerializeField] Type type;
  [SerializeField] SpriteRenderer sprite;
  [SerializeField] AudioClip audioClip;
  HealthBarScript healthBarScript;

  AudioSource audioSource;
  void Start()
  {
    Camera cam = Camera.main;
    audioSource = cam.GetComponent<AudioSource>();
    healthBarScript = GameObject.Find("Canvas/HealthBar").GetComponent<HealthBarScript>();
  }

  // Update is called once per frame
  void Update()
  {
    //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    PlayerController player = collision.GetComponent<PlayerController>();
    if (collision.CompareTag("Player"))
    {
      Stats stats = player.GetComponent<Stats>();
      Transform playerTransform = player.GetComponent<Transform>();
      Animator playerAnimator = player.GetComponent<Animator>();

      audioSource.PlayOneShot(audioClip);
      GameObject prefab = Resources.Load<GameObject>("Prefabs/BuffParticleEffect");
      GameObject instance = Instantiate(prefab, playerTransform.position, Quaternion.identity);
      BuffParticleEffect script = instance.GetComponent<BuffParticleEffect>();

      switch (type)
      {
        case Type.HealthCure:
          stats.LifePoints += value;
          script.target = player.gameObject;
          script.targetColor = new Color(0, 1f, 0.09411765f);

          healthBarScript.SetHealthBarValue(stats.LifePoints);
          if (stats.LifePoints > 200)
          {
            stats.LifePoints = 200;
            healthBarScript.SetHealthBarValue(200);
          }

          break;
        case Type.GetMoney:
          player.Score += value;
          script.target = player.gameObject;
          script.targetColor = new Color(1, 0.8171908f, 0.2783019f);
          break;
        case Type.ImproveAttack:
          stats.AttackValue += value;
          script.target = player.gameObject;
          script.targetColor = new Color(1, 0.2971698f, 0.2971698f);
          break;
        case Type.ImproveSpeed:
          float speedValue = value / 50f;
          stats.Speed += speedValue;
          script.target = player.gameObject;
          script.targetColor = new Color(0.2028302f, 0.6840283f, 1);
          playerAnimator.speed += speedValue / 1.5f;
          break;
        default: break;
      }
      Destroy(gameObject);
    }
  }





}
