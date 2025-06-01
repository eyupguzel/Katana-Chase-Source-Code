using UnityEngine;

public interface IAttack
{
     IPlayerState AttackState { get; }
    AttackDataSO AttackData { get; }
    IPlayerState GetAttactState();
}
public interface IMeleeAttack : IAttack
{
    void Attack(Transform attackerTransform, BoxCollider2D boxCollider);
}

public interface IThrowableAttack : IAttack
{
    void Attack(GameObject prefab, Transform transform);
}