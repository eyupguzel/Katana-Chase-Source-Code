using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BloodyScreenEffect : MonoBehaviour
{
    private RectTransform rectTransform;
    private Image image;
    private EventBinding<PlayerHealthEvent> playerHealthBinding;

    private float timer;
    private bool onDanger = false;
    private bool goingUp;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        playerHealthBinding = new EventBinding<PlayerHealthEvent>(OnPlayerHealthLow);
        EventBus<PlayerHealthEvent>.Subscribe(playerHealthBinding);

        image.enabled = false;
    }
    private void Update()
    {
        if (onDanger)
        {
            image.enabled = true;
            timer += Time.deltaTime;
            if (timer > 1)
            {
                goingUp = !goingUp;
                timer = 0;
            }
            float height = goingUp ? Mathf.Lerp(1080, 1800, timer) : Mathf.Lerp(1800, 1080, timer);
            rectTransform.sizeDelta = new Vector2(1920, height);
        }
        else
            image.enabled = false;
    }
    private void OnPlayerHealthLow(PlayerHealthEvent e)
    {
        Debug.Log(e.health);
        if (e.health <= 20)
            onDanger = true;
        else
            onDanger = false;
    }
    private void OnDestroy()
    {
        EventBus<PlayerHealthEvent>.Unsubscribe(playerHealthBinding);
    }
}
