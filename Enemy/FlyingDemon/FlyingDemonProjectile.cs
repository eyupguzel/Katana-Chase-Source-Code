using UnityEngine;

public class FlyingDemonProjectile : MonoBehaviour
{
    private Collider2D[] colliders;
    private Player player;

    private Vector2 direction;

    private bool oneTime = true;
    private void Start()
    {
        transform.SetParent(null, true);
        transform.localScale = Vector3.one;
        colliders = Physics2D.OverlapCircleAll(transform.position, 15f);
        foreach (Collider2D collider in colliders)
        {
            player = collider.GetComponent<Player>();
            if (player != null)
                break;
        }
    }
    private void Update()
    {

        if (oneTime)
        {
            direction = (player.transform.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);

            oneTime = false;
        }

        transform.position += (Vector3)direction * 10f * Time.deltaTime;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Wall"))
            Destroy(gameObject);
        if (collision.CompareTag("Player"))
        {
            EventBus<OnPlayerTakeDamageEvent>.Publish(new OnPlayerTakeDamageEvent()
            {
                damage = 20
            });
           /* EventBus<PlayerAudioEvent>.Publish(new PlayerAudioEvent()
            {
                SoundType = PlayerSoundType.BowHit
            });*/
        }
    }
}
