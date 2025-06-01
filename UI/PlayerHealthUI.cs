using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    private const float maxHealth = 100;

    private EventBinding<PlayerHealthEvent> playerHealthEvent;
    private void Start()
    {
        playerHealthEvent = new EventBinding<PlayerHealthEvent>(UpdatePlayerHealthUI);
        EventBus<PlayerHealthEvent>.Subscribe(playerHealthEvent);
    }
    public void UpdatePlayerHealthUI(PlayerHealthEvent e)
    {
        float normalizedHealth = e.health / maxHealth;
        transform.localScale = new Vector3(normalizedHealth, 1, 1);
    }
    private void OnDestroy()
    {
        EventBus<PlayerHealthEvent>.Unsubscribe(playerHealthEvent);
    }
}
