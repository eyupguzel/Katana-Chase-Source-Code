using UnityEngine;

public interface IEvent { }

public struct PlayerHealthEvent : IEvent
{
    public float health;
}
public struct PlayerEnergyEvent : IEvent
{
    public float energy;
}
public struct PlayerUseEnergyEvent : IEvent
{
    public float amount;
}


public struct PlayerHealFullEvent : IEvent { }
public struct PlayerAttackEvent : IEvent { }
public enum PlayerSoundType { Jump, Land,Run,Dash ,Hurt, Block,Death,Hit,BowHit}
public class PlayerAudioEvent : IEvent
{
    public PlayerSoundType SoundType;
    public AudioClip Clip;
}
public enum PlayerStateType { Idle,Run,Jump,JumpEnd,Attack};

public struct PlayerStateEvent : IEvent
{
    public PlayerStateType StateType;
}
public struct  EnemyAttackEvent : IEvent
{
    public int value;
}
public struct OnKnightAnimationEndEvent : IEvent { }
public struct OnPlayerAnimationEndEvent : IEvent { }
public struct OnPlayerTakeDamageEvent : IEvent
{
    public float damage;
}
public struct OnPlayerDashing : IEvent{ }
public struct OnBossTakeDamage : IEvent
{
    public float health;
}