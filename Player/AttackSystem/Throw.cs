using Unity.VisualScripting;
using UnityEngine;

public class Throw : IThrowableAttack
{
    public AttackDataSO AttackData { get; set; }

    public IPlayerState AttackState { get; set; }
    private Rigidbody2D rb;

    public Throw(AttackDataSO attackData, Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService input)
    {
        AttackData = attackData;
        AttackState = new ThrowState(rb, animator, playerStateMachine, input);
    }

    public IPlayerState GetAttactState()
    {
        return AttackState;
    }
    public void Attack(GameObject prefab,Transform transform)
    {
        EventBus<PlayerAudioEvent>.Publish(new PlayerAudioEvent
        {
            Clip = AttackData.attackSfx
        });
        GameObject throwableObjects = Object.Instantiate(prefab, transform.position, Quaternion.identity, transform.parent);
        throwableObjects.GetComponent<IceSpell>().SetAttackState(this);
    }
}
