using UnityEngine;

public class ArcherGladiatorAttackState : IEnemyState
{
    private IEnemy enemy;
    private Animator animator;
    private EnemyStateMachine stateMachine;

    public ArcherGladiatorAttackState(IEnemy enemy, Animator animator, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.animator = animator;
        this.stateMachine = stateMachine;
    }
    public void Enter()
    {
        enemy.OnAnimationEndEvent += OnAnimationEnd;
        animator.SetTrigger("Attack");
    }
    private void OnAnimationEnd()
    {
        stateMachine.ChangeEnemyState(new ArcherGladiatorIdleState(enemy, animator, stateMachine));
    }
    public void Exit()
    {
        enemy.OnAnimationEndEvent -= OnAnimationEnd;
        animator.ResetTrigger("Attack");
    }
}
