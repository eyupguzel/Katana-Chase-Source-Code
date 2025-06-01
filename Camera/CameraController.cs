using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;

    private float shakeDuration = 0.15f;
    private float shakeAmplitude = 0.035f;
    private float shakeFrequency = 10f;

    private float currentShakeTime = 0f;
    private Vector3 initialPosition;

    Vector3 desiredPosition;

    private EventBinding<PlayerStateEvent> playerStateEvent;


    private void Start()
    {
        offset = transform.position - playerTransform.position;
        playerStateEvent = new EventBinding<PlayerStateEvent>(TriggerShake);
        EventBus<PlayerStateEvent>.Subscribe(playerStateEvent);

    }
    private void LateUpdate()
    {
        desiredPosition.x = playerTransform.position.x + offset.x;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.z = -10f;

        if (currentShakeTime > 0)
        {
            float x = (Mathf.PerlinNoise(Time.time * shakeFrequency, 0f) - 0.5f) * 2f * shakeAmplitude;
            float y = (Mathf.PerlinNoise(0f, Time.time * shakeFrequency) - 0.5f) * 2f * shakeAmplitude;
            Vector3 shakeOffset = new Vector3(x, y, 0f);

            transform.position = smoothedPosition + shakeOffset;
            currentShakeTime -= Time.deltaTime;
        }
        else
        {
            transform.position = smoothedPosition;
        }
    }
    public void TriggerShake(PlayerStateEvent e)
    {
        if (PlayerStateType.Attack == e.StateType)
            currentShakeTime = shakeDuration;
    }
    private void OnDestroy()
    {
        EventBus<PlayerStateEvent>.Unsubscribe(playerStateEvent);
    }

}
