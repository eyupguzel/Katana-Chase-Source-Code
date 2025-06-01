using UnityEngine;
using System.Collections;


public class FlyingDemonAttackHandler : IAttackHandler
{
    private GameObject projectile;
    private EnemyStateMachine enemyStateMachine;
    private MonoBehaviour context;
    private float attackCooldown;
    private Transform enemyTransform;
    private Animator animator;

    private bool isOnCooldown;

    private GameObject alertObject;
    public FlyingDemonAttackHandler(EnemyStateMachine enemyStateMachine, MonoBehaviour context, float attackCooldown, Transform enemyTransform, Animator animator,GameObject projectile)
    {
        this.enemyStateMachine = enemyStateMachine;
        this.context = context;
        this.attackCooldown = attackCooldown;
        this.enemyTransform = enemyTransform;
        this.animator = animator;
        this.projectile = projectile;

        alertObject = context.GetComponent<FlyingDemonComboTracker>().alertObject;
    }
    public void Attack(Player player)
    {
        if(isOnCooldown) return;

        context.StartCoroutine(HandleAttackSequence());
    }
    private IEnumerator HandleAttackSequence()
    {
        isOnCooldown = true;

        alertObject.SetActive(true);

        yield return new WaitForSeconds(0.25f);

        alertObject.SetActive(false);

        IEnemyState state = context.GetComponent<FlyingDemonComboTracker>().GetCurrentAttackState((IEnemy)context, animator, enemyStateMachine);
        if (state != null)
        {
            enemyStateMachine.ChangeEnemyState(state);
            Object.Instantiate(projectile, enemyTransform.position, Quaternion.identity, enemyTransform);
        }

        yield return new WaitForSeconds(attackCooldown);
        isOnCooldown = false;
    }
}
