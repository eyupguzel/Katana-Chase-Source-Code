using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    

    private EventBinding<PlayerAttackEvent> attackEventBinding;
   
   
    private void OnDisable()
    {
        EventBus<PlayerAttackEvent>.Unsubscribe(attackEventBinding);
    }
}
