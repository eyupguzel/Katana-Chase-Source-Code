using UnityEngine;
using UnityEngine.UI;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private float parallaxEffectFactor;

    private Transform cam;
    private float length, startPos;

    private void Start()
    {
        startPos = transform.position.x;
        cam = Camera.main.transform;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void LateUpdate()
    {
        float movement = cam.position.x * (1 - parallaxEffectFactor);
        float distanceX = cam.position.x * parallaxEffectFactor;

        transform.position = new Vector3(startPos + distanceX, transform.position.y, transform.position.z);

        if(movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length)
        {
            startPos -= length;
        }
    }
}
