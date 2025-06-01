using UnityEngine;
using System.Collections;

public class PlayerEnergyHandler
{
    private EventBinding<PlayerUseEnergyEvent> useEnergyBinding;

    private MonoBehaviour context;
    private float maxEnergy;
    public float currentEnergy { get; private set; }

    private bool isRestoringEnergy = false;

    public PlayerEnergyHandler(MonoBehaviour context)
    {
        maxEnergy = 100;
        currentEnergy = 10;
        this.context = context;

    }
    public bool UseEnergy(float amount)
    {
        if (currentEnergy >= amount)
        {
            currentEnergy -= amount;
            return true;
        }
        else
            return false;
    }

    public void RestoreEnergy(float amount = 0)
    {
        if (!isRestoringEnergy && currentEnergy < maxEnergy)
        {
            context.StartCoroutine(RestoreEnergyCoroutine());
        }
    }

    private IEnumerator RestoreEnergyCoroutine()
    {
        isRestoringEnergy = true;

        while (currentEnergy < maxEnergy)
        {
            yield return new WaitForSeconds(0.1f);

            currentEnergy++;
            EventBus<PlayerEnergyEvent>.Publish(new PlayerEnergyEvent()
            {
                energy = currentEnergy
            });
        }

        isRestoringEnergy = false;
    }
    public float GetCurrentEnergy()
    {
        return currentEnergy;
    }
}
