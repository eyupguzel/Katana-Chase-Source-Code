using System;
using UnityEngine;

public class Knight : MonoBehaviour, IEnemy
{
    public Vector3 Position { get; set; }
    public Action OnAnimationEndEvent { get; set; }
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private LayerMask layerMask;
    public EnemySO EnemySO => enemySO;
    public bool IsDead { get; set; }

    public IHealth HealthHandler => healthHandler;

    private Animator animator;

    private IMovementHandler movementHandler;
    private IDamaging attackHandler;
    public IHealth healthHandler;

    private EnemyStateMachine enemyStateMachine;
    private EnemyHealthBar healthBar;
    private void Start()
    {
        animator = GetComponent<Animator>();
        movementHandler = new MovementHandler(transform, EnemySO, layerMask);
        enemyStateMachine = new EnemyStateMachine();
        healthHandler = new HealthHandler(this, EnemySO, animator, enemyStateMachine);
        enemyStateMachine.ChangeEnemyState(new EnemyMovementState(this, animator, enemyStateMachine));
        attackHandler = new AttackHandler(EnemySO, enemyStateMachine, this, animator, transform);

        healthBar = GetComponent<EnemyHealthBar>();
    }
    void Update()
    {
        if (IsDead) return;

        Move();
        if (movementHandler.SearchPlayer())
        {
            Player player = movementHandler.GetPlayer();
            Attack();
        }

        enemyStateMachine.Update();
        enemyStateMachine.GetType();
    }
    public void Attack()
    {
        if (movementHandler.ControlDistance())
        {
            attackHandler.Attack(movementHandler.GetPlayer());
        }
    }

    public void Move()
    {
        if (enemyStateMachine.currentEnemyState is EnemyMovementState or EnemyIdleState)
        {
            movementHandler.Move();
            Position = transform.position;
        }
    }

    public void TakeDamage(IAttack attack, int damage)
    {
        if (IsDead) return;

        HitEffectPlayer.Instance.PlayHitEffect(transform.position, attack.AttackData.hitVfx, attack.AttackData.hitSfx, transform.localScale.x);
        enemyStateMachine.ChangeEnemyState(new EnemyHurtState(this, animator, enemyStateMachine));
        healthHandler.TakeDamage(damage);
        healthBar.UpdateHealthBar();
    }
    public void OnAttackAnimation()
    {
        attackHandler.DealDamage();
    }
    public void OnAnimationEnd()
    {
        OnAnimationEndEvent?.Invoke();
    }
}
