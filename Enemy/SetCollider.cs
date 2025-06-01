using System;
using System.Collections;
using UnityEngine;

public class SetCollider : MonoBehaviour
{
    private BoxCollider2D[] boxCollider;
    private BoxCollider2D currentBoxCollider;

    private EventBinding<OnPlayerDashing> eventBinding;

    private void Start()
    {
        eventBinding = new EventBinding<OnPlayerDashing>(onPlayerDashing);
        EventBus<OnPlayerDashing>.Subscribe(eventBinding);
        boxCollider = GetComponents<BoxCollider2D>();
        foreach (var collider in boxCollider)
            if (!collider.isTrigger)
                currentBoxCollider = collider;
    }

    private void onPlayerDashing(OnPlayerDashing dashing)
    {
        StartCoroutine(DashingTimer());
    }
    private IEnumerator DashingTimer()
    {
        currentBoxCollider.isTrigger = true;
        yield return new WaitForSeconds(0.5f);
        currentBoxCollider.isTrigger = false;
    }
    private void OnDestroy()
    {
        EventBus<OnPlayerDashing>.Unsubscribe(eventBinding);
    }
}
