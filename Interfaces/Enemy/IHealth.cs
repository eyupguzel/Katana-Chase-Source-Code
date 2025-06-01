using UnityEngine;

public interface IHealth
{
    void TakeDamage(int damage);
    float GetCurrentHealth();
}
