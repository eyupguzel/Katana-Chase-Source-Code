using System;
using UnityEngine;

public class ArcherGladiatorMovementHandler : IMovementHandler
{
    private enum Point
    {
        PointA,
        PointB,
        PointC
    }
    private Point currentPoint;

    private Player player;
    private Collider2D[] colliders;
    private Transform enemyTransform;

    private Vector2 pointA, pointB, pointC;
    private Vector2 targetPoint;

    public bool IsMovable { get; set; } = true;

    public ArcherGladiatorMovementHandler(Transform enemyTransform)
    {
        this.enemyTransform = enemyTransform;
        pointB = enemyTransform.position;
        pointA = new Vector2(pointB.x - 2f, pointB.y);
        pointC = new Vector2(pointB.x + 2f, pointB.y);

        currentPoint = Point.PointB;
    }
    public void Move()
    {
        SetDirection();
        if (IsMovable)
        {
            if (!SearchPlayer())
            {
                switch (currentPoint)
                {
                    case Point.PointA:
                        enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, pointB, 2f * Time.deltaTime);
                        if (Vector2.Distance(enemyTransform.position, pointB) < 0.1f)
                        {
                            currentPoint = Point.PointB;
                            targetPoint = pointC;
                        }
                        break;
                    case Point.PointB:
                        if (enemyTransform.localScale.x > 0)
                        {

                            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, pointC, 2f * Time.deltaTime);
                            if (Vector2.Distance(enemyTransform.position, pointC) < 0.1f)
                            {
                                currentPoint = Point.PointC;
                                targetPoint = pointB;
                            }
                        }
                        else
                        {
                            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, pointA, 2f * Time.deltaTime);
                            if (Vector2.Distance(enemyTransform.position, pointA) < 0.1f)
                            {
                                currentPoint = Point.PointA;
                                targetPoint = pointB;
                            }
                        }
                        break;
                    case Point.PointC:
                        enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, pointB, 2f * Time.deltaTime);
                        if (Vector2.Distance(enemyTransform.position, pointB) < 0.1f)
                        {
                            currentPoint = Point.PointB;
                            targetPoint = pointA;
                        }
                        break;
                }
            }
            else
            {
                targetPoint = player.transform.position;
                if (MathF.Abs(enemyTransform.position.x - targetPoint.x) > 15f)
                    enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, new Vector2(targetPoint.x, enemyTransform.position.y), 2f * Time.deltaTime);

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
            return player;
        else
            return null;
    }
    public bool SearchPlayer()
    {
        colliders = Physics2D.OverlapCircleAll(enemyTransform.position, 15f);
        foreach (Collider2D collider in colliders)
        {
            player = collider.GetComponent<Player>();
            if (player != null)
                return true;
        }
        player = null;
        return false;
    }
    private void SetDirection()
    {
        Vector2 direction = (targetPoint - (Vector2)enemyTransform.position).normalized;
        if (direction.x > 0)
            enemyTransform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0)
            enemyTransform.localScale = new Vector3(-1, 1, 1);
    }
}
