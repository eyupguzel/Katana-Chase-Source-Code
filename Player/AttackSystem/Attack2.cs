using UnityEngine;

public class Attack2 : IMeleeAttack
{
    public IPlayerState AttackState { get; private set; }

    public AttackDataSO AttackData { get; private set; }

    public Attack2(AttackDataSO attack1Data, Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService input)
    {
        AttackData = attack1Data;
        AttackState = new Attack_2_State(rb, animator, playerStateMachine, input);
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

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(center, size, 1f);

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
