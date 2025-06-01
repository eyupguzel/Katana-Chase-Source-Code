using UnityEngine;

public class CheckCollider : Singleton<CheckCollider>
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    public bool IsGround(Transform transform)
    {
        Collider2D ground = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - 1f), new Vector2(1.5f, .2f), 0, groundLayer);

        if (ground != null && (ground.CompareTag("Ground") || ground.CompareTag("Enemy")))
        {
            return true;
        }
        else
            return false;
    }
    public bool IsGround(Vector2 position)
    {
        RaycastHit2D ground = Physics2D.Raycast(position, Vector2.down, 1.5f, groundLayer);
        Debug.DrawRay(position, Vector2.down * 1.5f);
        if (ground.collider != null && ground.collider.CompareTag("Ground"))
        {
            return true;
        }
        else
            return false;
    }
    public bool IsGround(bool isSlading, Transform transform)
    {
        RaycastHit2D ground = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayer);
        if (ground.collider != null && ground.collider.CompareTag("Ground"))
        {
            return true;
        }
        else
            return false;

    }
    public bool IsWall(int direction, Transform transform)
    {
        RaycastHit2D wall = Physics2D.Raycast(transform.position, direction == 1 ? Vector2.right : Vector2.left, 1f, wallLayer);

        if (wall.collider != null && wall.collider.CompareTag("Wall"))
            return true;
        else
            return false;
    }
    public int GetWallType(out RaycastHit2D wall,Transform transform)
    {
        float halfWidth = transform.GetComponent<Collider2D>().bounds.extents.x;

        RaycastHit2D rightWall = Physics2D.Raycast((Vector2)transform.position + Vector2.right * halfWidth, Vector2.right, 1f, wallLayer);
        RaycastHit2D leftWall = Physics2D.Raycast((Vector2)transform.position + Vector2.left * halfWidth, Vector2.left, 1f, wallLayer);
        if (rightWall.collider != null && rightWall.collider.CompareTag("Wall"))
        {
            wall = rightWall;
            return 1;
        }
        else if (leftWall.collider != null && leftWall.collider.CompareTag("Wall"))
        {
            wall = leftWall;
            return -1;
        }
        wall = new RaycastHit2D();
        return 0;
    }
}
