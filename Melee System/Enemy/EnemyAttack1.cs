using UnityEngine;

public class EnemyAttack1 : IEnemyState
{
    private readonly EnemyStateMachine enemyStateMachine;
    private readonly Animator animator;
    private readonly IEnemy enemy;

    private EventBinding<OnKnightAnimationEndEvent> eventBinding;

    public EnemyAttack1(IEnemy enemy,EnemyStateMachine enemyStateMachine, Animator animator)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
        this.animator = animator;
    }
    
    public void Enter()
    {
        eventBinding = new EventBinding<OnKnightAnimationEndEvent>(OnAnimationEnd);
        EventBus<OnKnightAnimationEndEvent>.Subscribe(eventBinding);

        animator.SetTrigger("Attack_1");
    }
    private void OnAnimationEnd()
    {
        enemyStateMachine.ChangeEnemyState(new EnemyMovementState(enemy, animator, enemyStateMachine));
    }
    public void Update()
    {
    }
    public void Exit()
    {
        animator.ResetTrigger("Attack_1");
        EventBus<OnKnightAnimationEndEvent>.Unsubscribe(eventBinding);
    }
}
