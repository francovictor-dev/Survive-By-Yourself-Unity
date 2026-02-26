using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public enum DirectionType
{
  Up, Down, Horizontal
}
public class Combat : MonoBehaviour
{
  [SerializeField] Stats stats;
  [SerializeField] ParticleSystem damageParticle;
  [SerializeField] GameObject fireballPrefab;

  [SerializeField] GameObject hitBox;
  private ParticleSystem particleInstance;
  public Stats Stats { get => stats; set => stats = value; }

  void Update()
  {
    if (particleInstance)
    {
      particleInstance.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
    }
  }

  public void TakeDamage(int amount)
  {
    stats.LifePoints -= amount;
    if (stats.LifePoints <= 0)
    {
      StartCoroutine(Die());
    }
  }

  private IEnumerator Die()
  {
    SpawnDamageParticle();
    yield return new WaitForSeconds(0.7f);
    Destroy(particleInstance.gameObject);
    Destroy(gameObject);
  }

  public void Attack()
  {

    //hitBox.GetComponent<Rigidbody2D>();
    //hitBox.GetComponent<BoxCollider2D>().enabled = true;
    hitBox.SetActive(true);
    /* GameObject prefab = Instantiate(fireballPrefab, hitBox.position, Quaternion.identity);
    Fireball fireball = prefab.GetComponent<Fireball>();
    Vector3 direction = new(1, 0, 0);
    fireball.direction = hitBox.position;
    fireball.attackValue = 0; */
  }

  public void AttackOff()
  {
    //hitBox.GetComponent<BoxCollider2D>().enabled = false;
    hitBox.SetActive(false);
  }

  // TODO: REFACTOR THIS METHOD TO LAUNCH FIREBALL IN DIFFERENT DIRECTIONS
  public void LaunchFireball(DirectionType direction)
  {
    if (stats.AttackValue < 100) return;
    switch (direction)
    {
      case DirectionType.Horizontal:
        {
          SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
          GameObject prefab = Instantiate(fireballPrefab, transform.position + new Vector3(spriteRenderer.flipX ? -1 : 1, 0, 0), Quaternion.identity);
          Fireball fireball = prefab.GetComponent<Fireball>();
          prefab.GetComponent<SpriteRenderer>().flipX = spriteRenderer.flipX;

          fireball.direction = new Vector3(spriteRenderer.flipX ? -1 : 1, 0, 0);
          fireball.attackValue = Mathf.FloorToInt(stats.AttackValue * 0.3f);

          if (stats.AttackValue >= 200)
          {
            GameObject prefab2 = Instantiate(fireballPrefab, transform.position + new Vector3(spriteRenderer.flipX ? -0.8f : 0.8f, 0, 0), Quaternion.identity);
            Fireball fireball2 = prefab2.GetComponent<Fireball>();
            prefab2.transform.rotation = Quaternion.Euler(0, 0, spriteRenderer.flipX ? 135 : 45);
            fireball2.direction = new Vector3(spriteRenderer.flipX ? -0.8f : 0.8f, 0.5f, 0);
            fireball2.attackValue = Mathf.FloorToInt(stats.AttackValue * 0.3f);

            GameObject prefab3 = Instantiate(fireballPrefab, transform.position + new Vector3(spriteRenderer.flipX ? -0.8f : 0.8f, 0, 0), Quaternion.identity);
            Fireball fireball3 = prefab3.GetComponent<Fireball>();
            prefab3.transform.rotation = Quaternion.Euler(0, 0, spriteRenderer.flipX ? 225 : 315);
            fireball3.direction = new Vector3(spriteRenderer.flipX ? -0.8f : 0.8f, -0.5f, 0);
            fireball3.attackValue = Mathf.FloorToInt(stats.AttackValue);
          }
          break;
        }
      case DirectionType.Up:
        {
          GameObject prefab = Instantiate(fireballPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
          Fireball fireball = prefab.GetComponent<Fireball>();
          prefab.transform.rotation = Quaternion.Euler(0, 0, 90);
          fireball.direction = new Vector3(0, 1, 0);
          fireball.attackValue = Mathf.FloorToInt(stats.AttackValue * 0.3f);

          if (stats.AttackValue >= 200)
          {
            GameObject prefab2 = Instantiate(fireballPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            Fireball fireball2 = prefab2.GetComponent<Fireball>();
            prefab2.transform.rotation = Quaternion.Euler(0, 0, 45);
            fireball2.direction = new Vector3(0.5f, 0.8f, 0);
            fireball2.attackValue = Mathf.FloorToInt(stats.AttackValue * 0.3f);

            GameObject prefab3 = Instantiate(fireballPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            Fireball fireball3 = prefab3.GetComponent<Fireball>();
            prefab3.transform.rotation = Quaternion.Euler(0, 0, 135);
            fireball3.direction = new Vector3(-0.5f, 0.8f, 0);
            fireball3.attackValue = Mathf.FloorToInt(stats.AttackValue * 0.3f);
          }
          break;
        }
      default:
        {
          GameObject prefab = Instantiate(fireballPrefab, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
          Fireball fireball = prefab.GetComponent<Fireball>();
          prefab.transform.rotation = Quaternion.Euler(0, 0, 270);
          fireball.direction = new Vector3(0, -1, 0);
          fireball.attackValue = Mathf.FloorToInt(stats.AttackValue / 2);

          if (stats.AttackValue >= 200)
          {
            GameObject prefab2 = Instantiate(fireballPrefab, transform.position + new Vector3(0, -0.8f, 0), Quaternion.identity);
            Fireball fireball2 = prefab2.GetComponent<Fireball>();
            prefab2.transform.rotation = Quaternion.Euler(0, 0, 315);
            fireball2.direction = new Vector3(0.5f, -0.8f, 0);
            fireball2.attackValue = Mathf.FloorToInt(stats.AttackValue / 2);

            GameObject prefab3 = Instantiate(fireballPrefab, transform.position + new Vector3(0, -0.8f, 0), Quaternion.identity);
            Fireball fireball3 = prefab3.GetComponent<Fireball>();
            prefab3.transform.rotation = Quaternion.Euler(0, 0, 225);
            fireball3.direction = new Vector3(-0.5f, -0.8f, 0);
            fireball3.attackValue = Mathf.FloorToInt(stats.AttackValue / 2);
          }
          break;
        }
    }
  }

  void SpawnDamageParticle()
  {
    particleInstance = Instantiate(damageParticle, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity);
  }
}