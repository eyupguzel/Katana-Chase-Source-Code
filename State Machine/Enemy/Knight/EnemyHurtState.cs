using System;
using UnityEngine;

public class EnemyHurtState : IEnemyState
{
    private readonly IEnemy enemy;
    private readonly EnemyStateMachine stateMachine;
    private readonly Animator animator;

    public EnemyHurtState(IEnemy enemy, Animator animator, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animator = animator;
    }

    public void Enter()
    {
        enemy.OnAnimationEndEvent += OnAnimationEnd;
        animator.SetTrigger("Hurt");
    }
    private void OnAnimationEnd()
    {
        stateMachine.ChangeEnemyState(new EnemyIdleState(enemy, animator, stateMachine));
    }
    public void Exit()
    {
        enemy.OnAnimationEndEvent -= OnAnimationEnd;
        animator.ResetTrigger("Hurt");
    }
}
