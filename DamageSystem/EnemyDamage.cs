using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector2 center, size;
    public Player player;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        Vector2 directionMultiplier = new Vector2(Mathf.Sign(transform.root.localScale.x), 1f);
        Vector2 adjustedOffset = Vector2.Scale(boxCollider.offset, directionMultiplier);
        center = (Vector2)transform.position + adjustedOffset;
        size = boxCollider.size;
    }
    public bool IsDamageable()
    {
        Collider2D playerCollider = Physics2D.OverlapBox(center, size, 0f);
        player = playerCollider?.GetComponent<Player>();

        if (player != null)
            return true;

        return false;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
}
