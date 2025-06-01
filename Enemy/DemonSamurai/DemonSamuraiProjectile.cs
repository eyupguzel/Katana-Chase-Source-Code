using UnityEngine;

public class DemonSamuraiProjectile : MonoBehaviour
{
    private float direction;
    private float startPosX;
    private bool oneTime = true;

    private float halfHeight;

    private void Start()
    {
        direction = transform.parent.localScale.x;
        halfHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
        transform.position = new Vector3(transform.position.x, transform.position.y - halfHeight, transform.position.z);
        startPosX = transform.position.x;

        transform.SetParent(null, true);

    }
    private void Update()
    {
        if (Mathf.Abs(transform.position.x - startPosX) !< 10f)
            transform.position += transform.right * direction * 10f * Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
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
