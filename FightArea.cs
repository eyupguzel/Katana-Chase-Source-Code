using UnityEngine;

public class FightArea : MonoBehaviour
{
    [SerializeField] private GameObject bossHealthBar;
    private void OnTriggerStay2D(Collider2D collision)
    {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                bossHealthBar.SetActive(true);
            }
    }
}
