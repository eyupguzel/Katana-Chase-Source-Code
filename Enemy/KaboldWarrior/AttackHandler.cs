using System.Collections;
using UnityEngine;

public class AttackHandler : IDamaging
{
    private MonoBehaviour context;
    private EnemySO enemySO;
    private EnemyStateMachine enemyStateMachine;
    private Animator animator;
    private bool isOnCooldown;
    private int comboCount;
    private Transform transform;

    private GameObject alertObject;

    public AttackHandler(EnemySO enemySO,EnemyStateMachine enemyStateMachine, MonoBehaviour context, Animator animator,Transform transform)
    {
        this.animator = animator;
        this.enemyStateMachine = enemyStateMachine;
        this.context = context;
        this.transform = transform;
        this.enemySO = enemySO;

        alertObject = context.GetComponent<KnightCombotracker>().attackAlert;
    }
    public void Attack(Player player)
    {
        if (isOnCooldown) return;

        context.StartCoroutine(HandleAttackSequence());
    }
    public void DealDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + (Mathf.Ceil(transform.localScale.x) == 1 ? 1f : -1f), transform.position.y), new Vector2(1.5f, 3f), 0);
        foreach (Collider2D collider in colliders)
        {
            Player player = collider.gameObject.GetComponent<Player>();
            if (player != null)
            {
                EventBus<OnPlayerTakeDamageEvent>.Publish(new OnPlayerTakeDamageEvent()
                {
                    damage = 15
                });
                EventBus<PlayerAudioEvent>.Publish(new PlayerAudioEvent()
                {
                    SoundType = PlayerSoundType.Hit
                });
                break;
            }
        }
    }
    private IEnumerator HandleAttackSequence()
    {
        isOnCooldown = true;

        alertObject.SetActive(true);

        yield return new WaitForSeconds(0.25f);

        alertObject.SetActive(false);
        if (comboCount >= 3) comboCount = 0;

        IEnemyState state = context.GetComponent<KnightCombotracker>().GetCurrentAttackState(comboCount, (IEnemy)context, animator, enemyStateMachine);
        if(state != null) 
            enemyStateMachine.ChangeEnemyState(state);

        yield return new WaitForSeconds(enemySO.attackCooldown);
        comboCount++;
        isOnCooldown = false;
    }
}
