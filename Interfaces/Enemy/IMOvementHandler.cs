using UnityEngine;

public interface IMovementHandler
{
    void Move();
    bool SearchPlayer();
    Player GetPlayer();
    bool ControlDistance();
}
