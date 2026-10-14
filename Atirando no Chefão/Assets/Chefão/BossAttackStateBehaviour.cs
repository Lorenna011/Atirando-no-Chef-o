using UnityEngine;

public class BossAttackStateBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        BossHighSingleShot ataque =
            animator.GetComponent<BossHighSingleShot>();

        if (ataque != null)
            ataque.AtivarComportamento();
    }

    public override void OnStateExit(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        BossHighSingleShot ataque =
            animator.GetComponent<BossHighSingleShot>();

        if (ataque != null)
            ataque.DesativarComportamento();
    }
}