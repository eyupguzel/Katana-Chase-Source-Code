using System.Collections;
using UnityEngine;

public class DemonSamuraiAttackHandler : IDamaging
{
    private bool isOnCooldown;
    private int comboCount;

    private MonoBehaviour context;
    private EnemySO enemySO;
    private GameObject alertObject;
    private GameObject projectile;
    private EnemyStateMachine stateMachine;
    private Animator animator;
    private Transform transform;

    public DemonSamuraiAttackHandler(MonoBehaviour context, EnemySO enemySO, Animator animator,Transform transform,EnemyStateMachine stateMachine)
    {
        this.context = context;
        this.enemySO = enemySO;
        this.animator = animator;
        this.stateMachine = stateMachine;
        this.transform = transform;
    }
    public void Attack(Player player)
    {
        if(isOnCooldown) return;

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
                    damage = 30
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
        if (comboCount >= 5) comboCount = 0;
        isOnCooldown = true;
        //alertObject.SetActive(true);
        yield return new WaitForSeconds(0.35f);

        IEnemyState state = context.GetComponent<DemonSamuraiComboCounter>().GetCurrentEnemyState((IEnemy)context, animator,stateMachine,comboCount); 
        if(state != null)
            stateMachine.ChangeEnemyState(state);

        //alertObject.SetActive(false);
        comboCount++;
        yield return new WaitForSeconds(enemySO.attackCooldown);
        isOnCooldown = false;
    }
}
