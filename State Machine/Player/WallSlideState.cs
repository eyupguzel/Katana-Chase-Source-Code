using UnityEngine;

public class WallSlideState : IPlayerState, IUpdatableState, IFixedUpdatableState
{
    private readonly Rigidbody2D rb;
    private readonly Animator animator;
    private readonly PlayerStateMachine playerStateMachine;
    private readonly IPlayerInputService input;

    private Vector2 playerTransform;
    private float playerWidth;

    private Bounds playerBounds;
    private int wallType;
    private RaycastHit2D wall;
    private float halfWidth;
    private bool oneTime;

    public WallSlideState(Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService playerInputService)
    {
        this.rb = rb;
        this.animator = animator;
        this.playerStateMachine = playerStateMachine;
        input = playerInputService;
    }
    public void Enter()
    {
        playerBounds = rb.GetComponent<Collider2D>().bounds;
        halfWidth = playerBounds.extents.x;
        playerTransform = rb.position;
        wallType = CheckCollider.Instance.GetWallType(out wall,rb.transform);
        playerWidth = PlayerController.Instance.spriteRenderer.bounds.extents.x;
        animator.SetTrigger("WallSlide");
    }
   
    public void Update()
    {
        if (input.IsJumpPressed)
            playerStateMachine.ChangeState(new JumpState(true, rb, animator, playerStateMachine, input));
    }
    public void FixedUpdate()
    {
        rb.linearVelocityY = Vector2.down.y;
        if (!oneTime)
        {
            if (wallType == 1) //Right
            {
                Vector2 targetPos = new Vector2(wall.point.x, rb.position.y);
                rb.MovePosition(targetPos);
                rb.transform.localScale = new Vector3(-1,1,1);
                PlayerController.Instance.setDirection = false;
            }
            else if (wallType == -1) //Left
            {
                Vector2 targetPos = new Vector2(wall.point.x, rb.position.y);
                rb.MovePosition(targetPos);
                rb.transform.localScale = Vector3.one;
                PlayerController.Instance.setDirection = false;
            }
            oneTime = true;
        }

        if (CheckCollider.Instance.IsGround(true,rb.transform))
        {
            playerStateMachine.ChangeState(new IdleState(rb, animator, playerStateMachine, input));
        }
        
    }
    public void Exit()
    {
        PlayerController.Instance.setDirection = true;
        animator.ResetTrigger("WallSlide");
    }
}
