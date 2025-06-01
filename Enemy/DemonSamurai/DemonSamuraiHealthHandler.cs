using UnityEngine;

public class DemonSamuraiHealthHandler : IHealth
{
    private IEnemy enemy;
    private EnemySO enemySO;
    private Animator animator;
    private EnemyStateMachine stateMachine;

    private int maxHealth;
    private int health;
    public DemonSamuraiHealthHandler(IEnemy enemy,EnemySO enemySO, Animator animator, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.enemySO = enemySO;
        this.animator = animator;
        this.stateMachine = stateMachine;

        maxHealth = enemySO.maxHealth;
        health = maxHealth;
    }

    public float GetCurrentHealth()
    {
        if (health < 0)
            return 0;
        else
            return health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        EventBus<OnBossTakeDamage>.Publish(new OnBossTakeDamage() { health = health });
        if (health <= 0)
        {
            stateMachine.ChangeEnemyState(new DemonSamuraiDeathState(enemy,animator));
            enemy.IsDead = true;
        }
    }
}
