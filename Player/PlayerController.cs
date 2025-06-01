using System;
using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private Rigidbody2D rb;
    private Animator animator;

    public IPlayerInputService inputService;
    private PlayerHealthHandler playerHealthHandler;
    private PlayerEnergyHandler playerEnergyHandler;
    private Vector2 input;

    private int lastDirection = 1;

    public static bool isGround;

    PlayerStateMachine playerStateMachine;

    private ComboTracker comboTracker;

    private float comboTimer = 0f;
    private float comboWindow = 0.5f;
    private bool isComboActive;

    [HideInInspector] public bool setDirection;

    [HideInInspector] public SpriteRenderer spriteRenderer;

    protected override void Init()
    {
        inputService = InputServiceFactory.CreateInputService();
        
        setDirection = true;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        comboTracker = transform.Find("DamageArea").GetComponent<ComboTracker>();

        playerStateMachine = new PlayerStateMachine();
        playerStateMachine.ChangeState(new IdleState(rb, animator, playerStateMachine, inputService));
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealthHandler = new(100, playerStateMachine, rb, animator, inputService);
        playerEnergyHandler = new(this);
    }
    private void Update()
    {
        playerEnergyHandler.RestoreEnergy();

        input = inputService.MovementInput;

        if (inputService.IsAttackPressed)
        {
            IPlayerState attackState = comboTracker.GetCurrentAttackState(rb, animator, playerStateMachine, inputService);
            if (attackState != null)
            {
                playerStateMachine.ChangeState(attackState);
                comboTimer = comboWindow;
                isComboActive = true;
                EventBus<PlayerAttackEvent>.Publish(new PlayerAttackEvent());
            }
        }
        if (inputService.IsAttack1Pressed && isComboActive)
        {
            IPlayerState attackState = comboTracker.GetSpecialAttackState(rb, animator, playerStateMachine, inputService);
            if (attackState != null)
            {
                playerStateMachine.ChangeState(attackState);
                EventBus<PlayerAttackEvent>.Publish(new PlayerAttackEvent());
                isComboActive = false;
            }
        }
        else if(inputService.IsAttack1Pressed && !isComboActive)
        {
            IPlayerState defendState = comboTracker.GetDefendState(rb, animator, playerStateMachine, inputService);
            if (defendState != null)
            {
                playerStateMachine.ChangeState(defendState);
                EventBus<PlayerAttackEvent>.Publish(new PlayerAttackEvent());
            }
        }

        if (inputService.IsAttack2Pressed && playerEnergyHandler.UseEnergy(20))
        {
            IPlayerState attackState = comboTracker.GetThrowAttackState(rb, animator, playerStateMachine, inputService);
            if (attackState != null)
            {
                playerStateMachine.ChangeState(attackState);
                EventBus<PlayerAttackEvent>.Publish(new PlayerAttackEvent());
            }
        }
        if (inputService.IsDashPressed && playerEnergyHandler.UseEnergy(15))
        {
            playerStateMachine.ChangeState(new DashState(rb, animator, playerStateMachine, inputService));
            EventBus<OnPlayerDashing>.Publish(new OnPlayerDashing());
        }

        SetDirection();

        if (playerStateMachine.currentState is IUpdatableState currenState)
            currenState.Update();

        if (isComboActive)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                isComboActive = false;
            }
        }
    }

    private void SetDirection()
    {
        if (setDirection)
            if (input.x > 0)
            {
                lastDirection = 1;
                transform.localScale = new Vector3(lastDirection, 1, 1);
            }
            else if (input.x < 0)
            {
                lastDirection = -1;
                transform.localScale = new Vector3(lastDirection, 1, 1);
            }
            else
                transform.localScale = new Vector3(lastDirection, 1, 1);
    }

    private void FixedUpdate()
    {
        if (playerStateMachine.currentState is IFixedUpdatableState currenState)
            currenState.FixedUpdate();
    }
    public void OnAnimationEnd()
    {
        EventBus<OnPlayerAnimationEndEvent>.Publish(new OnPlayerAnimationEndEvent());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector2(transform.position.x - .5f,transform.position.y), new Vector2(.2f,1.5f));
    }

}
