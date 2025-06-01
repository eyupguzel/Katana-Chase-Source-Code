using UnityEngine;

public class JumpingMovement : IMovementStrategy
{
    public void Move(Rigidbody2D rigidbody2D, Vector2 input)
    {
        rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, 15f);
    }

    public void OnMovementEnd(Rigidbody2D rigidbody2D)
    {
        throw new System.NotImplementedException();
    }
}
