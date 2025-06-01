using UnityEngine;

public class ComboTracker : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private int comboCount = 0;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject throwablePrefab;

    private IMeleeAttack meleeAttackType;
    private IThrowableAttack throwableAttackType;

    [SerializeField] private AttackDataSO attack1Data;
    [SerializeField] private AttackDataSO attack2Data;
    [SerializeField] private AttackDataSO attack3Data;
    [SerializeField] private AttackDataSO throwAttackData;
    [SerializeField] private AttackDataSO specialAttackData;
    [SerializeField] private AttackDataSO airAttackData;
    [SerializeField] private AttackDataSO defendData;


    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, layerMask);

        if (hit.collider != null && hit.collider.tag == "Ground")
        {
            return true;
        }
        else
            return false;
    }
    public IPlayerState GetCurrentAttackState(Rigidbody2D rb, Animator animator, PlayerStateMachine playerState, IPlayerInputService input)
    {
        EventBus<PlayerStateEvent>.Publish(new PlayerStateEvent
        {
            StateType = PlayerStateType.Attack
        });

        if (!IsGrounded())
        {
            meleeAttackType = new AirAttack(airAttackData, rb, animator, playerState, input);
            meleeAttackType.Attack(rb.transform, boxCollider);
            return meleeAttackType.GetAttactState();
        }

        comboCount++;
        if (comboCount == 1)
        {
            meleeAttackType = new Attack1(attack1Data, rb, animator, playerState, input);
            meleeAttackType.Attack(rb.transform, boxCollider);
            return meleeAttackType.GetAttactState();
        }
        else if (comboCount == 2)
        {
            meleeAttackType = new Attack2(attack2Data, rb, animator, playerState, input);
            meleeAttackType.Attack(rb.transform, boxCollider);
            return meleeAttackType.GetAttactState();
        }
        else if (comboCount == 3)
        {
            meleeAttackType = new Attack3(attack3Data, rb, animator, playerState, input);
            comboCount = 0;
            meleeAttackType.Attack(rb.transform, boxCollider);
            return meleeAttackType.GetAttactState();
        }
        else
            return new IdleState(rb, animator, playerState, input);
    }
    public IPlayerState GetThrowAttackState(Rigidbody2D rb, Animator animator, PlayerStateMachine playerState, IPlayerInputService input)
    {
        throwableAttackType = new Throw(throwAttackData, rb, animator, playerState, input);
        throwableAttackType.Attack(throwablePrefab, transform);
        return throwableAttackType.GetAttactState();
    }
    public IPlayerState GetSpecialAttackState(Rigidbody2D rb, Animator animator, PlayerStateMachine playerState, IPlayerInputService input)
    {
        meleeAttackType = new SpecialAttack(specialAttackData, rb, animator, playerState, input);
        return meleeAttackType.GetAttactState();
    }
    public IPlayerState GetDefendState(Rigidbody2D rb,Animator animator, PlayerStateMachine playerState, IPlayerInputService input)
    {
        meleeAttackType = new Defend(defendData, rb, animator, playerState, input);
        meleeAttackType.Attack(transform, boxCollider);
        return meleeAttackType.GetAttactState();
    }
}
