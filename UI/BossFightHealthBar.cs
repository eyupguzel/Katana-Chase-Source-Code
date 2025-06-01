using UnityEngine;

public class BossFightHealthBar : MonoBehaviour
{
    private const float maxHealth = 400;
    private EventBinding<OnBossTakeDamage> onBossTakeDamageBinding;
    private void Start()
    {
        onBossTakeDamageBinding = new EventBinding<OnBossTakeDamage>(UpdateHealthBar);
        EventBus<OnBossTakeDamage>.Subscribe(onBossTakeDamageBinding);
    }
    public void UpdateHealthBar(OnBossTakeDamage e)
    {
        float normalizedHealth = e.health / maxHealth;
        transform.localScale = new Vector3(normalizedHealth, 1, 1);

        if(e.health <= 0)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        EventBus<OnBossTakeDamage>.Unsubscribe(onBossTakeDamageBinding);
    }
}
