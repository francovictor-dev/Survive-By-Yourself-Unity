using UnityEngine;

public class AttackState : StateMachineBehaviour
{
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    var controller = animator.GetComponent<PlayerController>();
    controller.IsAttacking = true; // trava movimento ao entrar no ataque
  }

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    var controller = animator.GetComponent<PlayerController>();
    controller.IsAttacking = false; // libera movimento ao sair do ataque
  }

}
