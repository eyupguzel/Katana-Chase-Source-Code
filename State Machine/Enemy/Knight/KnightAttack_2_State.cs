using UnityEngine;

public class KnightAttack_2_State : IEnemyState
{
    private IEnemy enemy;
    private Animator animator;
    private EnemyStateMachine enemyState;
    public KnightAttack_2_State(IEnemy enemy, Animator animator, EnemyStateMachine enemyState)
    {
        this.enemy = enemy;
        this.animator = animator;
        this.enemyState = enemyState;
    }
    public void Enter()
    {
        enemy.OnAnimationEndEvent += OnAnimationEnd;
        animator.SetTrigger("Attack_2");
    }
    private void OnAnimationEnd()
    {
        enemyState.ChangeEnemyState(new EnemyIdleState(enemy, animator, enemyState));
    }
    public void Exit()
    {
        enemy.OnAnimationEndEvent -= OnAnimationEnd;
        animator.ResetTrigger("Attack_2");
    }
}
