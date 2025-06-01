using System;
using UnityEngine;

public interface IEnemy
{
    void Move();
    void Attack();
    void TakeDamage(IAttack attack,int damage);
    EnemySO EnemySO { get; }
    IHealth HealthHandler { get; }
    Vector3 Position { get; set; }
    bool IsDead { get; set; }

    Action OnAnimationEndEvent { get; set; }

}
