using UnityEngine;


public class JumpAttack : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private DemonSamurai demonSamurai;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        demonSamurai = transform.parent.GetComponent<DemonSamurai>();
    }
    public void GetAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxCollider.size,0);
        foreach (Collider2D collider in colliders)
        {
            Player player = collider.GetComponent<Player>();
            if (player != null)
            {
                player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(50f * demonSamurai.transform.localScale.x,0f);
                Debug.Log(demonSamurai.transform.localScale.x);
                return;
            }
        }
    }
}
