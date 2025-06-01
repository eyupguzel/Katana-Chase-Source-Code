using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerStateMachine
{
    public IPlayerState currentState { get; private set; }
    public void ChangeState(IPlayerState newState)
    {
        //Debug.Log($"Previous State {currentState}, Current State {newState}");

        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
    public void Update()
    {
        if (currentState is IUpdatableState updateableState)
            updateableState.Update();
    }
    public void FixedUpdate()
    {
        if(currentState is IFixedUpdatableState fixedUpdateableState)
            fixedUpdateableState.FixedUpdate();
    }
}
