using System;
using Unity.VisualScripting;
using UnityEngine;

public class MovementHandler : IMovementHandler
{
    private enum Points
    {
        PointA,
        PointB,
        PointC
    }
    private Points currentPoint;
    private Transform enemyPosition;
    private Collider2D collider2D;
    private Vector2 pointA, pointB, pointC;

    private Vector2 moveDirection;
    private Vector2 targetPoint;
    private float lastDirectionX;

    private Player player;

    private EnemySO enemySO;

    private LayerMask layerMask;

    public MovementHandler(Transform transform, EnemySO enemySO, LayerMask layerMask)
    {
        enemyPosition = transform;
        pointB = new Vector2(enemyPosition.position.x, enemyPosition.position.y);
        pointA = new Vector2(pointB.x + (Vector2.left.x * 5f), pointB.y);
        pointC = new Vector2(pointB.x + (Vector2.right.x * 5f), pointB.y);

        currentPoint = Points.PointB;
        this.enemySO = enemySO;
        this.layerMask = layerMask;

        lastDirectionX = 1;
    }
    public void Move()
    {
        SetDirection();
        if (CheckGround() && !CheckWall())
            if (!SearchPlayer())
            {
                switch (currentPoint)
                {
                    case Points.PointA:
                        targetPoint = pointC;
                        enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, pointB, Time.deltaTime * enemySO.moveSpeed);
                        if (Vector2.Distance(enemyPosition.position, pointB) < 0.1f)
                            currentPoint = Points.PointB;
                        break;
                    case Points.PointB:
                        if (enemyPosition.localScale.x > 0)
                        {
                            targetPoint = pointC;
                            enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, pointC, Time.deltaTime * enemySO.moveSpeed);
                            if (Vector2.Distance(enemyPosition.position, pointC) < 0.1f)
                                currentPoint = Points.PointC;
                            break;
                        }
                        else
                        {
                            targetPoint = pointA;
                            enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, pointA, Time.deltaTime * enemySO.moveSpeed);
                            if (Vector2.Distance(enemyPosition.position, pointA) < 0.1f)
                                currentPoint = Points.PointA;
                            break;
                        }
                    case Points.PointC:
                        targetPoint = pointA;
                        enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, pointB, Time.deltaTime * enemySO.moveSpeed);
                        if (Vector2.Distance(enemyPosition.position, pointB) < 0.1f)
                            currentPoint = Points.PointB;
                        break;

                }
            }
            else
            {
                enemyPosition.position = Vector2.MoveTowards(enemyPosition.position, new Vector2(player.transform.position.x, enemyPosition.position.y), Time.deltaTime * enemySO.attackSpeed);
                targetPoint = player.transform.position;
            }
    }
    public bool ControlDistance()
    {
        if (Vector2.Distance(enemyPosition.position, player.transform.position) < 1.7f)
            return true;
        else
            return false;
    }
    public Player GetPlayer()
    {
        if (player != null)
            return player;
        else
            return null;
    }
    public bool SearchPlayer()
    {
        collider2D = Physics2D.OverlapCircle(enemyPosition.position, enemySO.detectionRange);
        player = collider2D?.GetComponent<Player>();
        if (player != null)
        {
            targetPoint = player.transform.position;
            return true;
        }
        return false;
    }
    private void SetDirection()
    {
        moveDirection = targetPoint - (Vector2)enemyPosition.position;

        if (moveDirection.x > 0)
            enemyPosition.localScale = new Vector3(1, 1, 1);
        else if (moveDirection.x < 0)
            enemyPosition.localScale = new Vector3(-1, 1, 1);

        moveDirection.x = Mathf.Ceil(moveDirection.x);
    }
    public bool CheckWall()
    {
        float direction = enemyPosition.transform.localScale.x == 1 ? 1 : -1;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(enemyPosition.position.x, enemyPosition.position.y), Vector2.right * direction, 3f);
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
            return true;
        else 
            return false;
    }
    public bool CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(enemyPosition.position.x + (enemyPosition.transform.localScale.x == 1 ? +.5f : -.5f), enemyPosition.position.y), Vector2.down, 1.5f, layerMask);
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
            return true;
        else
        {
            if (moveDirection.x != lastDirectionX)
            {
                lastDirectionX = moveDirection.x;
                return true;
            }
            return false;
        }
    }
}
