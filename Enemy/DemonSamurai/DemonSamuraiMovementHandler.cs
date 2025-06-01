using UnityEngine;

public class DemonSamuraiMovementHandler : IMovementHandler
{
    private Collider2D[] colliders;
    private Player player;

    private float moveSpeed;
    private float attackSpeed;
    private float dectectionRange;

    private EnemySO enemySo;
    private Transform transform;

    private Vector2 targetPosition;

    private LayerMask layerMask;

    private float lastDirectionX;
    Vector2 direction;

    public bool IsMovable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public DemonSamuraiMovementHandler(EnemySO enemySo, Transform transform, LayerMask layerMask)
    {
        this.enemySo = enemySo;
        this.transform = transform;
        this.layerMask = layerMask;
        lastDirectionX = 1;
    }
    public void Move()
    {
        SetDirection();
        if (SearchPlayer())
            if (CheckGround() && !ControlDistance())
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, transform.position.y), enemySo.attackSpeed * Time.deltaTime);
    }
    public bool ControlDistance()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < 2.2f)
                return true;
        }
        return false;
    }

    public Player GetPlayer()
    {
        if (player != null)
            return player;
        else
            return null;
    }
    private bool CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, layerMask);
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
            return true;
        else
        {
            if (direction.x != lastDirectionX)
            {
                lastDirectionX = direction.x;
                return true;
            }
            return false;
        }
    }
    public bool SearchPlayer()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, enemySo.detectionRange);
        foreach (Collider2D collider in colliders)
        {
            player = collider.GetComponent<Player>();
            if (player != null)
            {
                targetPosition = player.transform.position;
                return true;
            }
        }
        player = null;
        return false;
    }
    public void SetDirection()
    {
        direction = (targetPosition - (Vector2)transform.position).normalized;
        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        direction.x = Mathf.Ceil(direction.x);
    }
    public void SetMovable()
    {
        throw new System.NotImplementedException();
    }
}
