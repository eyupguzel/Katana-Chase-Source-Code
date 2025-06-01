using UnityEngine;

public class PlayerEnergyUI : MonoBehaviour
{
    private const float maxEnergy = 100;
    private EventBinding<PlayerEnergyEvent> playerEnergyEvent;
    private void Start()
    {
        playerEnergyEvent = new EventBinding<PlayerEnergyEvent>(UpdatePlayerEnergyUI);
        EventBus<PlayerEnergyEvent>.Subscribe(playerEnergyEvent);
    }
    private void UpdatePlayerEnergyUI(PlayerEnergyEvent e)
    {
        Debug.Log(e);
        float normalizedEnergy = e.energy / maxEnergy;
        transform.localScale = new Vector3(normalizedEnergy,1,1);
    }
}
