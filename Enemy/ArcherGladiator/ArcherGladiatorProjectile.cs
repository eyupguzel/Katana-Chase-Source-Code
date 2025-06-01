using UnityEngine;

public class ArcherGladiatorProjectile : MonoBehaviour, IProjectile
{
    private float direction;
    private float startPosX;
    private void Start()
    {
        startPosX = transform.position.x;
        direction = transform.parent.localScale.x;
        transform.SetParent(null, true);
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x - startPosX)! < 20f)
            transform.position += (Vector3)new Vector2(transform.right.x * direction, transform.right.y) * Time.deltaTime * 17f;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            EventBus<OnPlayerTakeDamageEvent>.Publish(new OnPlayerTakeDamageEvent()
            {
                damage = 10
            });
            EventBus<PlayerAudioEvent>.Publish(new PlayerAudioEvent()
            {
                SoundType = PlayerSoundType.BowHit
            });
        }
    }

}
