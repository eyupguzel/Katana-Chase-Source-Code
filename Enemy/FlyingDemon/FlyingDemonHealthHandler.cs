using UnityEngine;

public class FlyingDemonHealthHandler : IHealth
{
    private IEnemy enemy;
    public float maxHealth;
    private float health;
    private EnemySO enemySO;
    private Animator animator;
    private EnemyStateMachine stateMachine;

    public FlyingDemonHealthHandler(IEnemy enemy,EnemySO enemySO, Animator animator, EnemyStateMachine stateMachine)
    {
        this.enemySO = enemySO;
        this.animator = animator;
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        maxHealth = enemySO.maxHealth;
        health = maxHealth;

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
            enemy.IsDead = true;
        }
    }
    private void Die()
    {
        stateMachine.ChangeEnemyState(new FlyingDemonDeathState(enemy,animator));
    }
    public float GetCurrentHealth()
    {
        if (health < 0)
            return 0;
        else
            return health;
    }
}
