using UnityEngine;

public class ArcherGladiatorDeathState : IEnemyState
{
    private IEnemy enemy;
    private Animator animator;
    
    public ArcherGladiatorDeathState(IEnemy enemy,Animator animator)
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
