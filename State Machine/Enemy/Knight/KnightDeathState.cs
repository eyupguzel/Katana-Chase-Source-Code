using UnityEngine;

public class KnightDeathState : IEnemyState
{
    private Animator animator;
    private IEnemy enemy;
    public KnightDeathState(IEnemy enemy,Animator animator)
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
