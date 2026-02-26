using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
  [SerializeField] Animator animator;
  public int score = 10;
  public List<Iten> itens;

  GameObject player;
  Stats stats;
  SpriteRenderer spriteRenderer;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    stats = GetComponent<Stats>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    animator.speed = stats.Speed;
  }

  // Update is called once per frame
  void Update()
  {

    if (player == null) return;

    transform.position = Vector3.MoveTowards(
        transform.position,
        player.transform.position,
        stats.Speed * Time.deltaTime
    );

    bool isPositionXLessThanPlayer = transform.position.x < player.transform.position.x;

    spriteRenderer.flipX = isPositionXLessThanPlayer;

  }

  void OnDestroy()
  {
    if (stats.LifePoints > 0) return;

    itens.Add(null);
    itens.Add(null);
    int index = Random.Range(0, itens.Count);
    Iten choosed = itens[index];
    if (choosed == null) return;
    Instantiate(choosed, transform.position, Quaternion.identity);
  }
}
