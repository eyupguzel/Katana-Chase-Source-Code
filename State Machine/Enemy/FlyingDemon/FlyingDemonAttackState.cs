using UnityEngine;

public class FlyingDemonAttackState : IEnemyState
{
    private IEnemy enemy;
    private Animator animator;
    private EnemyStateMachine enemyStateMachine;

    public FlyingDemonAttackState(IEnemy enemy, Animator animator, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.animator = animator;
        this.enemyStateMachine = enemyStateMachine;
    }
    public void Enter()
    {
        enemy.OnAnimationEndEvent += OnAnimationEnd;
        animator.SetTrigger("Attack");
    }
    private void OnAnimationEnd()
    {
        enemyStateMachine.ChangeEnemyState(new FlyingDemonIdle(enemy, animator, enemyStateMachine));
    }
    public void Exit()
    {
        enemy.OnAnimationEndEvent -= OnAnimationEnd;
        animator.ResetTrigger("Attack");
    }
}
