using UnityEngine;

public class AttackControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private IPlayerInputService playerInputService;
    private PlayerStateMachine playerStateMachine;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInputService = InputServiceFactory.CreateInputService();
        playerStateMachine = new PlayerStateMachine();
    }
    private void Update()
    {
        if (playerInputService.IsAttackPressed)
        {
            playerStateMachine.ChangeState(new Attack_1_State(rb, animator, playerStateMachine,playerInputService));
        }
        playerStateMachine.Update();
    }
}
