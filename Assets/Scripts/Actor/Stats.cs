using UnityEngine;
using UnityVector3 = UnityEngine.Vector3;
public class Stats : MonoBehaviour
{
  [Header("Atributos")]
  [SerializeField] private int lifePoints = 100;
  [SerializeField] private int attackValue = 10;
  [SerializeField] private float speed = 5f;

  public int LifePoints { get => lifePoints; set => lifePoints = value; }
  public int AttackValue { get => attackValue; set => attackValue = value; }
  public float Speed { get => speed; set => speed = value; }

  void Update()
  {
    transform.position = new UnityVector3(transform.position.x, transform.position.y, transform.position.y);
  }
}
