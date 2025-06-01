using System;
using System.Xml.Serialization;
using UnityEngine;

public class FlyingDemon : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private GameObject projectile;
    private AudioSource audioSource;
    public EnemySO EnemySO => enemySO;
    private IMovementHandler movementHandler;
    private IAttackHandler attackHandler;
    private IHealth healthHandler;

    private EnemyStateMachine enemyStateMachine;

    private Animator animator;
    private GameObject alertObject;

    private EnemyHealthBar healthBar;
    public Vector3 Position { get; set; }
    public bool IsDead { get; set; }
    public Action OnAnimationEndEvent { get; set; }
    public IHealth HealthHandler => healthHandler;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        movementHandler = new FlyingDemonMovementHandler(transform, enemySO.moveSpeed, enemySO.detectionRange);
        enemyStateMachine = new EnemyStateMachine();
        enemyStateMachine.ChangeEnemyState(new EnemyIdleState(this, animator, enemyStateMachine));
        healthHandler = new FlyingDemonHealthHandler(this,enemySO, animator, enemyStateMachine);
        attackHandler = new FlyingDemonAttackHandler(enemyStateMachine, this, EnemySO.attackCooldown, transform, animator,projectile);

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

    }
    public void Attack()
    {
        attackHandler.Attack(movementHandler.GetPlayer());
    }
    public void Move()
    {
        movementHandler.Move();
        Position = transform.position;
    }

    public void TakeDamage(IAttack attack, int damage)
    {
        if (IsDead) return;

        HitEffectPlayer.Instance.PlayHitEffect(transform.position, attack.AttackData.hitVfx, attack.AttackData.hitSfx, transform.localScale.x);
        healthHandler.TakeDamage(damage);
        healthBar.UpdateHealthBar();
    }

    public void OnAnimationEnd()
    {
        OnAnimationEndEvent?.Invoke();
    }
}
