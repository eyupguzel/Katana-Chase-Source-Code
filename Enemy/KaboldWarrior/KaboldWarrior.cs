using System;
using System.Collections;
using UnityEngine;

public class KaboldWarrior : MonoBehaviour, IEnemy
{
    [SerializeField] public EnemySO EnemySO { get; private set; }

    private Animator animator;
    public IMovementHandler movementHandler;
    private IAttackHandler attackHandler;
    private IHealth healthHandler;
    private EnemyStateMachine enemyStateMachine;

    private Player player;
    public bool IsDead { get; set; }
    public Vector3 Position { get; set; }
    public Action OnAnimationEndEvent { get; set; }
    public IHealth HealthHandler => healthHandler;

    private void Start()
    {
        animator = GetComponent<Animator>();
       // movementHandler = new MovementHandler(transform, EnemySO.moveSpeed,EnemySO.detectionRange);
        //healthHandler = new HealthHandler(EnemySO);

        enemyStateMachine = new EnemyStateMachine();
        enemyStateMachine.ChangeEnemyState(new EnemyMovementState(this, animator, enemyStateMachine));
        //attackHandler = new AttackHandler(enemyStateMachine, this, EnemySO.attackCooldown, transform, animator);

    }

    void Update()
    {
        Move();
        if (movementHandler.SearchPlayer())
        {
            player = movementHandler.GetPlayer();
                Attack();
        }

        //enemyStateMachine.Update();
    }
    public void Attack()
    {
        //attackHandler.TryAttack(player);
    }
    public void Move()
    {
        movementHandler.Move();
        Position = transform.position;
    }

    public void TakeDamage(IAttack attack,int damage)
    {
        healthHandler.TakeDamage(damage);
    }
    public void OnAnimationEnd()
    {
        OnAnimationEndEvent?.Invoke();
    }
}
