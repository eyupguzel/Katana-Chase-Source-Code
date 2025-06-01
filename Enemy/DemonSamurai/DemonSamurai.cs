using System;
using UnityEngine;

public class DemonSamurai : MonoBehaviour, IEnemy
{
    private Player player;
    private Animator animator;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private GameObject projectile;
    public EnemySO EnemySO => enemySO;
    public Vector3 Position { get; set; }
    public bool IsDead { get; set; }
    public Action OnAnimationEndEvent { get; set; }
    public IHealth HealthHandler => healthHandler;

    private EnemyStateMachine stateMachine = new EnemyStateMachine();

    private IMovementHandler movementHandler;
    private IDamaging attackHandler;
    private IHealth healthHandler;

    private JumpAttack jumpAttack;

    private EnemyHealthBar healthBar;

    private void Start()
    {
        animator = GetComponent<Animator>();
        stateMachine.ChangeEnemyState(new DemonSamuraiIdleState(this, animator, stateMachine));
        healthHandler = new DemonSamuraiHealthHandler(this,enemySO, animator, stateMachine);
        movementHandler = new DemonSamuraiMovementHandler(EnemySO, transform, layerMask);
        attackHandler = new DemonSamuraiAttackHandler(this, enemySO, animator, transform, stateMachine);
        player = movementHandler.GetPlayer();

        jumpAttack = transform.Find("JumpAttackCollider").GetComponent<JumpAttack>();

    }


    void Update()
    {
        if(IsDead) return;

        Move();

        Attack();
        stateMachine.Update();
    }
    public void Attack()
    {
        if (movementHandler.ControlDistance())
            attackHandler.Attack(player);
    }

    public void Move()
    {
        if (stateMachine.currentEnemyState is DemonSamuraiMovementState or DemonSamuraiIdleState)
        {
            movementHandler.Move();
            Position = transform.position;
        }
    }
    public void OnAttackAnimation()
    {
        attackHandler.DealDamage();
    }
    public void TakeDamage(IAttack attack, int damage)
    {
        if (IsDead) return;
        //healthBar.UpdateHealthBar();
        HitEffectPlayer.Instance.PlayHitEffect(transform.position, attack.AttackData.hitVfx, attack.AttackData.hitSfx, transform.localScale.x);
        healthHandler.TakeDamage(damage);
    }
    public void OnAnimationEnd()
    {
        OnAnimationEndEvent?.Invoke();
    }
    public void CreateAttackProjectile()
    {
        Instantiate(projectile, transform.position, Quaternion.identity, transform);
    }
    public void JumpAttackAnimation()
    {
        jumpAttack.GetAttack();
    }
}
