using UnityEngine;

public class RunningMovement : IMovementStrategy
{
    public void Move(Rigidbody2D rigidbody2D,Vector2 input)
    {
        if(input.x > 0)
            rigidbody2D.linearVelocity = new Vector2(7.5f, rigidbody2D.linearVelocity.y);
        else if(input.x < 0)
            rigidbody2D.linearVelocity = new Vector2(-7.5f, rigidbody2D.linearVelocity.y);
        else
            rigidbody2D.linearVelocity = Vector2.zero;
    }

    public void OnMovementEnd(Rigidbody2D rigidbody2D)
    {
        throw new System.NotImplementedException();
    }
}
