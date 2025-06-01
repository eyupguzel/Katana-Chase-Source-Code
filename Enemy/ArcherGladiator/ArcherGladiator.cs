using System;
using UnityEngine;

public class ArcherGladiator : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject arrowProjectile;
    [SerializeField] private EnemySO enemySO;
    public EnemySO EnemySO => enemySO;
    public Vector3 Position { get; set; }
    public Action OnAnimationEndEvent { get; set; }
    public bool IsDead { get; set; }
    public IHealth HealthHandler => health;

    private EnemyStateMachine enemyState;

    private IMovementHandler movementHandler;
    private IAttackHandler attackHandler;
    private IHealth health;

    private Animator animator;

    private EnemyHealthBar healthBar;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        healthBar = GetComponent<EnemyHealthBar>();
        movementHandler = new ArcherGladiatorMovementHandler(transform);
        enemyState = new EnemyStateMachine();
        health = new ArcherGladiatorHealthHandler(this,enemySO, animator, enemyState);
        enemyState.ChangeEnemyState(new ArcherGladiatorIdleState(this, animator, enemyState));
        attackHandler = new ArcherGladiatorAttackHandler(EnemySO, this,animator, enemyState);
    }
    void Update()
    {
        if(IsDead) return;

        Move();

        if (movementHandler.SearchPlayer())
        {
            Attack();
        }
        enemyState.Update();
    }
    public void Attack()
    {
        attackHandler.Attack(movementHandler.GetPlayer());
    }

    public void Move()
    {
        if (enemyState.currentEnemyState is ArcherGladiatorMovementState or ArcherGladiatorIdleState)
        {
            movementHandler.Move();
            Position = transform.position;
        }
    }

    public void TakeDamage(IAttack attack, int damage)
    {
        if (IsDead) return;
        health.TakeDamage(damage);
        HitEffectPlayer.Instance.PlayHitEffect(transform.position,attack.AttackData.hitVfx,attack.AttackData.hitSfx,(int)transform.localScale.x);
        healthBar.UpdateHealthBar();
    }

    public void OnAttackAnimation()
    {
        Instantiate(arrowProjectile, transform.position, Quaternion.identity,transform);
    }
    public void OnAnimationEnd()
    {
        OnAnimationEndEvent?.Invoke();
    }
}
