using UnityEngine;

public class HealingPotionTrigger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private IPlayerInputService inputService;

    private bool interactRequested = false;
    private void Start()
    {
        inputService = InputServiceFactory.CreateInputService();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
    private void Update()
    {
        if(!interactRequested)
            interactRequested = inputService.IsInteractPressed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            spriteRenderer.enabled = true;
            interactRequested = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            if (interactRequested)
            {
                interactRequested = false;
                EventBus<PlayerHealFullEvent>.Publish(new PlayerHealFullEvent());
                Destroy(transform.parent.gameObject);
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
            spriteRenderer.enabled = false;
    }
}
