using Unity.VisualScripting;
using UnityEngine;

public interface IEnemyState
{
    void Enter();
    void Exit();
}
public interface  IWalkableEnemy
{
    void Walk();
}
public interface IFlyableEnemy
{
   void Fly();
}
