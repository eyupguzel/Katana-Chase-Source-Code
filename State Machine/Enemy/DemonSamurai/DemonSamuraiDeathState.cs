using System;
using UnityEngine;

public class DemonSamuraiDeathState : IEnemyState
{
    private IEnemy enemy;
    private Animator animator;
    public DemonSamuraiDeathState(IEnemy enemy,Animator animator)
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
