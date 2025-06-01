using System;
using UnityEngine;

public class FlyingDemonDeathState : IEnemyState
{
    private IEnemy enemy;
    private Animator animator;
    public FlyingDemonDeathState(IEnemy enemy,Animator animator)
    {
        this.enemy = enemy;
        this.animator = animator;
    }
    public void Enter()
    {
        enemy.OnAnimationEndEvent += OnAnimationEnd;
        animator.SetTrigger("Death");
    }

    private void OnAnimationEnd()
    {
        GameObject.Destroy(animator.gameObject);
    }

    public void Exit()
    {
        enemy.OnAnimationEndEvent -= OnAnimationEnd;
    }
}
