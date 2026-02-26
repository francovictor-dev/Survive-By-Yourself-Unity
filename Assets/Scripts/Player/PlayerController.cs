using System;
using System.Numerics;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityVector2 = UnityEngine.Vector2;
using System.Collections;

public class PlayerController : MonoBehaviour
{
  public Animator animator;
  [Header("Sprite é aqui ")]
  [SerializeField] SpriteRenderer sprite;
  [SerializeField] GameObject hitBox;

  [SerializeField] AudioClip attackSound;
  AudioSource audioSource;
  CameraShake cameraShake;
  HealthBarScript healthBar;

  private UnityVector2 moveInput;
  Stats stats;
  int score = 0;

  bool isDamageCoolDown = false;

  bool isAttacking = false;

  public int Score { get => score; set => score = value; }

  public bool IsAttacking
  {
    get { return isAttacking; }
    set { isAttacking = value; }
  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    stats = GetComponent<Stats>();
    audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    healthBar = GameObject.Find("Canvas/HealthBar").GetComponent<HealthBarScript>();
  }

  public void OnMove(InputAction.CallbackContext context)
  {
    moveInput = context.ReadValue<UnityVector2>();
    bool isWalking = moveInput != UnityVector2.zero;
    animator.SetBool("isWalking", isWalking);
    if (moveInput.x > 0)
    {
      sprite.flipX = false;
      hitBox.transform.eulerAngles = new UnityEngine.Vector3(0, 0, 0);
    }
    else if (moveInput.x < 0)
    {
      sprite.flipX = true;
      hitBox.transform.eulerAngles = new UnityEngine.Vector3(0, 0, 180);
    }

    if (moveInput.x != 0)
    {
      if (moveInput.y > 0)
      {
        hitBox.transform.eulerAngles = new UnityEngine.Vector3(0, 0, 90);
      }
      else if (moveInput.y < 0)
      {
        hitBox.transform.eulerAngles = new UnityEngine.Vector3(0, 0, 270);
      }
    }

    if (!isWalking)
      hitBox.transform.eulerAngles = new UnityEngine.Vector3(0, 0, sprite.flipX ? 180 : 0);

  }

  public void OnAttack(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      int attackIndex;
      attackIndex = UnityEngine.Random.Range(1, 3);
      animator.SetInteger("attackIndex", attackIndex);

      if (moveInput.y > 0)
      {
        if (moveInput.x != 0)
        {
          animator.SetTrigger("attackHorizontal");
          /* if (moveInput.x > 0)
            hitBox.transform.eulerAngles = new UnityEngine.Vector3(0, 0, 0);
          else
            hitBox.transform.eulerAngles = new UnityEngine.Vector3(0, 0, 180); */
        }
        else
        {
          animator.SetTrigger("attackTop");
          //hitBox.transform.eulerAngles = new UnityEngine.Vector3(0, 0, 90);
        }
      }
      else if (moveInput.y < 0)
      {
        if (moveInput.x != 0)
        {
          animator.SetTrigger("attackHorizontal");
          /* if (moveInput.x > 0)
            hitBox.transform.eulerAngles = new UnityEngine.Vector3(0, 0, 0);
          else
            hitBox.transform.eulerAngles = new UnityEngine.Vector3(0, 0, 180); */
        }
        else
        {
          animator.SetTrigger("attackBottom");
          //hitBox.transform.eulerAngles = new UnityEngine.Vector3(0, 0, 270);
        }
      }
      else
      {
        animator.SetTrigger("attackHorizontal");
        /* if (sprite.flipX)
          hitBox.transform.eulerAngles = new UnityEngine.Vector3(0, 0, 180);
        else
          hitBox.transform.eulerAngles = new UnityEngine.Vector3(0, 0, 0); */
      }
    }
  }

  public void HandleAttackSound()
  {
    audioSource.PlayOneShot(attackSound);
  }
  // Update is called once per frame
  void Update()
  {
    transform.Translate(isAttacking ? UnityVector2.zero : moveInput * Time.deltaTime * stats.Speed);
  }

  void OnTriggerStay2D(Collider2D collision)
  {
    if (isDamageCoolDown) return;

    if (collision.gameObject.CompareTag("Enemy"))
    {
      Flash flash = GetComponent<Flash>();
      Rigidbody2D rb = GetComponent<Rigidbody2D>();
      Combat combat = GetComponent<Combat>();
      int enemyAttack = collision.GetComponent<Combat>().Stats.AttackValue;
      audioSource.PlayOneShot(attackSound);
      flash.FlashRed(.2f);
      cameraShake.Shake(.15f, .1f);
      combat.TakeDamage(enemyAttack);
      UnityVector2 dir = (transform.position - collision.gameObject.transform.position).normalized;
      DamageCoolDown();
      rb.AddForce(dir * 10f, ForceMode2D.Impulse);
      healthBar.SetHealthBarValue(stats.LifePoints);
    }
  }
  void DamageCoolDown()
  {
    StartCoroutine(DamageCoolDownRoutine());
  }

  private IEnumerator DamageCoolDownRoutine()
  {
    float cdTime = 0f;
    isDamageCoolDown = true;
    Color color = sprite.color;
    while (cdTime < 2f)
    {
      cdTime += 0.2f;
      color = sprite.color;
      color.a = sprite.color.a == .5f ? 1f : .5f;
      sprite.color = color;
      yield return new WaitForSeconds(0.2f);
    }
    color.a = 1f;
    sprite.color = color;
    isDamageCoolDown = false;
  }

}
