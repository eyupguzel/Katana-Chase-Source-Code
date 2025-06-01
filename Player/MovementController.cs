using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    [HideInInspector] public IMovementStrategy currentStrategy;
    private IMovementStrategy previousStrategy;

    private void Start()
    {
        currentStrategy = new RunningMovement();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetMovementStrategy(IMovementStrategy newMovementStrategy)
    {
        if(currentStrategy.GetType() != newMovementStrategy.GetType())
        {
            previousStrategy = currentStrategy;
            currentStrategy = newMovementStrategy;
        }
    }
    public void RestorePreviousMovementStrategy()
    {
        currentStrategy.OnMovementEnd(rb);
        currentStrategy = previousStrategy;
    }
}
