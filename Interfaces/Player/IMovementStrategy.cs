using UnityEngine;

public interface IMovementStrategy
{
    void Move(Rigidbody2D rigidbody2D,Vector2 input);
    void OnMovementEnd(Rigidbody2D rigidbody2D);
}
