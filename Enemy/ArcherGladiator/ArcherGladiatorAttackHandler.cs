using System.Collections;
using UnityEngine;

public class ArcherGladiatorAttackHandler : IAttackHandler
{
    private EnemySO enemySO;
    private MonoBehaviour context;
    private GameObject alertObject;
    private Animator animator;
    private EnemyStateMachine enemyStateMachine;

    private bool isOnCooldown;
    public ArcherGladiatorAttackHandler(EnemySO enemySO,MonoBehaviour context, Animator animator, EnemyStateMachine enemyStateMachine)
    {
        this.context = context;
        this.animator = animator;
        this.enemyStateMachine = enemyStateMachine;
        this.enemySO = enemySO;

        alertObject = context.GetComponent<ArcherGladiatorComboTracker>().alertObject;
    }
    public void Attack(Player player)
    {
        if (isOnCooldown) return;

        context.StartCoroutine(HandleAttackSequence());
    }
    private IEnumerator HandleAttackSequence()
    {
        isOnCooldown = true;
        alertObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        alertObject.SetActive(false);
        IEnemyState state = new ArcherGladiatorAttackState((IEnemy)context, animator, enemyStateMachine);
        if (state != null)
        {
            enemyStateMachine.ChangeEnemyState(state);
        }
        yield return new WaitForSeconds(enemySO.attackCooldown);
        isOnCooldown = false;
    }
}
