using System.Threading.Tasks;
using UnityEngine;

public class Defend : IMeleeAttack
{
    public IPlayerState AttackState { get; set; }

    public AttackDataSO AttackData { get; set; }

    public Defend(AttackDataSO attackData, Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService input)
    {
        AttackData = attackData;
        AttackState = new PlayerDefendState(rb, animator, playerStateMachine, input);
    }
    public void Attack(Transform attackerTransform, BoxCollider2D boxCollider)
    {
        _ = HandleDefendAsync(attackerTransform);

    }
    private async Task HandleDefendAsync(Transform attackerTransform)
    {
        float defendDuration = 0.5f;
        float timer = 0f;
        float checkInterval = 0.02f;

        float offsetX = Mathf.Ceil(attackerTransform.parent.localScale.x) == 1 ? 0.6f : -0.6f;

        while (timer < defendDuration)
        {
            Vector2 center = new Vector2(attackerTransform.position.x + offsetX, attackerTransform.position.y);
            //Vector2 center = new Vector2(attackerTransform.position.x + .6f, attackerTransform.position.y);
            Vector2 size = new Vector2(0.2f, 1.5f);

            Collider2D[] hitObjects = Physics2D.OverlapBoxAll(center, size, 0f);
            foreach (Collider2D collider in hitObjects)
            {
                IProjectile projectile = collider.GetComponent<IProjectile>();
                if (projectile != null)
                {
                    EventBus<PlayerAudioEvent>.Publish(new PlayerAudioEvent()
                    {
                        Clip = AttackData.hitSfx,
                    });
                    GameObject.Destroy(collider.gameObject);
                    return;
                }
            }

            await Task.Delay((int)(checkInterval * 1000));
            timer += checkInterval;
        }
    }

    public IPlayerState GetAttactState()
    {
        return AttackState;
    }
}
