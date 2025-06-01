using UnityEngine;

public class FlyingDemonMovementHandler : IMovementHandler
{

    private enum Points
    {
        PointA,
        PointB,
        PointC
    }
    private Points currentPoint;
    private Transform enemyPosition;
    private Collider2D[] collider2D;
    private Vector2 pointA, pointB, pointC;

    private Vector2 moveDirection;
    private Vector2 targetPoint;

    private Player player;

    private float moveSpeed;
    private float detectionRange;

    private bool isPlayerFound;
    private bool isMovable = true;


    private float leftMax, rightMax;

    public bool IsMovable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public FlyingDemonMovementHandler(Transform transform, float moveSpeed, float detectionRange)
    {
        enemyPosition = transform;
        pointB = new Vector2(enemyPosition.position.x, enemyPosition.position.y);
        pointA = new Vector2(pointB.x + (Vector2.left.x * 5f), pointB.y);
        pointC = new Vector2(pointB.x + (Vector2.right.x * 5f), pointB.y);

        leftMax = pointA.x;
        rightMax = pointC.x;

        currentPoint = Points.PointB;
        this.moveSpeed = moveSpeed;
        this.detectionRange = detectionRange;
    }
    public void Move()
    {
        if (isMovable)
        {
            SetDirection();
            if (!SearchPlayer())
            {
                switch (currentPoint)
                {
                    case Points.PointA:
                        targetPoint = pointB;
                        enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, pointB, Time.deltaTime * 2f);
                        if (Vector2.Distance(enemyPosition.position, pointB) < 0.1f)
                            currentPoint = Points.PointB;
                        break;
                    case Points.PointB:
                        if (enemyPosition.localScale.x < 0)
                        {
                            targetPoint = pointC;
                            enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, pointC, Time.deltaTime * 2f);
                            if (Vector2.Distance(enemyPosition.position, pointC) < 0.1f)
                                currentPoint = Points.PointC;
                            break;
                        }
                        else
                        {
                            targetPoint = pointA;
                            enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, pointA, Time.deltaTime * 2f);
                            if (Vector2.Distance(enemyPosition.position, pointA) < 0.1f)
                                currentPoint = Points.PointA;
                            break;
                        }
                    case Points.PointC:
                        targetPoint = pointB;
                        enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, pointB, Time.deltaTime * 2f);
                        if (Vector2.Distance(enemyPosition.position, pointB) < 0.1f)
                            currentPoint = Points.PointB;
                        break;

                }
            }
            else
            {
                targetPoint = player.transform.position;

                Vector2 playerPos = player.transform.position;
                Vector2 enemyPos = enemyPosition.position;

                if (enemyPos.y > playerPos.y + 3.2f)
                {
                    enemyPosition.position = Vector2.MoveTowards(enemyPos, playerPos, Time.deltaTime * 4f);
                }
                else
                {
                    float xDistance = enemyPos.x - playerPos.x;

                    if (Mathf.Abs(xDistance) < 8f)
                    {
                        float direction = xDistance >= 0 ? 1 : -1;
                        Vector2 targetPos = new Vector2(playerPos.x + 6f * direction, enemyPos.y);
                        enemyPosition.position = Vector2.MoveTowards(enemyPos, targetPos, Time.deltaTime * 6f);
                    }
                }

            }
        }
    }
    public bool ControlDistance()
    {
        throw new System.NotImplementedException();
    }

    public Player GetPlayer()
    {
        if (player != null)
        {
            return player;
        }
        else
        {
            return null;
        }
    }
    public void SetMovable()
    {
        isMovable = true;
    }
    public bool SearchPlayer()
    {
        collider2D = Physics2D.OverlapCircleAll(enemyPosition.position, 15f);
        foreach (var item in collider2D)
        {
            player = item.GetComponent<Player>();
            if(player != null)
            {
                isPlayerFound = true;
                return true;
            }
        }
        Debug.Log("Player not found");
        player = null;
        isPlayerFound = false;
        return false;
    }
    private void SetDirection()
    {
        moveDirection = targetPoint - (Vector2)enemyPosition.position;
        if (moveDirection.x > 0)
        {
            enemyPosition.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection.x < 0)
        {
            enemyPosition.localScale = new Vector3(1, 1, 1);
        }
        //moveDirection = direction.normalized;

        /* float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
         enemyPosition.rotation = Quaternion.Euler(0, 0, angle);*/
    }
}
