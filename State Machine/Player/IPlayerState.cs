using UnityEngine;

public interface IPlayerState
{
    void Enter();
    void Exit();
}
public interface IUpdatableState
{
    void Update();
}
public interface IFixedUpdatableState
{
    void FixedUpdate();
}
