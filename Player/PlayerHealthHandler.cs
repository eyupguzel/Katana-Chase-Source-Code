using UnityEngine;

public class PlayerHealthHandler
{
    private EventBinding<OnPlayerTakeDamageEvent> onPlayerTakeDamageEventBinding;
    private EventBinding<PlayerHealFullEvent> onPlayerHealFullEventBinding;
    private float maxHealth;
    private float health;

    private PlayerStateMachine stateMachine;
    private Rigidbody2D rb;
    private Animator animator;
    private IPlayerInputService input;

    public PlayerHealthHandler(float maxHealth,PlayerStateMachine stateMachine,Rigidbody2D rb,Animator animator,IPlayerInputService input)
    {
        onPlayerTakeDamageEventBinding = new EventBinding<OnPlayerTakeDamageEvent>(TakeDamage);
        EventBus<OnPlayerTakeDamageEvent>.Subscribe(onPlayerTakeDamageEventBinding);

        onPlayerHealFullEventBinding = new EventBinding<PlayerHealFullEvent>(PlayerHealing);
        EventBus<PlayerHealFullEvent>.Subscribe(onPlayerHealFullEventBinding);

        this.maxHealth = maxHealth;
        health = maxHealth;

        this.stateMachine = stateMachine;
        this.rb = rb;
        this.animator = animator;
        this.input = input;

    }

    public float GetCurrentHealth()
    {
        return health;
    }
    private void TakeDamage(OnPlayerTakeDamageEvent e)
    {
        stateMachine.ChangeState(new HurtState(rb,animator,stateMachine,input));
        health -= e.damage;
        EventBus<PlayerHealthEvent>.Publish(new PlayerHealthEvent()
        {
            health = health
        });
        if (health <= 0)
            Debug.Log("Player is die");
    }
    private void PlayerHealing()
    {
        stateMachine.ChangeState(new HealState(animator, stateMachine, rb, input));
        health = maxHealth;

        EventBus<PlayerHealthEvent>.Publish(new PlayerHealthEvent()
        {
            health = health
        });
    }
    
}
