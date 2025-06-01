using UnityEngine;

public class DemonSamuraiAttack1State : IEnemyState
{
    private IEnemy enemy;
    private Animator animator;
    private EnemyStateMachine stateMachine;
    public DemonSamuraiAttack1State(IEnemy enemy, Animator animator, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.animator = animator;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        enemy.OnAnimationEndEvent += OnAnimationEnd;
        animator.SetTrigger("Attack1");
    }
    private void OnAnimationEnd()
    {
        stateMachine.ChangeEnemyState(new DemonSamuraiIdleState(enemy,animator,stateMachine));
    }
    public void Exit()
    {
        enemy.OnAnimationEndEvent -= OnAnimationEnd;
        animator.ResetTrigger("Attack1");
    }
}
