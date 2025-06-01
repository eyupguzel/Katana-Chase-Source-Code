using UnityEngine;

public class EnemyStateMachine
{
    public IEnemyState currentEnemyState { get; private set; }
    public void ChangeEnemyState(IEnemyState enemyState)
    {
        currentEnemyState?.Exit();
        currentEnemyState = enemyState;
        currentEnemyState.Enter();
    }
    public void Update()
    {
        if (currentEnemyState is IWalkableEnemy walkableEnemy)
        {
            walkableEnemy.Walk();
        }
        else if (currentEnemyState is IFlyableEnemy flyableEnemy)
        {
            flyableEnemy.Fly();
        }
    }
}
