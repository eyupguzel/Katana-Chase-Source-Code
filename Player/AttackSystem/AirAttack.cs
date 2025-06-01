using UnityEngine;

public class AirAttack : IMeleeAttack
{
    private AttackDataSO attackData;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerStateMachine playerState;
    private IPlayerInputService input;

    public IPlayerState AttackState {get; private set; }
    public AttackDataSO AttackData { get; private set; }

    public AirAttack(AttackDataSO attackData, Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService input)
    {
        AttackData = attackData;
        AttackState = new AirAttackState(rb, animator, playerStateMachine, input);
    }
    public IPlayerState GetAttactState()
    {
        return AttackState;
    }
    public void Attack(Transform attackerTransform, BoxCollider2D boxCollider)
    {
        EventBus<PlayerAudioEvent>.Publish(new PlayerAudioEvent
        {
            Clip = AttackData.attackSfx
        });

        Vector2 directionMultiplier = new Vector2(Mathf.Sign(attackerTransform.root.localScale.x), 1f);
        Vector2 adjustedOffset = Vector2.Scale(boxCollider.offset, directionMultiplier);
        Vector2 center = (Vector2)attackerTransform.position + adjustedOffset;
        Vector2 size = boxCollider.size;

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(center, size, 0f);

        foreach (var hit in hitEnemies)
        {
            IEnemy enemy = hit?.GetComponent<IEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(this, AttackData.damage);
                return;
            }
        }
    }
}
