using UnityEngine;

public class DemonSamuraiShoutState : IEnemyState
{
    private IEnemy enemy;
    private Animator animator;
    private EnemyStateMachine stateMachine;
    public DemonSamuraiShoutState(IEnemy enemy, Animator animator, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.animator = animator;
        this.stateMachine = stateMachine;
    }
    public void Enter()
    {
        enemy.OnAnimationEndEvent += OnAnimationEnd;
        animator.SetTrigger("Shout");
    }
    private void OnAnimationEnd()
    {
        stateMachine.ChangeEnemyState(new DemonSamuraiIdleState(enemy, animator, stateMachine));
    }
    public void Exit()
    {
        enemy.OnAnimationEndEvent -= OnAnimationEnd;
        animator.ResetTrigger("Shout");
    }
}
